using CommunityToolkit.Maui;
using GloboTicket.Admin.Mobile.Repositories;
using GloboTicket.Admin.Mobile.Services;
using GloboTicket.Admin.Mobile.View;
using GloboTicket.Admin.Mobile.ViewModel;
using Microsoft.Extensions.Logging;

namespace GloboTicket.Admin.Mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
			})
			.RegisterRepositories()
			.RegisterServices()
			.RegisterViewModels()
			.RegisterViews();



#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	private static MauiAppBuilder RegisterRepositories(this MauiAppBuilder builder)
	{

		var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
			? "http://localhost:5191"
			: "http://10.0.2.2:5191";

		builder.Services.AddHttpClient("GloboTicketAdminAPIClient", client =>
		{
			client.BaseAddress = new Uri(baseUrl);
			client.DefaultRequestHeaders.Add("Accept", "application/json");
		});

		builder.Services.AddTransient<IEventRepository, EventRepository>();
		builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

		return builder;
	}

	private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
	{

		builder.Services.AddTransient<IEventService, EventService>();
		builder.Services.AddSingleton<INavigationService, NavigationService>();
		builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();

		return builder;
	}

	private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<EventDetailViewModel>();
		builder.Services.AddSingleton<EventListOverviewViewModel>();
		builder.Services.AddTransient<EventAddEditViewModel>();


		return builder;
	}

	private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<EventDetailPage>();
		builder.Services.AddSingleton<EventOverviewPage>();
		builder.Services.AddTransient<EventAddEditPage>();
		return builder;
	}
}
