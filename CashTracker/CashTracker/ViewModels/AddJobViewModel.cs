using CashTracker.Database;
using CashTracker.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CashTracker.ViewModels
{
    public class AddJobViewModel : BaseViewModel
    {
        private JobRepository _repo = DependencyService.Get<JobRepository>();

        public ICommand AddJobCommand { get; }

        private string _jobName;
        public string JobName
        {
            get => _jobName;
            set
            {
                SetProperty(ref _jobName, value);
                (AddJobCommand as Command).ChangeCanExecute();
            }
        }

        public AddJobViewModel()
        {
            Title = "Add Job";
            AddJobCommand = new Command(
                execute: async () => await SaveNewJob(),
                canExecute: () => !string.IsNullOrEmpty(JobName)
            );
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
