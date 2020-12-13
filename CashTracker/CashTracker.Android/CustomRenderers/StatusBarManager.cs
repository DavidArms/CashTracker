using Android.OS;
using CashTracker.Droid.CustomRenderers;
using CashTracker.Styles;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(StatusBarManager))]
namespace CashTracker.Droid.CustomRenderers
{
    public class StatusBarManager : IStatusBarStyleManager
    {
        public void SetColor(Color color)
        {
            // The SetStatusBarcolor is new since API 21
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var androidColor = color.AddLuminosity(-0.1).ToAndroid();
                //Use the plugin
                CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(androidColor);
            }
        }

        public StatusBarManager()
        { }
    }
}