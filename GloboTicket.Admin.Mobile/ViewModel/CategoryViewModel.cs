using CommunityToolkit.Mvvm.ComponentModel;

namespace GloboTicket.Admin.Mobile.ViewModel;

public partial class CategoryViewModel: ObservableObject
{
    [ObservableProperty]
    private Guid id;

    [ObservableProperty]
    private string? name;
    [ObservableProperty]
    private string? description;

}