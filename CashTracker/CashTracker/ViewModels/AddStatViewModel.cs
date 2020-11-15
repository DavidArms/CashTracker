using CashTracker.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CashTracker.ViewModels
{
    [QueryProperty("JobName", "jobName")]
    public class AddStatViewModel : BaseViewModel
    {
        private double? _totalHours;
        private double? _totalMoney;

        private Job _activeJob;
        /// <summary>
        /// The currently selected job
        /// </summary>
        public Job ActiveJob
        {
            get => _activeJob;
            set
            {
                _activeJob = value;
                OnPropertyChanged();
            }
        }

        private string _jobName;
        public string JobName
        {
            get => _jobName;
            set
            {
                SetProperty(ref _jobName, value);
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
        /// <param name="job"></param>
        /// <param name="repo"></param>
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
                Date = DateWorked,
                DayOfWeek = (int)DateWorked.DayOfWeek
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
