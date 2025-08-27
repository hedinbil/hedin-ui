using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hedin.UI.Demo.Services.MCP.Client;

public class SemanticChatService
{
    private readonly Kernel _kernel;
    private readonly IChatCompletionService _chatSvc;
    private readonly McpPluginProvider _pluginProvider;
    private readonly AzureOpenAiOptions _options;

    // one ChatHistory _per_ Blazor circuit
    private readonly ChatHistory _history = new();

    // ensure we only load plugins once
    private Task? _loadPluginsTask;

    public SemanticChatService(
        Kernel kernel,
        McpPluginProvider pluginProvider,
        IOptions<AzureOpenAiOptions> options)
    {
        _kernel = kernel;
        _pluginProvider = pluginProvider;
        _options = options.Value;
        _chatSvc = kernel.Services.GetService<IChatCompletionService>()!;

        _history.AddSystemMessage(_options.SystemPrompt);
    }

    // Have to lazy load this. Otherwise add this on startup but since mcp server and client exist in the same host, the client cannot be configured before mcp server has fully started up.
    private static readonly SemaphoreSlim _pluginLock = new(1, 1);

    private Task EnsurePluginsAsync()
    {
        // If someone's already kicked off the load, just return that Task.
        if (_loadPluginsTask != null)
            return _loadPluginsTask;

        // Otherwise, atomically initialize it.
        _loadPluginsTask = LoadPluginsAsync();
        return _loadPluginsTask;
    }

    private async Task LoadPluginsAsync()
    {
        // only one loader at a time
        await _pluginLock.WaitAsync().ConfigureAwait(false);
        try
        {
            // double-check after taking the lock
            if (_kernel.Plugins.Any())
                return;

            var all = await _pluginProvider.LoadAsync().ConfigureAwait(false);
            foreach (var p in all)
            {
                // optionally check by name to avoid dupes
                if (!_kernel.Plugins.Any(x => x.Name == p.Name))
                    _kernel.Plugins.Add(p);
            }
        }
        finally
        {
            _pluginLock.Release();
        }
    }

    public async Task<string> AskAsync(string userPrompt, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(userPrompt))
            return string.Empty;

        // make sure our /mcp plugins are imported just-in-time
        await EnsurePluginsAsync().ConfigureAwait(false);

        // record user turn
        _history.AddUserMessage(userPrompt);

        // configure function-calling, etc.
        var settings = new AzureOpenAIPromptExecutionSettings
        {
            Temperature = 0.7f,
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };

        // one‐shot completion
        var reply = await _chatSvc.GetChatMessageContentAsync(
            _history,
            settings,
            _kernel,
            ct
        ).ConfigureAwait(false);

        // record assistant turn
        _history.AddAssistantMessage(reply.Content);

        return reply.Content;
    }

    public async IAsyncEnumerable<string> StreamAsync(
        string userPrompt,
        [EnumeratorCancellation] CancellationToken ct = default
    )
    {
        await EnsurePluginsAsync().ConfigureAwait(false);

        // record user turn
        _history.AddUserMessage(userPrompt);

        var buffer = new StringBuilder();
        var settings = new AzureOpenAIPromptExecutionSettings
        {
            Temperature = 0.7f,
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };

        await foreach (var piece in _chatSvc.GetStreamingChatMessageContentsAsync(
            _history, settings, _kernel, ct
        ).ConfigureAwait(false))
        {
            buffer.Append(piece.Content);
            yield return piece.Content;
        }

        // record the completed assistant turn
        _history.AddAssistantMessage(buffer.ToString());
    }
}
