using GloboTicket.Admin.Mobile.Model;
using GloboTicket.Admin.Mobile.Repositories;

namespace  GloboTicket.Admin.Mobile.Services;


public class EventService(IEventRepository eventRepository) : IEventService
{
    public async Task<List<EventModel>> GetAllEventsAsync() =>
     await eventRepository.GetAllEventsAsync();

    public async Task<EventModel?> GetEventAsync(Guid id) =>
     await eventRepository.GetEventAsync(id);

    public async Task<bool> AddEventAsync(EventModel eventModel) =>
     await eventRepository.AddEventAsync(eventModel);

    public async Task<bool> UpdateEventAsync(EventModel eventModel) =>
     await eventRepository.UpdateEventAsync(eventModel);
    

    public async Task<bool> UpdateEventStatusAsync(Guid id, EventStatusModel status) =>
     await eventRepository.UpdateEventStatusAsync(id, status);

    public async Task<bool> DeleteEventAsync(Guid id)=>
     await eventRepository.DeleteEventAsync(id);

}

