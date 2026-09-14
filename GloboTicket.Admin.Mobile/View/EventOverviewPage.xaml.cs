using GloboTicket.Admin.Mobile.ViewModel;

namespace GloboTicket.Admin.Mobile.View;

public partial class EventOverviewPage : ContentPageBase
{
    public EventOverviewPage(EventListOverviewViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}