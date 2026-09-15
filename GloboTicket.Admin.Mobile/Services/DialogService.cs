namespace  GloboTicket.Admin.Mobile.Services;

public class DialogService : IDialogService
{
    public async Task ShowAlertAsync(string title, string message, string buttonText = "OK")
        => await Shell.Current.DisplayAlertAsync(title, message, buttonText);

    public async Task<bool> ShowConfirmationAsync(string title, string message, string trueButtonText = "Yes", string falseButtonText = "No")
        => await Shell.Current.DisplayAlertAsync(title, message, trueButtonText, falseButtonText);
    
}

