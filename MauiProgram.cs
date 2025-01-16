using FineTrack.Database;
using FineTrack.Services;
using Microsoft.Extensions.Logging;
using Blazorise;
using Blazorise.Bootstrap5;
using ChartJs.Blazor;
using MudBlazor;
using MudBlazor.Services;

namespace FineTrack
{
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

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton<ApplicationDbContext>();
            builder.Services.AddScoped<BalanceService>();
            builder.Services.AddSingleton<UserSessionService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
