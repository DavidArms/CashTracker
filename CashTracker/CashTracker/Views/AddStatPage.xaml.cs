using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CashTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStatPage : ContentPage
    {
        public AddStatPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //TODO: Make sure this doesn't get called twice (once via constructor and once here)
            //if (_viewModel.AllJobs.IsCompleted)
            //    _viewModel.AllJobs = new NotifyTaskCompletion<List<Job>>(_viewModel.LoadAllJobsAsync());
        }
    }
}