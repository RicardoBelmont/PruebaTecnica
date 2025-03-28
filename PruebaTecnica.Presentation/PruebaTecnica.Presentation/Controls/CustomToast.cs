using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace PruebaTecnica.Presentation.Controls
{
    public static class CustomToast
    {
        /// <summary>
        /// Display a toast
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="duration">Time in ms</param>
        public static async Task DisplayToast(string text, ToastDuration duration = ToastDuration.Long)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }

    }
}
