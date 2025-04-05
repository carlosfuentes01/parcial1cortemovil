using Microsoft.Extensions.Logging;
using llamada.Servicios;
using llamada.View;
using Syncfusion.Maui.Toolkit;
using Syncfusion.Maui.Toolkit.Hosting;


#if ANDROID
using llamada.Platforms.Android;

#endif
namespace llamada;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
    
#if ANDROID
        builder.Services.AddSingleton<PhoneCaller, Servicios_android>();
#endif
        builder
            .UseMauiApp<App>()
			.ConfigureSyncfusionToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<Detalle>();
        builder.Services.AddSingleton<Plantilla>();

        return builder.Build();
	}
}
