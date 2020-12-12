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
        private List<string> _availableThemes;

        /// <summary>
        /// The string names of themes that can be applied across the apps
        /// </summary>
        public List<string> AvailableThemes
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
            AvailableThemes = Enum.GetNames(typeof(Theme)).ToList();
            ThemeSelectedCommand = new Command((object selectedTheme) =>
            {
                if (Enum.TryParse(selectedTheme.ToString(), out Theme newTheme))
                    AppTheme.Set(newTheme);
            });
        }
    }
}
