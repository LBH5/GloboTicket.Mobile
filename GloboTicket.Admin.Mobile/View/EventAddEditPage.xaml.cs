using GloboTicket.Admin.Mobile.ViewModel;

namespace GloboTicket.Admin.Mobile.View;

public partial class EventAddEditPage : ContentPageBase
{
    public EventAddEditPage(EventAddEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}