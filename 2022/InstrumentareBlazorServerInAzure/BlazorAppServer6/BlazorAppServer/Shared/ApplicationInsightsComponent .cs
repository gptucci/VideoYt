using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorAppServer.Shared;

public class ApplicationInsightsComponent:ComponentBase,IDisposable
{

    [Inject]
    private TelemetryClient _telemetryClient { get; init; }

    [Inject]
    private NavigationManager _navigationManager { get; init; }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _navigationManager.LocationChanged += NavigationManagerOnLocationChanged;
        }
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        _telemetryClient.TrackPageView(e.Location); 

    }




    public void Dispose()
    {
        _navigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }

}
