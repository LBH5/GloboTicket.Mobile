using GloboTicket.Admin.Mobile.ViewModel.Base;

namespace GloboTicket.Admin.Mobile.View;


public class ContentPageBase : ContentPage
{
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is not IViewModelBase viewModel)
        {
            return;
        } 
        await viewModel.InitializeAsyncCommand.ExecuteAsync(null);
    }
}