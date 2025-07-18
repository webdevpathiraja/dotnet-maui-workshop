using Microsoft.Extensions.Logging;
using MonkeyFinder.Services;
using MonkeyFinder.View;
using MonkeyFinder.ViewModel;

namespace MonkeyFinder;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });
#if DEBUG
        builder.Logging.AddDebug();
#endif
        //builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        //builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        //builder.Services.AddSingleton<IMap>(Map.Default);

        // Services
        builder.Services.AddSingleton<MonkeyService>();

        // ViewModels
        builder.Services.AddSingleton<MonkeysViewModel>();
        builder.Services.AddTransient<MonkeyDetailsViewModel>();

        // Views
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<DetailsPage>();

        return builder.Build();
    }
}