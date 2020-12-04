
using CashTracker.Models;
using CashTracker.ViewModels;
using BubblePopup = Forms9Patch.BubblePopup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace CashTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStatPage : ContentPage
    {
        //TODO: Is there a better way to access the viewmodel in a shell app?
        // I think this will be safe because the BindingContext is created by the xaml which should be
        // rendered by InitializeComponent which means this should never hit a null reference exception.
        private AddStatViewModel _viewModel => (AddStatViewModel)BindingContext;
        private BubblePopup _jobsPopup;
        private ListView _jobsListView;

        public AddStatPage()
        {
            InitializeComponent();

            var jobTemplate = new DataTemplate(() =>
            {
                var stack = new StackLayout();

                var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
                var employerLabel = new Label();

                nameLabel.SetBinding(Label.TextProperty, "Name");
                employerLabel.SetBinding(Label.TextProperty, "Employer");

                stack.Children.Add(nameLabel);
                stack.Children.Add(employerLabel);

                return new ViewCell { View = stack };
            });
            _jobsListView = new ListView
            {
                ItemsSource = _viewModel.AllJobs,
                ItemTemplate = jobTemplate,
                // These size requests are required to squeeze this content inside of the bubble popup. Otherwise the popup's pointer may break
                WidthRequest = 50, 
                HeightRequest = 100
            };
            _jobsListView.ItemSelected += (async (object sender, SelectedItemChangedEventArgs args) => await JobSelectedAsync(args));

            _jobsPopup = new BubblePopup(JobName)
            {
                Content = _jobsListView,
                HeightRequest = 300,
                WidthRequest = 200,
                IsAnimationEnabled = true,
                PointerDirection = Forms9Patch.PointerDirection.Up,
            };
        }

        private async void ChooseJobButton_Clicked(object sender, System.EventArgs e)
        {
            await _jobsPopup.PushAsync();
        }

        private async Task JobSelectedAsync(SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is Job newJob)
            {
                _viewModel.ActiveJob = newJob;
                await _jobsPopup.PopAsync();
            }
        }
    }
}