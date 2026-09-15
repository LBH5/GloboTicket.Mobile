using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GloboTicket.Admin.Mobile.ViewModel.Base;



public partial class ViewModelBase : ObservableValidator, IViewModelBase
{
    [ObservableProperty]
    private bool _isBusy;

    public IAsyncRelayCommand InitializeAsyncCommand { get; }

    public ViewModelBase()
    {
        InitializeAsyncCommand = new AsyncRelayCommand(
            async () =>
            {
                IsBusy = true;
                await Loading(LoadAsync);
                IsBusy = false;
            }
        );
    }

    protected async Task Loading(Func<Task> unitOfWork)
    {
        await unitOfWork();
    }

    public virtual Task LoadAsync()
    {
        return Task.CompletedTask;
    }


}