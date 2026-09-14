using GloboTicket.Admin.Mobile.ViewModel;

namespace GloboTicket.Admin.Mobile.Messages;


public class StatusChangeMessage
{
    public Guid EventId { get; }
    public EventStatusEnum Status { get; }

    public StatusChangeMessage(Guid eventId, EventStatusEnum status)
    {
        EventId = eventId;
        Status = status;
    }
}