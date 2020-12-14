
using CashTracker.Models;
using CashTracker.ViewModels;
using BubblePopup = Forms9Patch.BubblePopup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System;

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
            InitializeBubblePopup();
        }

        private void InitializeBubblePopup()
        {
            var jobTemplate = new DataTemplate(() =>
            {
                var stack = new StackLayout();

                var nameLabel = new Label { FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center };
                var employerLabel = new Label { HorizontalOptions = LayoutOptions.Center };

                nameLabel.SetBinding(Label.TextProperty, "Name");
                nameLabel.SetDynamicResource(Label.TextColorProperty, "ButtonTextColor");
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
            _jobsListView.SetDynamicResource(VisualElement.BackgroundColorProperty, "SecondaryColor");

            _jobsPopup = new BubblePopup(JobName)
            {
                Content = _jobsListView,
                HeightRequest = 300,
                WidthRequest = 200,
                IsAnimationEnabled = true,
                PointerDirection = Forms9Patch.PointerDirection.Up,
                Padding = 2
            };

            _jobsPopup.Popped += CancelChangeJob;
        }

        private async void ChooseJobButton_Clicked(object sender, System.EventArgs e)
        {
            _viewModel.IsBusy = true;
            _viewModel.RefreshCanExecutes();
            await _jobsPopup.PushAsync();
        }

        private void CancelChangeJob(object sender, Forms9Patch.PopupPoppedEventArgs e)
        {
            _viewModel.IsBusy = false;
            _viewModel.RefreshCanExecutes();
            //TODO: Could the viewmodel subscribe to the OnPropertyChanged event and just refreshCanExecutes no matter what property changes?
        }

        private async Task JobSelectedAsync(SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is Job newJob)
            {
                _viewModel.ActiveJob = newJob;
                await _jobsPopup.PopAsync();
                _viewModel.IsBusy = false;
            }
        }
    }
}