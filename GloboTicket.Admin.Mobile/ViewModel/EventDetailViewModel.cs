using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GloboTicket.Admin.Mobile.Messages;
using GloboTicket.Admin.Mobile.Model;
using GloboTicket.Admin.Mobile.Services;
using GloboTicket.Admin.Mobile.ViewModel.Base;

namespace GloboTicket.Admin.Mobile.ViewModel;


public partial class EventDetailViewModel(
    IEventService eventService,
    INavigationService navigationService,
    IDialogService dialogService) : ViewModelBase, IQueryAttributable
{


    [ObservableProperty]
    private Guid _id;

    [ObservableProperty]
    private string _name = null!;

    [ObservableProperty]
    private string _description = null!;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CancelEventCommand))]
    private DateTime _date;

    [ObservableProperty]
    private double _price;

    [ObservableProperty]
    private CategoryViewModel _category = null!;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CancelEventCommand))]
    private EventStatusEnum _eventStatus ;

    [ObservableProperty]
    private string _imageUrl = null!;

    [ObservableProperty]
    private ObservableCollection<string> _artists = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowThumbnailImage))]
    private bool _showLargeImage;
    public bool ShowThumbnailImage => !ShowLargeImage;

    [RelayCommand]
    private void ToggleImageSize()
    {
        ShowLargeImage = !ShowLargeImage;
    }

    [RelayCommand(CanExecute = nameof(CanCancelEvent))]
    private async Task CancelEvent()
    {
        if (await eventService.UpdateEventStatusAsync(Id, EventStatusModel.Canceled))
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
        await navigationService.NavigateToEditEventPageAsync(detailModel);
    }

    [RelayCommand]
    private async Task DeleteEvent()
    {
        var confirm = await dialogService.ShowConfirmationAsync("Confirm Delete", "Are you sure you want to delete this event?");
        if (confirm)
        {
            if (await eventService.DeleteEventAsync(Id))
            {
                WeakReferenceMessenger.Default.Send(new EventDeletedMessage(Id));
                await navigationService.NavigateToOverviewPageAsync();
            }
            else
            {
                await  dialogService.ShowAlertAsync("Error", "Failed to delete event. Please try again.");
            }
        }
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
        var @event = await eventService.GetEventAsync(eventId);
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
            ImageUrl = viewModel.ImageUrl,
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
