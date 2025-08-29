using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Bunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using MudBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace Hedin.UI.Tests.Base;
/// <summary>
/// Base class for Blazor component tests.
/// Provides a TestContext with Hedin.UI services configured
/// and a minimal IConfiguration.
/// </summary>
public abstract class UiTestBase : TestContext
{
    protected IConfiguration Configuration { get; }

    protected UiTestBase(IDictionary<string, string?>? seedConfig = null)
    {
        // Build a minimal in-memory configuration; seed values if needed.
        Configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(seedConfig ?? new Dictionary<string, string?>())
            .Build();

        // Register Hedin.UI's DI graph (+ MudBlazor + Blazored.LocalStorage, etc.)
        Services.AddUIConfiguration(Configuration);

        // If any components hit JS interop, keep it loose to avoid strict mocks.
        JSInterop.Mode = JSRuntimeMode.Loose;

        // Optional: if you need Mud services directly, they are already added by AddUIConfiguration.
        // If you want extra services for tests, add them here:
        // Services.AddLogging();
        
        // Add test implementations of required services
        Services.AddSingleton<HUILocalizer, TestHUILocalizer>();
        
        // Add authorization services for components that use AuthorizeView
        Services.AddSingleton<IAuthorizationPolicyProvider, TestAuthorizationPolicyProvider>();
        Services.AddSingleton<IAuthorizationService, TestAuthorizationService>();
        Services.AddSingleton<IAuthorizationHandlerProvider, TestAuthorizationHandlerProvider>();
        Services.AddSingleton<IAuthorizationEvaluator, TestAuthorizationEvaluator>();
        
        // Add authentication services for components that use AuthorizeView
        Services.AddSingleton<AuthenticationStateProvider, TestAuthenticationStateProvider>();
    }

    protected IRenderedComponent<TComponent> RenderComponentWithMudProviders<TComponent>(
        Action<ComponentParameterCollectionBuilder<TComponent>> parameterBuilder)
        where TComponent : class, IComponent
    {
        // Build params -> fragment that renders <TComponent ... />
        var fragment = new ComponentParameterCollectionBuilder<TComponent>(parameterBuilder)
            .Build()
            .ToRenderFragment<TComponent>();

        var shell = RenderComponent<MudTestHost>(host => host.Add(h => h.ChildContent, fragment));
        return shell.FindComponent<TComponent>();
    }

    protected IRenderedComponent<TComponent> RenderComponentWithMudProviders<TComponent>(
        params (string Name, object? Value)[] parameters)
        where TComponent : class, IComponent
    {
        var builder = new ComponentParameterCollectionBuilder<TComponent>();
        foreach (var (name, value) in parameters)
            builder.TryAdd(name, value); // name/value fallback

        var fragment = builder.Build().ToRenderFragment<TComponent>();

        var shell = RenderComponent<MudTestHost>(host => host.Add(h => h.ChildContent, fragment));
        return shell.FindComponent<TComponent>();
    }
    
    /// <summary>
    /// Test implementation of HUILocalizer that returns the key as the value
    /// </summary>
    private class TestHUILocalizer : HUILocalizer
    {
        public override LocalizedString this[string key] => new(key, key, false);
    }
    
    /// <summary>
    /// Test implementation of IAuthorizationPolicyProvider that returns a default policy
    /// </summary>
    private class TestAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName) => Task.FromResult<AuthorizationPolicy?>(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy?>(null);
    }
    
    /// <summary>
    /// Test implementation of IAuthorizationService that always succeeds
    /// </summary>
    private class TestAuthorizationService : IAuthorizationService
    {
        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, IEnumerable<IAuthorizationRequirement> requirements) => Task.FromResult(AuthorizationResult.Success());
        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, string policyName) => Task.FromResult(AuthorizationResult.Success());
    }
    
    /// <summary>
    /// Test implementation of IAuthorizationHandlerProvider that returns empty handlers
    /// </summary>
    private class TestAuthorizationHandlerProvider : IAuthorizationHandlerProvider
    {
        public Task<IEnumerable<IAuthorizationHandler>> GetHandlersAsync(AuthorizationHandlerContext context) => Task.FromResult(Enumerable.Empty<IAuthorizationHandler>());
    }
    
    /// <summary>
    /// Test implementation of IAuthorizationEvaluator that always succeeds
    /// </summary>
    private class TestAuthorizationEvaluator : IAuthorizationEvaluator
    {
        public AuthorizationResult Evaluate(ClaimsPrincipal user, object? resource, IEnumerable<IAuthorizationRequirement> requirements) => AuthorizationResult.Success();
        
        public AuthorizationResult Evaluate(AuthorizationHandlerContext context) => AuthorizationResult.Success();
    }
    
    /// <summary>
    /// Test implementation of AuthenticationStateProvider that provides an authenticated user
    /// </summary>
    private class TestAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser") }, "Test");
            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }
        
        public event AuthenticationStateChangedHandler? AuthenticationStateChanged;
        
        public void NotifyAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            AuthenticationStateChanged?.Invoke(task);
        }
    }
}
