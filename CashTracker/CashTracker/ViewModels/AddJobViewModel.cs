using CashTracker.Views;
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
                execute: SaveNewJob,
                canExecute: () => !string.IsNullOrEmpty(JobName)
            );



        }

        private void SaveNewJob()
        {
            //TODO: Verify that none of the jobs in the database have the same name as this job

            var newSideBarItem = new FlyoutItem
            {
                Title = JobName
            };

            // TODO: look into making tab templates here instead
            var addStatTab = new Tab()
            {
                Title = "Add Stat"
            };
            // TODO: Create a new View Stats page
            var aboutTab = new Tab()
            {
                Title = "About"
            };

            var addStatPage = new ShellContent()
            {
                Route = "AddStatPage",
                Content = new AddStatPage()
            };
            var aboutPage = new ShellContent()
            {
                Route = "AboutPage",
                Content = new AboutPage()
            };

            addStatTab.Items.Add(addStatPage);
            aboutTab.Items.Add(aboutPage);

            newSideBarItem.Items.Add(addStatTab);
            newSideBarItem.Items.Add(aboutTab);

            // Add the new pages to the shell
            Shell.Current.Items.Add(newSideBarItem);

            // Navigate to the add stat page for the new job
            // TODO: Determine if this is the best approach. Doing this just replaces the active page with the addStatPage. There's no animation.
            // If we were to use "goToAsync" instead then we'd end up with the nav bar and back button at the top.
            Shell.Current.CurrentItem = addStatPage;
        }
    }
}
