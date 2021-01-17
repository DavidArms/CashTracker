using CashTracker.Models;
using Rg.Plugins.Popup.Pages;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace CashTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobsPopup : PopupPage
    {
        private List<Job> _jobs;
        public List<Job> Jobs
        {
            get => _jobs;
            set 
            { 
                _jobs = value;
                OnPropertyChanged(nameof(Jobs));
            }
        }


        public JobsPopup(List<Job> jobs)
        {
            InitializeComponent();
            BindingContext = this;
            Jobs = jobs;
        }
    }
}