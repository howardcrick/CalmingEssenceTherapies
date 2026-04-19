using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace CalmingEssenceTherapies.App.Helpers
{
    public static class ToastHelper
    {
        public static async Task ShowToast(string message, ToastDuration duration = ToastDuration.Short, double fontSize = 14, CancellationToken cancellationToken = default)
        {
            var toast = Toast.Make(message, duration, fontSize);

            await toast.Show(cancellationToken);
        }
    }
}
