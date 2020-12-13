using CashTracker.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace CashTracker
{
    public partial class AppShell : Shell
    {
        /// <summary>
        /// Command for reacting to the AddJob button being tapped
        /// </summary>
        public ICommand AddJobCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("AddJobPage");
            Shell.Current.FlyoutIsPresented = false;
        });

        /// <summary>
        /// Command for reacting to the ChangeTheme button being tapped
        /// </summary>
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
