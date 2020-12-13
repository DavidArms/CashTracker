using CashTracker.iOS.CustomRenderers;
using CashTracker.Styles;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(StatusBarManager))]
namespace CashTracker.iOS.CustomRenderers
{
    public class StatusBarManager : IStatusBarStyleManager
    {
        public void SetColor(Color color)
        {
            UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if (statusBar != null && 
                statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                statusBar.BackgroundColor = color.ToUIColor();
            }
        }
    }
}