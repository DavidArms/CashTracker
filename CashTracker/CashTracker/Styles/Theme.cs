using Xamarin.Forms;

namespace CashTracker.Styles
{
    /// <summary>
    /// Enum representing the color theme for the app
    /// </summary>
    public enum Theme
    {
        Light,
        Dark,
        Trippy,
        Bumblebee
    }

    public class AppTheme
    {
        public static void Set(Theme selectedTheme)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (selectedTheme)
                {
                    case Theme.Light:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                    case Theme.Dark:
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case Theme.Trippy:
                        mergedDictionaries.Add(new TrippyTheme());
                        break;
                    case Theme.Bumblebee:
                        mergedDictionaries.Add(new BumblebeeTheme());
                        break;
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }

            //TODO: Use Xamarin Essentials Preferences to save/load selected theme
        }
    }
}
