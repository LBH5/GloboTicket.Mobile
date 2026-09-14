using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GloboTicket.Admin.Mobile.Messages;
using GloboTicket.Admin.Mobile.Model;
using GloboTicket.Admin.Mobile.Services;
using GloboTicket.Admin.Mobile.ViewModel.Base;

namespace GloboTicket.Admin.Mobile.ViewModel;


public partial class EventDetailViewModel : ViewModelBase, IQueryAttributable
{
    private readonly IEventService _eventService;
    private readonly INavigationService _navigationService;
    
    [ObservableProperty]
    private Guid id; 

    [ObservableProperty]
    private string name = null!;

    [ObservableProperty]
    private string description = null!;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CancelEventCommand))]
    private DateTime date;

    [ObservableProperty]
    private double price;
        
    [ObservableProperty]
    private CategoryViewModel category = null!;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CancelEventCommand))]
    private EventStatusEnum eventStatus ;

    [ObservableProperty]
    private string imageUrl = null!;
    
    [ObservableProperty]
    private ObservableCollection<string> artists = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowThumbnailImage))]
    private bool showLargeImage;
    public bool ShowThumbnailImage => !ShowLargeImage;

    [RelayCommand]
    private void ToggleImageSize()
    {
        ShowLargeImage = !ShowLargeImage;
    }

    [RelayCommand(CanExecute = nameof(CanCancelEvent))]
    private async Task CancelEvent()
    {
        if (await _eventService.UpdateEventStatusAsync(Id, EventStatusModel.Canceled))
        {
            EventStatus = EventStatusEnum.Canceled;
            WeakReferenceMessenger.Default.Send(new StatusChangeMessage(Id, EventStatus));
        }
    }

    private bool CanCancelEvent() => EventStatus != EventStatusEnum.Canceled
        && Date > DateTime.Now.AddHours(4);

    [RelayCommand]
    private async Task NavigateToEditEvent()
    {
        var detailModel = MapToEventDetailModel(this);
        await _navigationService.NavigateToEditEventPageAsync(detailModel);
    }


    public EventDetailViewModel(IEventService eventService,
        INavigationService navigationService)
    {
        _eventService = eventService;
        _navigationService = navigationService;
    }

    public override async Task LoadAsync()
    {
        await Loading(
            async () => {
                if (Id != Guid.Empty)
                {
                    await LoadEventDetailsAsync(Id);
                }   
            }
        );
    }

    private async Task LoadEventDetailsAsync(Guid eventId)
    {
        var @event = await _eventService.GetEventAsync(eventId);
        if (@event != null)
        {
            MapEventToViewModel(@event);
        }
    }

    private void MapEventToViewModel(EventModel @event)
    {
        Name = @event.Name;
        Description = @event.Description;
        Date = @event.Date;
        Price = @event.Price;
        EventStatus = (EventStatusEnum)@event.Status;
        Category = new CategoryViewModel
        {
            Id = @event.Category.Id,
            Name = @event.Category.Name,
            Description = @event.Category.Description
        };
        ImageUrl = @event.ImageUrl ?? string.Empty;
        Artists = new ObservableCollection<string>(@event.Artists);
    }

    private EventModel MapToEventDetailModel(EventDetailViewModel viewModel)
    {
        return new EventModel
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Description = viewModel.Description,
            Date = viewModel.Date,
            Price = viewModel.Price,
            Status = (EventStatusModel)viewModel.EventStatus,
            Category = new CategoryModel
            {
                Id = viewModel.Category.Id,
                Name = viewModel.Category.Name!,
                Description = viewModel.Category.Description
            },
            ImageUrl = viewModel.ImageUrl ?? string.Empty,
            Artists = [.. viewModel.Artists]
        };
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var eventId = query["EventId"].ToString();
        if (Guid.TryParse(eventId, out var parsedEventId))
        {
            Id = parsedEventId;
        }
    }
}