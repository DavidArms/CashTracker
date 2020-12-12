using CashTracker.Styles;
using CashTracker.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CashTracker
{
    public partial class AppShell : Shell
    {
        public ICommand AddJobCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("AddJobPage");
            Shell.Current.FlyoutIsPresented = false;
        });

        public ICommand SelectThemeCommand => new Command(async () =>
        {
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync("ThemeSelectionPage");
        });

        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;

            Routing.RegisterRoute(nameof(AddJobPage), typeof(AddJobPage));
            Routing.RegisterRoute(nameof(ThemeSelectionPage), typeof(ThemeSelectionPage));
        }
    }
}
