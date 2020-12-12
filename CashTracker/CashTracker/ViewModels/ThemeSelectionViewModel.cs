using CashTracker.Styles;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CashTracker.ViewModels
{
    /// <summary>
    /// View model for the <see cref="Views/ThemeSelectionPage"/>
    /// </summary>
    public class ThemeSelectionViewModel : BaseViewModel
    {
        private List<ThemePreview> _availableThemes;

        /// <summary>
        /// The collection of themes that can be applied across the apps
        /// </summary>
        public List<ThemePreview> AvailableThemes
        {
            get => _availableThemes;
            set => SetProperty(ref _availableThemes, value);
        }

        /// <summary>
        /// Command fired when one of the <see cref="AvailableThemes"/> is selected
        /// </summary>
        public ICommand ThemeSelectedCommand { get; set; }

        public ThemeSelectionViewModel()
        {
            AvailableThemes = ((Theme[])Enum.GetValues(typeof(Theme)))
                                            .Select(theme => new ThemePreview(theme))
                                            .ToList();

            ThemeSelectedCommand = new Command((selectedTheme) =>
            {
                var themeName = ((ThemePreview)selectedTheme).Name;

                if (Enum.TryParse(themeName, out Theme newTheme))
                    AppTheme.Set(newTheme);
            });
        }

        public class ThemePreview
        {
            public string Name { get; set; }
            public SolidColorBrush PrimaryColor { get; set; }
            public SolidColorBrush SecondaryColor { get; set; }

            public ThemePreview(Theme themeToLoad)
            {
                ResourceDictionary theme;
                switch (themeToLoad)
                {
                    case Theme.Light:
                        theme = new LightTheme();
                        break;
                    case Theme.Dark:
                        theme = new DarkTheme();
                        break;
                    case Theme.Trippy:
                        theme = new TrippyTheme();
                        break;
                    case Theme.Bumblebee:
                        theme = new BumblebeeTheme();
                        break;
                    default:
                        theme = new LightTheme();
                        break;
                }

                Name = themeToLoad.ToString();
                var primaryColorType = (Color)theme["PrimaryColor"];
                PrimaryColor = new SolidColorBrush(primaryColorType);
                var secondaryColorType = (Color)theme["SecondaryColor"];
                SecondaryColor = new SolidColorBrush(secondaryColorType);
            }
        }
    }
}
