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

        public ICommand ToggleThemeCommand => new Command(async () =>
        {
            
        });

        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;

            Routing.RegisterRoute(nameof(AddJobPage), typeof(AddJobPage));
        }
    }
}
