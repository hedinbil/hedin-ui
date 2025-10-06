using Hedin.UI;
using Hedin.UI.Demo;
using Hedin.UI.Demo.Configuration;
using Hedin.UI.Demo.Services;
using Hedin.UI.Demo.Services.MCP.Client;
using Hedin.UI.Demo.Services.MCP.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

builder
    .AddExternalServices()
    .AddUIConfiguration()
    .AddServices()
    .AddSemanticAi();

#if DEBUG
builder.Services.AddSassCompiler();
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.MapStaticAssets();

app.UseRouting();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .WithStaticAssets();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

// Map SEO endpoints
app.MapControllers();

app.MapMcp(pattern: "mcp").AllowAnonymous();

app.Run();
