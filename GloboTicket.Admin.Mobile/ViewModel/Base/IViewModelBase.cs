using CommunityToolkit.Mvvm.Input;

namespace GloboTicket.Admin.Mobile.ViewModel.Base;

public interface IViewModelBase
{
    IAsyncRelayCommand InitializeAsyncCommand { get; }
}