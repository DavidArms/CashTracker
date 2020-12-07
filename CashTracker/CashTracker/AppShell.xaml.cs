using CashTracker.Styles;
using CashTracker.Views;
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

        public ICommand ToggleThemeCommand => new Command(() =>
        {
            if (Application.Current.Resources is LightTheme)
                Application.Current.Resources = new DarkTheme();
            else
                Application.Current.Resources = new LightTheme();
        });

        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;

            Routing.RegisterRoute(nameof(AddJobPage), typeof(AddJobPage));
        }
    }
}
