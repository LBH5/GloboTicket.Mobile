using CommunityToolkit.Mvvm.ComponentModel;

namespace GloboTicket.Admin.Mobile.ViewModel;

public partial class CategoryViewModel: ObservableObject
{
    [ObservableProperty]
    private Guid _id;

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    public partial string? Description { get; set; }

    public bool Equals(CategoryViewModel? other)
    {
        return other is not null && Id.Equals(other.Id);
    }
}