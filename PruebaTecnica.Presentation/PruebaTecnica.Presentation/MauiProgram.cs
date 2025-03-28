using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using PruebaTecnica.Presentation.NativeServices.Biometric;
using PruebaTecnica.Presentation.Navigation;

namespace PruebaTecnica.Presentation;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.UseFFImageLoading()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        IPublicClientApplication PCA = PublicClientApplicationBuilder.Create("59585e93-e555-4d91-8a32-864b6ae6334b")
            .WithRedirectUri($"msal59585e93-e555-4d91-8a32-864b6ae6334b://auth")
            .WithAuthority(AzureCloudInstance.AzurePublic, "45d754b7-0e40-4102-84ac-fb9217666027")
            .Build();

        // Application Registry 
        new Application.Startup().RegisterServices().ForEach(e => builder.Services.AddSingleton(e.Item1, e.Item2));

        // Utilities
        builder.Services.AddSingleton<BiometricHelper>();
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton(PCA);

        //Views
        builder.Services.AddSingleton<Views.LoginView>();

		//ViewModels
		builder.Services.AddSingleton<ViewModel.LoginViewModel>();


        return builder.Build();
	}
}
