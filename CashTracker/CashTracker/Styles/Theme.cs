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
            mergedDictionaries.Clear();

            var theme = GetResourceDictionaryForTheme(selectedTheme);

            mergedDictionaries.Add(theme);
            Preferences.Set(THEME_KEY, selectedTheme.ToString());

            // Attempt to set the device's StatusBar color the app's primary color
            var primaryColor = (Color)theme["PrimaryColor"];
            var statusbar = DependencyService.Get<IStatusBarStyleManager>();
            statusbar.SetColor(primaryColor);
        }

        /// <summary>
        /// Returns the Resource Dictionary corresponding with the passed in theme
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public static ResourceDictionary GetResourceDictionaryForTheme(Theme theme)
        {
            switch (theme)
            {
                case Theme.Light:
                    return new LightTheme();
                case Theme.Dark:
                    return new DarkTheme();
                case Theme.Trippy:
                    return new TrippyTheme();
                case Theme.Bumblebee:
                    return new BumblebeeTheme();
                default:
                    return new LightTheme();
            }
        }
    }
}
