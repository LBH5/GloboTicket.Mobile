using GloboTicket.Admin.Mobile.Model;

namespace GloboTicket.Admin.Mobile.Services;


public class NavigationService : INavigationService
{

    public async Task NavigateToEventDetailPageAsync(Guid eventId)
    {
        var parameters = new Dictionary<string, object>
        {
            { "EventId", eventId }
        };
        await Shell.Current.GoToAsync(nameof(View.EventDetailPage), parameters);
    }

    public async Task NavigateToAddEventPageAsync()
     => await Shell.Current.GoToAsync("event/add");

    public async Task NavigateToEditEventPageAsync(EventModel detailModel)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Event", detailModel }
        };
        await Shell.Current.GoToAsync("event/edit", parameters);
    }

    public async Task GoBackAsync()
        => await Shell.Current.GoToAsync("..");

    public async Task NavigateToOverviewPageAsync()
        => await Shell.Current.GoToAsync("//EventOverviewPage");
}   