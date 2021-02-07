using CashTracker.Controls;
using CashTracker.Database;
using CashTracker.Models;
using CashTracker.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CashTracker.ViewModels
{
    [QueryProperty("JobID", "JobID")]
    public class AddStatViewModel : BaseViewModel
    {
        private IAsyncRepository<IncomeStat> _statRepo = App.Container.Resolve<IAsyncRepository<IncomeStat>>();
        private IAsyncRepository<Job> _jobRepo = App.Container.Resolve<IAsyncRepository<Job>>();
        private IPopupNavigation _popupNavigation = App.Container.Resolve<IPopupNavigation>();
        private IMessage _toaster = DependencyService.Get<IMessage>();

        private double? _totalHours;
        private double? _totalMoney;

        private NotifyTaskCompletion<List<Job>> _allJobs;
        /// <summary>
        /// All of the jobs saved in the database
        /// </summary>
        /// <remarks>This is an async property. To access the value, you must use AllJobs.Result</remarks>
        public NotifyTaskCompletion<List<Job>> AllJobs
        {
            get => _allJobs;
            set => SetProperty(ref _allJobs, value);
        }

        private Job _activeJob;
        /// <summary>
        /// The currently selected job
        /// </summary>
        public Job ActiveJob
        {
            get
            {
                if (_activeJob == null)
                    ActiveJob = Task.Run(async () => await _jobRepo.FirstOrDefaultAsync()).Result;

                return _activeJob;
            }
            set
            {
                SetProperty(ref _activeJob, value);
                // Refresh the jobs list if is currently loading or if it doesn't include our new value
                if (!AllJobs.IsSuccessfullyCompleted || !AllJobs.Result.Any(job => job.ID == value.ID))
                    AllJobs = new NotifyTaskCompletion<List<Job>>(LoadAllJobsAsync());
            }
        }

        /// <summary>
        /// The ID of the Job to store in <see cref="ActiveJob"/>
        /// </summary>
        /// TODO: When Shell finally supports passing objects, then remove this and update the query property to point at ActiveJob
        public string JobID
        {
            set
            {
                if (!int.TryParse(value, out var asInt))
                    return;

                var jobToLoad = Task.Run(() => _jobRepo.FirstOrDefaultAsync(job => job.ID == asInt)).Result;
                if (jobToLoad != null)
                    ActiveJob = jobToLoad;
            }
        }

        /// <summary>
        /// The total hours worked
        /// </summary>
        public double? TotalHours
        {
            get => _totalHours;
            set => SetProperty(ref _totalHours, value, onChanged:RefreshCanExecutes);
        }

        /// <summary>
        /// The total money earned
        /// </summary>
        public double? TotalMoney
        {
            get => _totalMoney;
            set => SetProperty(ref _totalMoney, value, onChanged: RefreshCanExecutes);
        }

        /// <summary>
        /// The date that the user worked
        /// </summary>
        public DateTime DateWorked { get; set; }

        /// <summary>
        /// Page for adding statistics for a job
        /// </summary>
        public AddStatViewModel()
        {
            TotalHours = null;
            TotalMoney = null;
            DateWorked = DateTime.Today;
            SaveStat = new AsyncCommand(SaveNewStatAsync, (_) => IsNotBusy && ValidateInputs());
            DeleteCommand = new AsyncCommand(DeleteJobAsync);
            OpenJobsPopupCommand = new AsyncCommand(ShowJobsPopupAsync);

            AllJobs = new NotifyTaskCompletion<List<Job>>(LoadAllJobsAsync());
            _popupNavigation.Popping += _popupNavigation_Popping;
        }

        ~AddStatViewModel()
        {
            _popupNavigation.Popping -= _popupNavigation_Popping;
        }

        private void RefreshCanExecutes()
        {
            (SaveStat as AsyncCommand).RaiseCanExecuteChanged();
        }

        private void _popupNavigation_Popping(object sender, Rg.Plugins.Popup.Events.PopupNavigationEventArgs e)
        {
            // Check if the popping popup is a Jobs popup, and if so, update our ActiveJob

            if (!(e.Page is JobsPopup jobsPopup) || jobsPopup.SelectedJob == null)
                return;

            ActiveJob = jobsPopup.SelectedJob;
        }

        /// <summary>
        /// Shows a popup allowing the user to select from existing jobs
        /// </summary>
        public ICommand OpenJobsPopupCommand { get; }
        private async Task ShowJobsPopupAsync()
        {
            if (!AllJobs.IsSuccessfullyCompleted)
                return;

            await _popupNavigation.PushAsync(new JobsPopup(AllJobs.Result, ActiveJob));
        }

        /// <summary>
        /// Deletes the active job from the database and handles switching to the next job
        /// </summary>
        public ICommand DeleteCommand { get; }
        private async Task DeleteJobAsync()
        {
            try
            {
                await _jobRepo.RemoveAsync(ActiveJob);
            }
            catch (Exception ex)
            {
                _toaster.LongAlert($"Unable To Delete Job: {ex.Message}");
                throw;
            }

            _toaster.LongAlert($"{ActiveJob.Name} Job Deleted");

            var nextJobToSelect = await _jobRepo.FirstOrDefaultAsync();
            if (nextJobToSelect != null)
                ActiveJob = nextJobToSelect;

            // refresh AllJobs. LoadAllJobsAsync will handle the case where we've removed all jobs from the DB
            AllJobs = new NotifyTaskCompletion<List<Job>>(LoadAllJobsAsync());
        }

        /// <summary>
        /// Saves a new statistic based on the inputs currently on screen
        /// </summary>
        public ICommand SaveStat { get; }
        private async Task SaveNewStatAsync()
        {
            // Keeping the responsibility of validating inputs OUT of this method. This should
            // only be called if the current user inputs are valid.

            var newStat = new IncomeStat
            {
                Job_ID = ActiveJob.ID,
                MoneyMade = TotalMoney.GetValueOrDefault(),
                HoursWorked = TotalHours.GetValueOrDefault(),
                DateStart = DateWorked
            };

            try
            {
                await _statRepo.AddAsync(newStat);
                _toaster.LongAlert("Successfully Saved Stats");
                TotalMoney = null;
                TotalHours = null;
            }
            catch (Exception ex)
            {
                // TODO Log and inform user of error
            }
        }

        /// <summary>
        /// Loads all jobs from the database
        /// </summary>
        /// <remarks>Will send user to the AddJobPage if no jobs are found</remarks>
        public async Task<List<Job>> LoadAllJobsAsync()
        {
            var jobs = await _jobRepo.GetAllAsync();
            var asList = jobs.ToList();
            if (asList.Count() == 0)
                await Shell.Current.GoToAsync("AddJobPage");

            return asList;
        }

        private bool ValidateInputs() => TotalHours > 0 && TotalMoney.HasValue;
    }
}
