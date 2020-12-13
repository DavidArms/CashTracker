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

        /// <summary>
        /// Struct which is meant to help reveal/visualize the primary elements of a <see cref="Theme"/>
        /// </summary>
        public readonly struct ThemePreview
        {
            /// <summary>
            /// The name of the theme
            /// </summary>
            public string Name { get;}

            /// <summary>
            /// The primary color for the theme as a <see cref="SolidColorBrush"/>
            /// </summary>
            public SolidColorBrush PrimaryColor { get; }

            /// <summary>
            /// The secondary color for the theme as a <see cref="SolidColorBrush"/>
            /// </summary>
            public SolidColorBrush SecondaryColor { get; }

            public ThemePreview(Theme themeToLoad)
            {
                var themeDictionary = AppTheme.GetResourceDictionaryForTheme(themeToLoad);

                Name = themeToLoad.ToString();

                // For now, our only use case for this struct requires the colors as SolidColorBrushes, so we must convert below.
                var primaryColorType = (Color)themeDictionary[AppTheme.PRIMARY_COLOR_KEY];
                PrimaryColor = new SolidColorBrush(primaryColorType);
                var secondaryColorType = (Color)themeDictionary[AppTheme.SECONDARY_COLOR_KEY];
                SecondaryColor = new SolidColorBrush(secondaryColorType);
            }
        }
    }
}
