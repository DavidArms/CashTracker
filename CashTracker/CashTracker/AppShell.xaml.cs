using CashTracker.Styles;
using CashTracker.Views;
using System.Linq;
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
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (CurrentTheme)
                {
                    case Theme.Light:
                        mergedDictionaries.Add(new DarkTheme());
                        CurrentTheme = Theme.Dark;
                        break;
                    case Theme.Dark:
                        mergedDictionaries.Add(new LightTheme());
                        CurrentTheme = Theme.Light;
                        break;
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        CurrentTheme = Theme.Light;
                        break;
                }
            }

        });

        public Theme CurrentTheme { get; set; } = Theme.Light;

        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;

            Routing.RegisterRoute(nameof(AddJobPage), typeof(AddJobPage));
        }
    }
}
