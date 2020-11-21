using CashTracker.Database;
using CashTracker.Models;
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
            set => SetProperty(ref _activeJob, value);
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
        private double? TotalHours
        {
            get => _totalHours;
            set
            {
                _totalHours = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The string version of the Total Hours
        /// </summary>
        /// <remarks>Needed to bind to the Entry control</remarks>
        public string TotalHoursString
        {
            get
            {
                if (TotalHours == null)
                    return "";
                return TotalHours.ToString();
            }
            set
            {
                try
                {
                    TotalHours = double.Parse(value);
                }
                catch
                {
                    TotalHours = null;
                }
            }
        }

        /// <summary>
        /// The total money earned
        /// </summary>
        private double? TotalMoney
        {
            get => _totalMoney;
            set
            {
                _totalMoney = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The string version of the total money earned
        /// </summary>
        /// <remarks>Needed for binding to the Entry control</remarks>
        public string TotalMoneyString
        {
            get
            {
                if (TotalMoney == null)
                    return "";
                return TotalMoney.ToString();
            }
            set
            {
                try
                {
                    TotalMoney = double.Parse(value);
                }
                catch
                {
                    TotalMoney = null;
                }
            }
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
            SaveStat = new Command(SaveNewStat);
        }

        public ICommand SaveStat { get; }
        /// <summary>
        /// Saves a new statistic based on the inputs currently on screen
        /// </summary>
        public void SaveNewStat()
        {
            if (!ValidateInputs())
                return;

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

        private bool ValidateInputs()
        {
            //TODO: Might want to build an error message for displaying instead
            return TotalHours != null && TotalMoney != null;
        }
    }
}
