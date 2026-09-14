
using GloboTicket.Admin.Mobile.ViewModel;

namespace GloboTicket.Admin.Mobile.View;

public partial class EventDetailPage : ContentPageBase
{
    public EventDetailPage(EventDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}