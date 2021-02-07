using Android.App;
using Android.Widget;
using CashTracker.Controls;
using CashTracker.Droid.CustomRenderers;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidToast))]
namespace CashTracker.Droid.CustomRenderers
{
    /// <inheritdoc/>
    public class AndroidToast : IMessage
    {
        /// <inheritdoc/>
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        /// <inheritdoc/>
        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}