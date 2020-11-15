using CashTracker.Views;
using System;
using Xamarin.Forms;

namespace CashTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //TODO: Register routes not defined in the shell visual hiearchy here
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AddJobPage");
        }
    }
}
