using Hedin.UI.Services;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;

/// <summary>
/// Base class for components that need to maintain state across navigation.
/// Automatically handles saving and loading component state when navigating between pages.
/// Components inheriting from this class must implement LoadState, SaveState, and ClearState methods.
/// </summary>
public abstract class StatefulComponentBase : ComponentBase, IDisposable
{
    /// <summary>
    /// Service for managing component state persistence across navigation.
    /// </summary>
    [Inject] protected IStateService StateService { get; set; }
    
    /// <summary>
    /// Service for managing navigation and location changes.
    /// </summary>
    [Inject] protected NavigationManager NavigationManager { get; set; }

    /// <summary>
    /// Unique identifier for the current page, used as a key for state storage.
    /// </summary>
    protected string PageKey { get; private set; }
    
    private string _previousUri;
    private string _currentUri;

    /// <summary>
    /// Initializes the component and sets up navigation change event handling.
    /// </summary>
    protected override void OnInitialized()
    {
        SetPageKey();
        _currentUri = NavigationManager.Uri;  // Initialize the current URI
        NavigationManager.LocationChanged += OnLocationChanged;
        base.OnInitialized();
    }

    /// <summary>
    /// Loads component state if this is the first time the page is loaded.
    /// </summary>
    protected override Task OnInitializedAsync()
    {
        // Only load the state if this is the first time the page is loaded, not on subsequent navigations
        if (string.IsNullOrEmpty(_previousUri))
        {
            LoadState();
        }
        return base.OnInitializedAsync();
    }

    /// <summary>
    /// Abstract method that derived components must implement to load their state.
    /// Called when the component is initialized or when returning from a subpage.
    /// </summary>
    protected abstract void LoadState();
    
    /// <summary>
    /// Abstract method that derived components must implement to save their state.
    /// Called before navigating away from the current page.
    /// </summary>
    protected abstract void SaveState();
    
    /// <summary>
    /// Abstract method that derived components must implement to clear their state.
    /// Called when navigating to a different page that is not a subpage of the current page.
    /// </summary>
    protected abstract void ClearState();

    /// <summary>
    /// Sets the page key based on the current navigation URI.
    /// </summary>
    protected void SetPageKey()
    {
        PageKey = NavigationManager.Uri;
    }

    /// <summary>
    /// Determines if state should be loaded based on navigation context.
    /// </summary>
    /// <param name="previousUri">The URI of the previous page.</param>
    /// <returns>True if state should be loaded, false otherwise.</returns>
    private void LoadStateIfNecessary()
    {
        if (_previousUri != null && ShouldLoadState(_previousUri))
        {
            LoadState();
        }
    }

    /// <summary>
    /// Checks if the previous page was a subpage of the current page.
    /// </summary>
    /// <param name="previousUri">The URI of the previous page.</param>
    /// <returns>True if the previous page was a subpage, false otherwise.</returns>
    private bool ShouldLoadState(string previousUri)
    {
        var parentPageUri = NavigationManager.ToAbsoluteUri(PageKey).GetLeftPart(UriPartial.Path);
        var previousPageUri = NavigationManager.ToAbsoluteUri(previousUri).GetLeftPart(UriPartial.Path);

        // Check if the previous page was a subpage of the current page
        return previousPageUri.StartsWith(parentPageUri) && previousPageUri != parentPageUri;
    }

    /// <summary>
    /// Handles navigation location changes and manages state persistence.
    /// Saves state when navigating away and loads state when returning from subpages.
    /// </summary>
    /// <param name="sender">The source of the navigation event.</param>
    /// <param name="e">The navigation change event arguments.</param>
    private void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        // Save the current state before navigating away
        if (e.Location.StartsWith(_currentUri))
            SaveState();
        else ClearState();


        // Update URIs for the next navigation step
        _previousUri = _currentUri;  // Set the previous URI to the current one
        _currentUri = e.Location;    // Set the current URI to the new location

        // After navigating, load the state if necessary
        LoadStateIfNecessary();
    }

    /// <summary>
    /// Disposes the component and removes navigation event handlers.
    /// </summary>
    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}


