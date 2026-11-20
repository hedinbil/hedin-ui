using Azure.Identity;
using Hedin.UI.Demo.Services;
using Hedin.UI.Demo.Data;
using MudBlazor;
using Brism;

namespace Hedin.UI.Demo.Configuration;

public static class WebApplicationBuilderExtensions
{

    public static WebApplicationBuilder AddExternalServices(this WebApplicationBuilder builder)
    {
        try
        {
            builder.AddKeyVault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            Console.WriteLine("Failed to connect to keyvault. Running offline!");
        }
        return builder;
    }

    public static WebApplicationBuilder AddKeyVault(this WebApplicationBuilder builder)
    {

        var keyVaultName = builder.Configuration.GetValue<string>("KeyVaultName");
        ArgumentException.ThrowIfNullOrWhiteSpace(keyVaultName);
        var credential = builder.Environment.IsDevelopment()
            ? new DefaultAzureCredential(new DefaultAzureCredentialOptions
            {
                ExcludeManagedIdentityCredential = true,
                ExcludeWorkloadIdentityCredential = true,
                ExcludeEnvironmentCredential = true,
            })
            : new DefaultAzureCredential();
        builder.Configuration.AddAzureKeyVault(new Uri($"https://{keyVaultName}.vault.azure.net/"), credential);
        return builder;
    }

    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<CodeBlockService>();
        builder.Services.AddSingleton<MarkdownReader>();
        builder.Services.AddTransient<HUILocalizer>();
        builder.Services.AddScoped<CookieConsentService>();
        builder.Services.AddScoped<SeoService>();
        builder.Services.AddScoped<SitemapService>();
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
