using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CashTracker.ViewModels
{
    public class AddJobViewModel : BaseViewModel
    {
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
            //TODO: Verify that none of the jobs in the database have the same name as this job
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
