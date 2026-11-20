namespace Hedin.UI.Demo.Services.MCP.Server
{
    public static class McpServerConfiguration
    {
        /// <summary>
        /// Hosts the Hedin UI MCP server (mapped at /mcp) so AI coding tools can look up components and examples.
        /// </summary>
        public static WebApplicationBuilder AddMcpServer(this WebApplicationBuilder builder)
        {
            builder.Services.AddMcpServer()
                .WithHttpTransport()
                .WithTools<HedinUiMcpTools>();

            return builder;
        }
    }
}
