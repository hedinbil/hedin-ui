using Azure.AI.OpenAI;
using Azure;
using Azure.Identity;
using Azure.Search.Documents;
using Hedin.UI.Demo.Services;
using Hedin.UI.Demo.Data;
using MudBlazor;
using Brism;

namespace Hedin.UI.Demo.Configuration;

public static class WebApplicationBuilderExtensions
{

    public static WebApplicationBuilder AddExternalServices(this WebApplicationBuilder builder)
    {

        var externalResourceStatus = new ExternalResourceStatus();
        try
        {
            builder
                .AddKeyVault()
                .AddAIChatBot();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            Console.WriteLine("Failed to connect to keyvault. Running offline!");
            externalResourceStatus.SetOffline();
        }
        builder.Services.AddSingleton(externalResourceStatus);
        return builder;

    }
    public static WebApplicationBuilder AddKeyVault(this WebApplicationBuilder builder)
    {

        var keyVaultName = builder.Configuration.GetValue<string>("KeyVaultName");
        ArgumentException.ThrowIfNullOrWhiteSpace(keyVaultName);
        builder.Configuration.AddAzureKeyVault(new Uri($"https://{keyVaultName}.vault.azure.net/"), new DefaultAzureCredential());
        return builder;
    }

    public static WebApplicationBuilder AddAIChatBot(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(provider =>
        {
            var azureClient = new AzureOpenAIClient(
                new Uri(builder.Configuration["AzureOpenAI:Endpoint"]!),
                new AzureKeyCredential(builder.Configuration["AzureOpenAI:ApiKey"]!)
            );
            return azureClient;
        });

        // Register the ChatClient
        builder.Services.AddSingleton(provider =>
        {
            var azureClient = provider.GetRequiredService<AzureOpenAIClient>();
            return azureClient.GetChatClient(builder.Configuration["AzureOpenAI:DeploymentName"]!);
        });


        builder.Services.AddSingleton(provider =>
        {
            var searchEndpoint = new Uri(builder.Configuration["CognitiveSearch:Endpoint"]!);
            var searchApiKey = builder.Configuration["CognitiveSearch:ApiKey"]!;
            var indexName = builder.Configuration["CognitiveSearch:IndexName"]!;
            return new SearchClient(searchEndpoint, indexName, new AzureKeyCredential(searchApiKey));
        });

        return builder;
    }



    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<CodeBlockService>();
        builder.Services.AddSingleton<MarkdownReader>();
        builder.Services.AddTransient<HUILocalizer>();
        builder.Services.AddSingleton<AiMessageStateService>();
        return builder;
    }
    public static WebApplicationBuilder AddUIConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddBrism();
        builder.Services.AddSingleton<ComponentCatalogService>();
        builder.Services.AddUIConfiguration(builder.Configuration);
        builder.Services.AddMudMarkdownServices();
        builder.Services.AddRazorPages();
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        return builder;
    }

}
