using Hedin.UI.Demo.Services.MCP.Server;
using Microsoft.SemanticKernel;

namespace Hedin.UI.Demo.Services.MCP.Client
{
    public static class McpClientConfiguration
    {
        public static WebApplicationBuilder AddSemanticAi(this WebApplicationBuilder builder)
        {
            builder.Services
                .Configure<AzureOpenAiOptions>(builder.Configuration.GetSection("AzureOpenAi"))
                .AddOptions<AzureOpenAiOptions>()
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<McpPluginProvider>();

            builder.Services.AddSingleton<Kernel>(sp =>
            {
                var cfg = sp.GetRequiredService<IConfiguration>();
                var endpoint = new Uri(cfg["AzureOpenAi:Endpoint"]!);
                var apiKey = cfg["AzureOpenAi:ApiKey"]!;
                var deployment = cfg["AzureOpenAi:DeploymentName"]!;

                return Kernel.CreateBuilder()
                    .AddAzureOpenAIChatCompletion(
                        deploymentName: deployment,
                        endpoint: endpoint.AbsoluteUri,
                        apiKey: apiKey)
                    .Build();
            });

            builder.Services.AddScoped<SemanticChatService>();

            builder.Services.AddMcpServer()
                .WithHttpTransport()
                .WithTools<HedinUiMcpTools>();
            
            builder.Services.AddScoped<SeoService>();
            builder.Services.AddScoped<SitemapService>();
            return builder;
        }
    }
    public class AzureOpenAiOptions
    {
        public required string Endpoint { get; set; }
        public required string ApiKey { get; set; }
        public required string DeploymentName { get; set; }
        public required string SystemPrompt { get; set; }
    }
}
