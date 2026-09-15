using System.Collections.ObjectModel;
using GloboTicket.Admin.Mobile.Services;
using GloboTicket.Admin.Mobile.ViewModel;
using NSubstitute;

namespace GloboTicket.Admin.Mobile.Tests.ViewModels;

public class EventDetailViewModelTests
{
    [Fact]
    public void EventDetailViewModel_Properties_AreSetCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Test Event";
        var date = DateTime.Now;
        var price = 100.0;
        var imageUrl = "https://example.com/image.jpg";
        var eventStatus = EventStatusEnum.OnSale;
        var artists = new ObservableCollection<string> { "Artist 1", "Artist 2" };
        var description = "Test event description";
        var category = new CategoryViewModel { Id = Guid.NewGuid(), Name = "Test Category" };

        var mockEventService = Substitute.For<IEventService>();
        var mockNavigationService = Substitute.For<INavigationService>();
        var mockDialogService = Substitute.For<IDialogService>();
        // Act
        var sut = new EventDetailViewModel(mockEventService, mockNavigationService, mockDialogService)
        {
            Id = id,
            Name = name,
            Price = price,
            ImageUrl = imageUrl,
            EventStatus = eventStatus,
            Date = date,
            Artists = artists,
            Description = description,
            Category = category
        };

        // Assert
        Assert.Equal(id, sut.Id);
        Assert.Equal(name, sut.Name);
        Assert.Equal(date, sut.Date);
        Assert.Equal(price, sut.Price);
        Assert.Equal(imageUrl, sut.ImageUrl);
        Assert.Equal(eventStatus, sut.EventStatus);
        Assert.Equal(artists, sut.Artists);
        Assert.Equal(description, sut.Description);
        Assert.Equal(category, sut.Category);
    }
    [Fact]
    public async Task EventDetailWithId_IsInitialized_GetEventIsCalled()
    {
        // Arrange
        var eventId = Guid.NewGuid();

        var mockEventService = Substitute.For<IEventService>();
        var mockNavigationService = Substitute.For<INavigationService>();
        var mockDialogService = Substitute.For<IDialogService>();

        var sut = new EventDetailViewModel(mockEventService, mockNavigationService, mockDialogService)
        {
            Id = eventId
        };

        // Act
        await sut.LoadAsync();

        // Assert
        await mockEventService.Received(1).GetEventAsync(eventId);

    }

    [Fact]
    public async Task EventDetailGuidEmptyId_IsInitialized_GetEventIsNotCalled()
    {
        // Arrange
        var mockEventService = Substitute.For<IEventService>();
        var mockNavigationService = Substitute.For<INavigationService>();
        var mockDialogService = Substitute.For<IDialogService>();

        var sut = new EventDetailViewModel(mockEventService, mockNavigationService, mockDialogService)
        {
            Id = Guid.Empty
        };

        // Act
        await sut.LoadAsync();

        // Assert
        await mockEventService.DidNotReceive().GetEventAsync(Arg.Any<Guid>());
    }
    
}