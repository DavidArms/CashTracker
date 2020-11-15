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
                execute: () =>
                {
                    var newPage = new AddStatPage();
                    var newMenu = new FlyoutItem
                    {
                        Title = JobName
                    };
                    var content = new ShellContent()
                    {
                        Route = "TestPage",
                        Content = newPage
                    };
                    newMenu.Items.Add(content);
                    Shell.Current.Items.Add(newMenu);

                    Shell.Current.CurrentItem = content;
                },
                canExecute: () => !string.IsNullOrEmpty(JobName)
            );



        }
    }
}
