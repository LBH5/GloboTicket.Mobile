using GloboTicket.Admin.Mobile.Model;

namespace GloboTicket.Admin.Mobile.Repositories;

public interface IEventRepository
{
    Task<List<EventModel>> GetAllEventsAsync();
    Task<EventModel?> GetEventAsync(Guid id);
    Task<bool> AddEventAsync(EventModel eventModel);
    Task<bool> UpdateEventAsync(EventModel eventModel);
    Task<bool> UpdateEventStatusAsync(Guid id, EventStatusModel status);
    
}