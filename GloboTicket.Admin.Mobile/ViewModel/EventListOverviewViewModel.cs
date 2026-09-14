using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GloboTicket.Admin.Mobile.Model;
using GloboTicket.Admin.Mobile.Services;
using GloboTicket.Admin.Mobile.ViewModel.Base;

namespace GloboTicket.Admin.Mobile.ViewModel;

public partial class EventListOverviewViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IEventService _eventService;

    [ObservableProperty]
    private ObservableCollection<EventListItemViewModel> _events = [];

    [ObservableProperty]
    private EventListItemViewModel? _selectedEvent;

    [RelayCommand]
    private async Task NavigateToSelectedDetail()
    {
        if (SelectedEvent is not null)
        {
            await _navigationService.NavigateToEventDetailPageAsync(SelectedEvent.Id);
            SelectedEvent = null;
        }
    }

    [RelayCommand]
    private async Task NavigateToAddEvent()
    {
        await _navigationService.NavigateToAddEventPageAsync();
    }

    public EventListOverviewViewModel(IEventService eventService, INavigationService navigationService)
    {
        _eventService = eventService;
        _navigationService = navigationService;
    }  

    public override async Task LoadAsync()
    {
        if(Events.Count == 0)
        {
            await Loading(LoadEventsAsync);
        }
    }

    private async Task LoadEventsAsync()
    {
        var events = await _eventService.GetAllEventsAsync();
        List<EventListItemViewModel> eventListItems = [];
        foreach (var @event in events)
        {
            eventListItems.Add(MapEventToEventListItemViewModel(@event));
        }
        Events.Clear();
        Events = eventListItems.ToObservableCollection();
    }

    private EventListItemViewModel MapEventToEventListItemViewModel(EventModel @event)
    {
        var category = new CategoryViewModel
        {
            Id = @event.Category.Id,
            Name = @event.Category.Name,
            Description = @event.Category.Description
        };
        return new EventListItemViewModel(
            @event.Id, 
            @event.Name, 
            @event.Price, 
            @event.ImageUrl, 
            (EventStatusEnum)@event.Status, 
            @event.Date, 
            @event.Artists, 
            @event.Description, 
            category);
    }
}