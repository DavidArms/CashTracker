using CashTracker.Database;
using CashTracker.Models;
using MvvmHelpers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CashTracker.ViewModels
{
    public class AddJobViewModel : BaseViewModel
    {
        private IAsyncRepository<Job> _repo = App.Container.Resolve<IAsyncRepository<Job>>();

        /// <summary>
        /// Command to add the new job to the database
        /// </summary>
        public ICommand AddJobCommand { get; }

        /// <summary>
        /// Cancels the Add Job operation and goes back
        /// </summary>
        public ICommand CancelCommand { get; }

        private string _jobName;
        /// <summary>
        /// Provided name of the new job
        /// </summary>
        public string JobName
        {
            get => _jobName;
            set
            {
                SetProperty(ref _jobName, value);
                (AddJobCommand as Command).ChangeCanExecute();
            }
        }

        private string _employerName;
        /// <summary>
        /// Provided name of the new job
        /// </summary>
        public string EmployerName
        {
            get => _employerName;
            set
            {
                SetProperty(ref _employerName, value);
                (AddJobCommand as Command).ChangeCanExecute();
            }
        }

        public AddJobViewModel()
        {
            Title = "Add Job";
            AddJobCommand = new Command(
                execute: async () => await SaveNewJob(),
                canExecute: () => !string.IsNullOrEmpty(JobName) && !string.IsNullOrEmpty(EmployerName)
            );
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        private async Task SaveNewJob()
        {
            var existingJob = await _repo.FirstOrDefaultAsync(job => job.Name == JobName && job.Employer == EmployerName);
            if (existingJob != null)
                return;

            var newJob = new Job
            {
                Name = JobName,
                Employer = EmployerName
            };
            await _repo.AddAsync(newJob);

            // Navigate to the add stat page for the new job
            await Shell.Current.GoToAsync($"//AddStatPage?JobID={newJob.ID}");
        }
    }
}
