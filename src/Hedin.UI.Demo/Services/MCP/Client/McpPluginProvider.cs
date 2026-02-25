#pragma warning disable SKEXP0010, SKEXP0001, SKEXP0020, KMEXP00
using Microsoft.SemanticKernel;
using ModelContextProtocol.Client;

namespace Hedin.UI.Demo.Services.MCP.Client;

public class McpPluginProvider
{
    private readonly IConfiguration _cfg;
    private readonly IHttpClientFactory _http;
    private readonly ILogger<McpPluginProvider> _log;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public McpPluginProvider(
        IConfiguration cfg,
        IHttpClientFactory http,
        ILogger<McpPluginProvider> log,
        IHttpContextAccessor httpContextAccessor)
    {
        _cfg = cfg;
        _http = http;
        _log = log;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<KernelPlugin>> LoadAsync()
    {
        var list = new List<KernelPlugin>();
        var servers = _cfg.GetSection("McpServersOptions")
                          .Get<List<McpServerConfig>>()!;

        var req = _httpContextAccessor.HttpContext!.Request;
        var baseUri = new Uri($"{req.Scheme}://{req.Host}");
        foreach (var s in servers)
        {
            try
            {
                Uri endpointUri;
                if (Uri.IsWellFormedUriString(s.Endpoint, UriKind.Absolute))
                {
                    endpointUri = new Uri(s.Endpoint, UriKind.Absolute);
                }
                else
                {
                    // treat as relative path
                    endpointUri = new Uri(baseUri, s.Endpoint);
                }

                var httpClient = _http.CreateClient();
                var options = new ModelContextProtocol.Client.HttpClientTransportOptions
                {
                    Endpoint = endpointUri,
                    TransportMode = HttpTransportMode.AutoDetect,
                    AdditionalHeaders = s.Headers
                };
                var transport = new HttpClientTransport(options, httpClient);

                // <-- add ConfigureAwait(false) to prevent deadlock -->
                var mcp = await McpClient
                                  .CreateAsync(transport)
                                  .ConfigureAwait(false);
                var tools = await mcp
                                  .ListToolsAsync()
                                  .ConfigureAwait(false);

                var plugin = KernelPluginFactory.CreateFromFunctions(
                    pluginName: s.Name,
                    functions: tools.Select(t => t.AsKernelFunction())
                );
                list.Add(plugin);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error loading MCP plugin {Name}", s.Name);
            }
        }

        return list;
    }

    class McpServerConfig
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Endpoint { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}
