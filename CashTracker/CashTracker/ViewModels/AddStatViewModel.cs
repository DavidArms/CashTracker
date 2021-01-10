using CashTracker.Database;
using CashTracker.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
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

        private double? _totalHours;
        private double? _totalMoney;

        private ObservableRangeCollection<Job> _allJobs = new ObservableRangeCollection<Job>();
        public ObservableRangeCollection<Job> AllJobs
        {
            get => _allJobs;
            set => SetProperty(ref _allJobs, value); //TODO: Use the OnChanged action to handle the case where there are no jobs
        }

        private Job _activeJob;
        /// <summary>
        /// The currently selected job
        /// </summary>
        public Job ActiveJob
        {
            get
            {
                //TODO: Update language version to get null coalescing assignment operator
                if (_activeJob == null)
                    ActiveJob = Task.Run(async () => await _jobRepo.FirstOrDefaultAsync()).Result;

                return _activeJob;
            }
            set
            {
                SetProperty(ref _activeJob, value);
                OnPropertyChanged(nameof(AllJobs));
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
            set => SetProperty(ref _totalHours, value);
        }

        /// <summary>
        /// The total money earned
        /// </summary>
        public double? TotalMoney
        {
            get => _totalMoney;
            set => SetProperty(ref _totalMoney, value);
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

            Task.Run(async () => AllJobs.AddRange(await _jobRepo.GetAllAsync()));
        }

        public ICommand DeleteCommand { get; }
        private async Task DeleteJobAsync()
        {
            AllJobs.Remove(ActiveJob);
            await _jobRepo.RemoveAsync(ActiveJob);
            var nextJobToSelect = await _jobRepo.FirstOrDefaultAsync();
            if (nextJobToSelect == null)
            {
                ActiveJob = new Job();
                await Shell.Current.GoToAsync("AddJobPage");
            }
            else
                ActiveJob = nextJobToSelect;
        }

        public ICommand SaveStat { get; }
        /// <summary>
        /// Saves a new statistic based on the inputs currently on screen
        /// </summary>
        private async Task SaveNewStatAsync()
        {
            var newStat = new IncomeStat
            {
                Job_ID = ActiveJob.ID,
                MoneyMade = TotalMoney.Value,
                HoursWorked = TotalHours.Value,
                DateStart = DateWorked
            };

            //try
            //{
            //    _repo.SaveNewIncomeStatForJob(newStat);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    // TODO Log and inform user of error
            //    return false;
            //}
        }

        private bool ValidateInputs() => TotalHours > 0 && TotalMoney.HasValue;
    }
}
