using CashTracker.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace CashTracker.ViewModels
{
    public class AddJobViewModel : BaseViewModel
    {
        public ICommand AddJobCommand { get; }

        public AddJobViewModel()
        {
            Title = "Add Job";
            AddJobCommand = new Command(() =>
            {
                var newPage = new AddStatPage();
                var newMenu = new FlyoutItem
                {
                    Title = "Added Job"
                };
                var content = new ShellContent()
                {
                    Route = "TestPage",
                    Content = newPage
                };
                newMenu.Items.Add(content);
                Shell.Current.Items.Add(newMenu);

                Shell.Current.CurrentItem = content;
            });
        }
    }
}
