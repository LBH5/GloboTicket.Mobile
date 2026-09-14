namespace GloboTicket.Admin.Mobile.Model;

public class CategoryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
}