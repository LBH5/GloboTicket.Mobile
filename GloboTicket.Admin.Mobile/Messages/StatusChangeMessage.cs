using GloboTicket.Admin.Mobile.ViewModel;

namespace GloboTicket.Admin.Mobile.Messages;


public class StatusChangeMessage(Guid eventId, EventStatusEnum status)
{
    public Guid EventId { get; } = eventId;
    public EventStatusEnum Status { get; } = status;
}