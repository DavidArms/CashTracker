using CashTracker.Styles;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CashTracker.ViewModels
{
    public class ThemeSelectionViewModel : BaseViewModel
    {
        private List<string> _availableThemes;

        public List<string> AvailableThemes
        {
            get => _availableThemes;
            set => SetProperty(ref _availableThemes, value);
        }

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
