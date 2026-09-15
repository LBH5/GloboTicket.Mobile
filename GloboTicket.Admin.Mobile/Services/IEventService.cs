using GloboTicket.Admin.Mobile.Model;

namespace GloboTicket.Admin.Mobile.Services;


public interface IEventService
{
    Task<List<EventModel>> GetAllEventsAsync();
    Task<EventModel?> GetEventAsync(Guid id);
    Task<bool> AddEventAsync(EventModel eventModel);
    Task<bool> UpdateEventAsync(EventModel eventModel);
    Task<bool> UpdateEventStatusAsync(Guid id, EventStatusModel status);
    Task<bool> DeleteEventAsync(Guid id);
}

