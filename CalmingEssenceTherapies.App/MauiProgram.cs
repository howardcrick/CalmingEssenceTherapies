using CalmingEssenceTherapies.App.Services;
using CalmingEssenceTherapies.App.Services.Abstractions;
using CalmingEssenceTherapies.App.ViewModels;
using CalmingEssenceTherapies.App.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace CalmingEssenceTherapies.App
{
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
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IFilePickerService, FilePickerService>();
            builder.Services.AddTransient<TreatmentDetailsView>();
            builder.Services.AddTransient<TreatmentDetailsViewModel>();
            builder.Services.AddTransient<AddTreatmentView>();
            builder.Services.AddTransient<AddTreatmentViewModel>();


            builder.Services.AddHttpClient<ITreatmentService, TreatmentService>(client =>
            {
                client.BaseAddress = new Uri(Constants.RestUrl);
            }).ConfigurePrimaryHttpMessageHandler(HttpClientHandler);

            builder.Services.AddHttpClient<ICategoryService, CategoryService>(client =>
            {
                client.BaseAddress = new Uri(Constants.RestUrl);
            }).ConfigurePrimaryHttpMessageHandler(HttpClientHandler); ;

            return builder.Build();
        }

        static HttpMessageHandler HttpClientHandler()
        {
            var handler = new HttpClientHandler();

#if DEBUG
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
#endif

            return handler;
        }
    }
}
