using CashTracker.Models;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Xaml;

namespace CashTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobsPopup : PopupPage
    {
        private IPopupNavigation _popupNavigation = App.Container.Resolve<IPopupNavigation>();
        private bool _closeOnSelection = false;

        private List<Job> _jobs;
        public List<Job> Jobs
        {
            get => _jobs;
            set 
            { 
                _jobs = value;
                OnPropertyChanged(nameof(Jobs));
            }
        }

        private Job _selectedJob;

        public Job SelectedJob
        {
            get => _selectedJob;
            set 
            { 
                _selectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public JobsPopup(List<Job> jobs, Job? selectedJob = null)
        {
            InitializeComponent();
            BindingContext = this;
            Jobs = jobs;

            if (selectedJob != null && Jobs.Any(job => job.ID == selectedJob.ID))
                SelectedJob = jobs.Single(job => job.ID == selectedJob.ID);

            // Now that we've selected the default job, we can automatically close on the next job selection
            _closeOnSelection = true;
        }

        private async void JobCollection_SelectionChanged(object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (!(e.CurrentSelection.FirstOrDefault() is Job selectedJob))
                return;

            SelectedJob = selectedJob;

            if (_closeOnSelection)
                await _popupNavigation.PopAsync();
        }
    }
}