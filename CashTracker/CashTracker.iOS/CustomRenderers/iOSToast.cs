using CashTracker.Controls;
using CashTracker.iOS.CustomRenderers;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(iOSToast))]
namespace CashTracker.iOS.CustomRenderers
{
    /// <inheritdoc/>
    public class iOSToast : IMessage
    {
        const double LONG_DELAY = 3.5;
        const double SHORT_DELAY = 2.0;

        NSTimer alertDelay;
        UIAlertController alert;

        /// <inheritdoc/>
        public void LongAlert(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }

        /// <inheritdoc/>
        public void ShortAlert(string message)
        {
            ShowAlert(message, SHORT_DELAY);
        }

        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                DismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void DismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}