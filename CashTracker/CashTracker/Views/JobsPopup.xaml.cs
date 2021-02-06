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
        /// <summary>
        /// The collection of jobs to show
        /// </summary>
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
        /// <summary>
        /// The currently selected job in the collection
        /// </summary>
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

            if (selectedJob != null && jobs.Any(job => job.ID == selectedJob.ID))
            {
                SelectedJob = jobs.Single(job => job.ID == selectedJob.ID);

                // Ensure the selected job is at the top of the list for convenience
                jobs.Remove(SelectedJob);
                jobs.Insert(0, SelectedJob);
            }

            Jobs = jobs;

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