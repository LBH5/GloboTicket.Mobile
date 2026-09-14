using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GloboTicket.Admin.Mobile.Model;
using GloboTicket.Admin.Mobile.Services;
using GloboTicket.Admin.Mobile.ViewModel.Base;

namespace GloboTicket.Admin.Mobile.ViewModel;




public partial class EventAddEditViewModel(
    IEventService eventService, 
    ICategoryService categoryService,
    INavigationService navigationService) : ViewModelBase
{
    private readonly IEventService _eventService = eventService;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly INavigationService _navigationService = navigationService;

    public EventModel? eventDetail;

    [ObservableProperty]
    private string _pageTitle = default!;

    [ObservableProperty]
    private Guid _id = default!;

    [ObservableProperty]
    private string _name = default!;

    [ObservableProperty]
    private double _price = default!;

    [ObservableProperty]
    private string _imageUrl = null!;

    [ObservableProperty]
    private EventStatusEnum _eventStatus = default!;

    [ObservableProperty]
    private DateTime _date = DateTime.Now;

    [ObservableProperty]
    private string _description = null!;

    [ObservableProperty]
    private CategoryModel? _category = new();

    public ObservableCollection<string> Artists { get; set; } = [];

    [ObservableProperty]
    private string _addedArtist = default!;

    [ObservableProperty]
    private DateTime _minDate = DateTime.Now;

    public List<EventStatusEnum> StatusList { get; set; } =
        [.. Enum.GetValues<EventStatusEnum>()];

    public ObservableCollection<CategoryModel> Categories { get; set; } = [];

    [RelayCommand(CanExecute = nameof(canAddArtist))]
    private async Task AddArtist()
    {
        Artists.Add(AddedArtist);
        AddedArtist = string.Empty;
    }
    
    private bool canAddArtist() => !string.IsNullOrWhiteSpace(AddedArtist);

    [RelayCommand(CanExecute = nameof(canSubmitEvent))]
    private async Task SubmitEvent()
    {
        var eventModel = new EventModel
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

        if (Id == Guid.Empty)
        {
            await _eventService.AddEventAsync(eventModel);
        }
        else
        {
            await _eventService.UpdateEventAsync(eventModel);
        }
        await _navigationService.NavigateToEventDetailPageAsync(eventModel.Id);
    }
    private bool canSubmitEvent() => !string.IsNullOrWhiteSpace(Name) && Price > 0 && !string.IsNullOrWhiteSpace(ImageUrl) && !string.IsNullOrWhiteSpace(Description);
    public override async Task LoadAsync()
    {
        await Loading(
            async () =>
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                MapCategories(categories);
                if(eventDetail is null && Id != Guid.Empty)
                {
                    eventDetail = await _eventService.GetEventAsync(Id);
                }
                MapEvent(eventDetail);
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
        Category = eventModel.Category;
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
            Categories.Add(category);
        }
    }
}