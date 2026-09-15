using System.Net.Http.Json;
using System.Text.Json;
using GloboTicket.Admin.Mobile.Model;

namespace  GloboTicket.Admin.Mobile.Repositories;


public class EventRepository(IHttpClientFactory httpClient) : IEventRepository
{


    public async Task<List<EventModel>> GetAllEventsAsync()
    {
        using var client = httpClient.CreateClient("GloboTicketAdminAPIClient");
        try
        {
            var events = await client.GetFromJsonAsync<List<EventModel>>("events",
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
            return await Task.FromResult(events ?? []);

        }
        catch (Exception)
        {
            return await Task.FromResult(new List<EventModel>());
        }
    }


    public async Task<EventModel?> GetEventAsync(Guid id)
    {
        using var client = httpClient.CreateClient("GloboTicketAdminAPIClient");
        try
        {
            var @event = await client.GetFromJsonAsync<EventModel>($"events/{id}",
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
            return await Task.FromResult(@event);

        }
        catch (Exception)
        {
            return await Task.FromResult<EventModel?>(null);
        }
    }

    public async Task<bool> AddEventAsync(EventModel eventModel)
    {
        using var client = httpClient.CreateClient("GloboTicketAdminAPIClient");
        var response = await client.PostAsJsonAsync("events", eventModel,
            new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return await Task.FromResult(response.IsSuccessStatusCode);
    }

    public async Task<bool> UpdateEventAsync(EventModel eventModel)
    {
        using var client = httpClient.CreateClient("GloboTicketAdminAPIClient");
        var response = await client.PutAsJsonAsync($"events/{eventModel.Id}", eventModel,
            new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return await Task.FromResult(response.IsSuccessStatusCode);
    }

    public async Task<bool> UpdateEventStatusAsync(Guid id, EventStatusModel status)
    {
        using var client = httpClient.CreateClient("GloboTicketAdminAPIClient");
        try
        {
            var statusJson = JsonContent.Create(status, options: new JsonSerializerOptions(JsonSerializerDefaults.Web));
            var response = await client.PatchAsync($"events/{id}/status", statusJson);
            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
        }
        catch (Exception)
        {
            return await Task.FromResult(false);
        }
        return await Task.FromResult(false);
    }

    public async Task<bool> DeleteEventAsync(Guid id)
    {
        using var client = httpClient.CreateClient("GloboTicketAdminAPIClient");
        try
        {
            var response = await client.DeleteAsync($"events/{id}");
            return await Task.FromResult(response.IsSuccessStatusCode);
        }
        catch (Exception)
        {
            return await Task.FromResult(false);
        }
    }
}


