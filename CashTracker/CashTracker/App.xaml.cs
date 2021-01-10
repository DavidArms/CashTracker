using CashTracker.Styles;
using Xamarin.Forms;

namespace CashTracker
{
    public partial class App : Application
    {
        /// <summary>
        /// The app's Ioc container 
        /// </summary>
        public static AppContainer Container { get; } = new AppContainer();

        public App()
        {
            //Device.SetFlags(new string[] { "Shapes_Experimental", "SwipeView_Experimental" });

            InitializeComponent();
            DependencyService.Register<IStatusBarStyleManager>();

            MainPage = new AppShell();
            AppTheme.LoadDefault();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
