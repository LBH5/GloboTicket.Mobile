using CommunityToolkit.Mvvm.Messaging;
using GloboTicket.Admin.Mobile.Messages;
using GloboTicket.Admin.Mobile.ViewModel;

namespace  GloboTicket.Admin.Mobile.Tests.ViewModels;

public class EventListItemViewModelTests
{
    [Fact]
    public void EventListItemViewModel_Properties_AreSetCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Test Event";
        var date = DateTime.Now;
        var price = 100.0;
        var imageUrl = "https://example.com/image.jpg";
        var eventStatus = EventStatusEnum.OnSale;
        var artists = new List<string> { "Artist 1", "Artist 2" };
        var description = "Test event description";
        var category = new CategoryViewModel { Id = Guid.NewGuid(), Name = "Test Category" };

        // Act
        var sut = new EventListItemViewModel(
            id,
            name,
            price,
            imageUrl,
            eventStatus,
            date,
            artists,
            description,
            category);

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

    public static TheoryData<Guid ,string?,double,string?,EventStatusEnum,DateTime,List<string>?,string?,CategoryViewModel?> 
        Cases = new()
    {
        { Guid.NewGuid(), "Test Event", 100.0, "https://example.com/image.jpg", EventStatusEnum.OnSale, DateTime.Now,
            ["Artist 1", "Artist 2"], "Test event description", new CategoryViewModel { Id = Guid.NewGuid(), Name = "Test Category" } },
        { Guid.NewGuid(), null, 0.0, null, EventStatusEnum.OnSale, DateTime.Now, null, null, null },
        { Guid.NewGuid(), "Another Event", 50.0, "https://example.com/image2.jpg", EventStatusEnum.Canceled, DateTime.Now.AddDays(1),
            ["Artist 3"], "Another event description", new CategoryViewModel { Id = Guid.NewGuid(), Name = "Another Category" } }

    };

    [Theory, MemberData(nameof(Cases))]
    public void EventListItemViewModel_Properties_AreSetCorrectly_WithDifferentCases(
        Guid id,
        string? name,
        double price,
        string? imageUrl,
        EventStatusEnum eventStatus,
        DateTime date,
        List<string>? artists,
        string? description,
        CategoryViewModel? category)
    {
        // Act
        var sut = new EventListItemViewModel(
            id,
            name!,
            price,
            imageUrl,
            eventStatus,
            date,
            artists!,
            description!,
            category);

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
    public void EventListItem_Initialized_SubscribeToStatusChangedMessage()
    {
        // Arrange
        var sut = new EventListItemViewModel(
            Guid.NewGuid(),
            "Test Event",
            100.0,
            "https://example.com/image.jpg",
            EventStatusEnum.OnSale,
            DateTime.Now,
            ["Artist 1", "Artist 2"],
            "Test event description",
            new CategoryViewModel { Id = Guid.NewGuid(), Name = "Test Category" });

  
        // Assert
        Assert.True(WeakReferenceMessenger.Default.IsRegistered<StatusChangeMessage>(sut));
    }

    [Theory]
    [InlineData(EventStatusEnum.OnSale, EventStatusEnum.AlmostSoldOut)]
    [InlineData(EventStatusEnum.AlmostSoldOut, EventStatusEnum.Canceled)]
    [InlineData(EventStatusEnum.AlmostSoldOut, EventStatusEnum.AlmostSoldOut)]
    public void StatusChangeMessage_Received_EventStatusUpdated(
        EventStatusEnum currentStatus,
        EventStatusEnum newStatus)
    {
        // Arrange
        var sut = new EventListItemViewModel(
            Guid.NewGuid(),
            "Test Event",
            100.0,
            "https://example.com/image.jpg",
            currentStatus,
            DateTime.Now,
            ["Artist 1", "Artist 2"],
            "Test event description",
            new CategoryViewModel { Id = Guid.NewGuid(), Name = "Test Category" });
        Assert.Equal(currentStatus, sut.EventStatus);
        // Act
        WeakReferenceMessenger.Default.Send(new StatusChangeMessage(sut.Id, newStatus));

        // Assert
        Assert.Equal(newStatus, sut.EventStatus);
    }

    [Theory]
    [InlineData(EventStatusEnum.OnSale, EventStatusEnum.AlmostSoldOut)]
    [InlineData(EventStatusEnum.AlmostSoldOut, EventStatusEnum.Canceled)]
    [InlineData(EventStatusEnum.AlmostSoldOut, EventStatusEnum.AlmostSoldOut)]
    public void StatusChangeMessageForOtherId_Received_EventStatusUpdated(
        EventStatusEnum currentStatus,
        EventStatusEnum newStatus)
    {
        // Arrange
        var sut = new EventListItemViewModel(
            Guid.NewGuid(),
            "Test Event",
            100.0,
            "https://example.com/image.jpg",
            currentStatus,
            DateTime.Now,
            ["Artist 1", "Artist 2"],
            "Test event description",
            new CategoryViewModel { Id = Guid.NewGuid(), Name = "Test Category" });
        Assert.Equal(currentStatus, sut.EventStatus);
        // Act
        WeakReferenceMessenger.Default.Send(new StatusChangeMessage(Guid.NewGuid(), newStatus));

        // Assert
        Assert.Equal(currentStatus, sut.EventStatus);
    }
}

