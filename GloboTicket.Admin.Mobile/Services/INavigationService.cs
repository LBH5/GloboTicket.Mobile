using GloboTicket.Admin.Mobile.Model;

namespace GloboTicket.Admin.Mobile.Services;


public interface INavigationService
{
    Task NavigateToEventDetailPageAsync(Guid eventId);
    Task NavigateToAddEventPageAsync();
    Task NavigateToEditEventPageAsync(EventModel detailModel);
}