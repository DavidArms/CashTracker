using System;
using Xamarin.Essentials;
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

    /// <summary>
    /// Class for interacting with the application theme
    /// </summary>
    public class AppTheme
    {
        private const string THEME_KEY = "SelectedTheme";
        private const string DEFAULT_THEME = "Light";

        /// <summary>
        /// Sets the default application theme
        /// </summary>
        public static void LoadDefault()
        {
            var themeToLoad = Preferences.Get(THEME_KEY, DEFAULT_THEME);
            if (Enum.TryParse(themeToLoad, out Theme newTheme))
                Set(newTheme);
        }

        /// <summary>
        /// Sets the <see cref="Theme"/> to use across the app
        /// </summary>
        /// <param name="selectedTheme"></param>
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

            Preferences.Set(THEME_KEY, selectedTheme.ToString());
        }
    }
}
