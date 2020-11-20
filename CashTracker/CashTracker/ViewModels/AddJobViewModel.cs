using CashTracker.Database;
using CashTracker.Models;
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
        /// Cancels the Add Job operation and goes back to the main screen
        /// </summary>
        /// TODO: maybe just go back to the last screen? Not necessarily the main screen.
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
            var existingJob = await _repo.FirstOrDefaultAsync(job => job.Name == JobName);
            if (existingJob != null)
                return;

            var newJob = new Job
            {
                Name = JobName,
                Employer = "Fake"
            };
            await _repo.AddAsync(newJob);

            var item = new MenuItem()
            {
                Text = JobName,
                Command = new Command(async () => await GoToNewJob())
            };

            // Add the new pages to the shell
            Shell.Current.Items.Add(item);

            // Navigate to the add stat page for the new job
            await GoToNewJob();
        }

        private async Task GoToNewJob()
        {
            await Shell.Current.GoToAsync($"//AddStatPage?jobName={JobName}");
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}
