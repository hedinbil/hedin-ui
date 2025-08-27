using Hedin.UI.Services;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;

public abstract class StatefulComponentBase : ComponentBase, IDisposable
{
    [Inject] protected IStateService StateService { get; set; }
    [Inject] protected NavigationManager NavigationManager { get; set; }

    protected string PageKey { get; private set; }
    private string _previousUri;
    private string _currentUri;

    protected override void OnInitialized()
    {
        SetPageKey();
        _currentUri = NavigationManager.Uri;  // Initialize the current URI
        NavigationManager.LocationChanged += OnLocationChanged;
        base.OnInitialized();
    }

    protected override Task OnInitializedAsync()
    {
        // Only load the state if this is the first time the page is loaded, not on subsequent navigations
        if (string.IsNullOrEmpty(_previousUri))
        {
            LoadState();
        }
        return base.OnInitializedAsync();
    }

    protected abstract void LoadState();
    protected abstract void SaveState();
    protected abstract void ClearState();

    protected void SetPageKey()
    {
        PageKey = NavigationManager.Uri;
    }

    private void LoadStateIfNecessary()
    {
        if (_previousUri != null && ShouldLoadState(_previousUri))
        {
            LoadState();
        }
    }

    private bool ShouldLoadState(string previousUri)
    {
        var parentPageUri = NavigationManager.ToAbsoluteUri(PageKey).GetLeftPart(UriPartial.Path);
        var previousPageUri = NavigationManager.ToAbsoluteUri(previousUri).GetLeftPart(UriPartial.Path);

        // Check if the previous page was a subpage of the current page
        return previousPageUri.StartsWith(parentPageUri) && previousPageUri != parentPageUri;
    }

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

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}


