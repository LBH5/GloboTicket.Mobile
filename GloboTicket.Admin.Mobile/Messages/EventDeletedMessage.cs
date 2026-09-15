namespace GloboTicket.Admin.Mobile.Messages;

public class EventDeletedMessage(Guid eventId)
{
    public Guid EventId { get; } = eventId;
}

