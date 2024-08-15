using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TextoVoz.Interfaces;
using TextoVoz.Mvvm.ViewModels;
using TextoVoz.Mvvm.Views;
using TextoVoz.Repository;
using TextoVoz.Service;
using TextoVoz.Service.Helpers;

namespace TextoVoz
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>().UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterRepository()
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        public static MauiAppBuilder RegisterRepository(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<IConfiguracoesRepository, ConfiguracoesRepository>();
            mauiAppBuilder.Services.AddTransient<ITextoRepository, TextoRepository>();
            // More services registered here.

            return mauiAppBuilder;
        }
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<IConfiguracoesService, ConfiguracoesService>();
            mauiAppBuilder.Services.AddTransient<ITextoService, TextoService>();
            mauiAppBuilder.Services.AddTransient<ICustomFileTypeService, CustomFileTypeService>();
            // More services registered here.

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<ConfiguracoesViewModel>();
            mauiAppBuilder.Services.AddSingleton<TextoViewModel>();
            // More view-models registered here.

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<ConfiguracoesView>();
            mauiAppBuilder.Services.AddSingleton<HomeView>();
            mauiAppBuilder.Services.AddSingleton<TextoView>();
            // More views registered here.

            return mauiAppBuilder;
        }
    }
}
