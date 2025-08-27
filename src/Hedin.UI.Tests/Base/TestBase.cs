using System.Collections.Generic;
using Bunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

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

        // Register Hedin.UI’s DI graph (+ MudBlazor + Blazored.LocalStorage, etc.)
        Services.AddUIConfiguration(Configuration);

        // If any components hit JS interop, keep it loose to avoid strict mocks.
        JSInterop.Mode = JSRuntimeMode.Loose;

        // Optional: if you need Mud services directly, they are already added by AddUIConfiguration.
        // If you want extra services for tests, add them here:
        // Services.AddLogging();
    }
}
