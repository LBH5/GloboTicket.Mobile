using GloboTicket.Admin.Mobile.Model;

namespace GloboTicket.Admin.Mobile.Services;


public class NavigationService : INavigationService
{

    public Task NavigateToEventDetailPageAsync(Guid eventId)
    {
        var parameters = new Dictionary<string, object>
        {
            { "EventId", eventId }
        };
        return Shell.Current.GoToAsync(nameof(View.EventDetailPage), parameters);
    }

    public Task NavigateToAddEventPageAsync()
     => Shell.Current.GoToAsync("event/add");

    public async Task NavigateToEditEventPageAsync(EventModel detailModel)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Event", detailModel }
        };
        await Shell.Current.GoToAsync("event/edit", parameters);
    }
}   