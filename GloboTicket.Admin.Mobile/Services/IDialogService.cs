namespace  GloboTicket.Admin.Mobile.Services;


public interface IDialogService
{
    Task ShowAlertAsync(string title, string message, string buttonText = "OK");
    Task<bool> ShowConfirmationAsync(string title, string message, string trueButtonText = "Yes", string falseButtonText = "No");
}

