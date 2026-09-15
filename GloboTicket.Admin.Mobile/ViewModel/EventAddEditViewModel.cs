using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GloboTicket.Admin.Mobile.Messages;
using GloboTicket.Admin.Mobile.Model;
using GloboTicket.Admin.Mobile.Services;
using GloboTicket.Admin.Mobile.ViewModel.Base;

namespace GloboTicket.Admin.Mobile.ViewModel;




public partial class EventAddEditViewModel : ViewModelBase, IQueryAttributable
{
    private readonly IEventService _eventService;
    private readonly ICategoryService _categoryService;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    public EventModel? EventDetail;

    [ObservableProperty]
    private string _pageTitle = null!;

    [ObservableProperty]
    private Guid _id;

    [ObservableProperty]
    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    [NotifyDataErrorInfo]
    private string _name = null!;

    [ObservableProperty]
    //[Range(25,125)]
    [CustomValidation(typeof(EventAddEditViewModel), nameof(ValidatePrice))]
    [NotifyDataErrorInfo]
    private double _price;

    public static ValidationResult? ValidatePrice(double price, ValidationContext context)
    {
        if (price is < 25 or > 125)
        {
            return new ("Price must be between 25 and 125.");
        }
        return ValidationResult.Success;
    }

    [ObservableProperty]
    private string _imageUrl = null!;

    [ObservableProperty]
    [Required]
    [NotifyDataErrorInfo]
    private EventStatusEnum _eventStatus;

    [ObservableProperty]
    [Required]
    [NotifyDataErrorInfo]
    private DateTime _date = DateTime.Now;

    [ObservableProperty]
    [MaxLength(250)]
    [NotifyDataErrorInfo]
    private string _description = null!;

    [ObservableProperty]
    [Required]
    [NotifyDataErrorInfo]
    private CategoryModel? _category = new();

    public ObservableCollection<string> Artists { get; set; } = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddArtistCommand))]
    private string _addedArtist = null!;

    [ObservableProperty]
    private DateTime _minDate = DateTime.Now;

    public List<EventStatusEnum> StatusList { get; set; } =
        [.. Enum.GetValues<EventStatusEnum>()];

    public ObservableCollection<CategoryModel> Categories { get; set; } = [];
    public ObservableCollection<ValidationResult> Errors { get; } = [];

    [RelayCommand(CanExecute = nameof(CanAddArtist))]
    private void AddArtist()
    {

        Artists.Add(AddedArtist);
        AddedArtist = string.Empty;
    }

    private bool CanAddArtist() => !string.IsNullOrWhiteSpace(AddedArtist);

    [RelayCommand(CanExecute = nameof(CanSubmitEvent))]
    private async Task SubmitEvent()
    {
        ValidateAllProperties();
        if (Errors.Any())
        {
            return;
        }

        if (Id == Guid.Empty)
        {
            EventModel model = MapDataToEventModel();
            if(await _eventService.AddEventAsync(model))
            {
                WeakReferenceMessenger.Default.Send(new EventAddedOrChangedMessage());
                await _dialogService.ShowAlertAsync("Success", "Event added successfully.");
                await _navigationService.NavigateToOverviewPageAsync();
            }
            else
            {
                await _dialogService.ShowAlertAsync("Error", "Failed to add event. Please try again.");
            }
        }
        else
        {
            EventModel model = MapDataToEventModel();
            if(await _eventService.UpdateEventAsync(model))
            {
                WeakReferenceMessenger.Default.Send(new EventAddedOrChangedMessage());
                await _dialogService.ShowAlertAsync("Success", "Event updated successfully.");
                await _navigationService.GoBackAsync();
            }
            else
            {
                await _dialogService.ShowAlertAsync("Error", "Failed to update event. Please try again.");
            }
        }
    }
    private bool CanSubmitEvent() => !HasErrors && !IsBusy;

    private EventModel MapDataToEventModel()
    {
        return new EventModel
        {
            Id = Id,
            Name = Name,
            Price = Price,
            ImageUrl = ImageUrl,
            Status = (EventStatusModel)EventStatus,
            Date = Date,
            Description = Description,
            Category = Category!,
            Artists = [.. Artists]
        };
    }

    public EventAddEditViewModel(
        IEventService eventService,
        ICategoryService categoryService,
        INavigationService navigationService,
        IDialogService dialogService)
    {
        _eventService = eventService;
        _categoryService = categoryService;
        _navigationService = navigationService;
        _dialogService =    dialogService;
        ErrorsChanged += AddEventViewModel_ErrorsChanged;
    }


    private void AddEventViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
    {
        Errors.Clear();
        GetErrors().ToList().ForEach(Errors.Add);
        SubmitEventCommand.NotifyCanExecuteChanged();
    }

    public override async Task LoadAsync()
    {
        await Loading(
            async () =>
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                MapCategories(categories);
                if(EventDetail is null && Id != Guid.Empty)
                {
                    EventDetail = await _eventService.GetEventAsync(Id);
                }
                MapEvent(EventDetail);
                ValidateAllProperties();
            }
        );
    }

    private void MapEvent(EventModel? eventModel)
    {
        if (eventModel is null) return;

        Id = eventModel.Id;
        Name = eventModel.Name;
        Price = eventModel.Price;
        ImageUrl = eventModel.ImageUrl!;
        EventStatus = (EventStatusEnum)eventModel.Status;
        Date = eventModel.Date;
        Description = eventModel.Description;
        Category = Categories.FirstOrDefault(c => c.Id == eventModel.Category.Id && c.Name == eventModel.Category.Name && c.Description == eventModel.Category.Description);
        Artists.Clear();
        foreach (var artist in eventModel.Artists)
        {
            Artists.Add(artist);
        }
    }

    private void MapCategories(List<CategoryModel> categories)
    {
        Categories.Clear();
        foreach (var category in categories)
        {
            Categories.Add(new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            });
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.Count > 0)
        {
            EventDetail = query["Event"] as EventModel;
        }
    }
}