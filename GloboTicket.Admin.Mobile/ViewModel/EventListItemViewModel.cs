using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using GloboTicket.Admin.Mobile.Messages;

namespace GloboTicket.Admin.Mobile.ViewModel;

public partial class EventListItemViewModel : ObservableObject, IRecipient<StatusChangeMessage>
{
    [ObservableProperty]
    private Guid _id;
    [ObservableProperty]
    private string _name = null!;
    [ObservableProperty]
    private double _price;
    [ObservableProperty]
    private string? _imageUrl;

    [ObservableProperty]
    private EventStatusEnum _eventStatus;
    [ObservableProperty]
    private DateTime _date;
    [ObservableProperty]
    private List<string> _artists = [];
    [ObservableProperty]
    private string _description = null!;
    [ObservableProperty]
    private CategoryViewModel? _category;
    

    public EventListItemViewModel(Guid id, string name, double price, string? imageUrl, EventStatusEnum eventStatus, DateTime date, List<string> artists, string description, CategoryViewModel? category)
    {
        Id = id;
        Name = name;
        Price = price;
        ImageUrl = imageUrl;
        EventStatus = eventStatus;
        Date = date;
        Artists = artists;
        Description = description;
        Category = category;

        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(StatusChangeMessage message)
    {
        if (message.EventId == Id)
        {
            EventStatus = message.Status;
        }
    }
}