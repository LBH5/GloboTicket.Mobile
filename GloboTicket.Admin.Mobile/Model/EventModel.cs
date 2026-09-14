namespace  GloboTicket.Admin.Mobile.Model;


public class EventModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public double Price { get; set; }
    public EventStatusModel Status { get; set; }
    public CategoryModel Category { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public List<string> Artists { get; set; } = new();
}

