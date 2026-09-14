namespace GloboTicket.Admin.Mobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(View.EventDetailPage), typeof(View.EventDetailPage));
		Routing.RegisterRoute("event/add", typeof(View.EventAddEditPage));
		Routing.RegisterRoute("event/edit", typeof(View.EventAddEditPage));
	}
}
