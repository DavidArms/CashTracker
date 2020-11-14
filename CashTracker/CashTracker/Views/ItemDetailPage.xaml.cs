using CashTracker.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CashTracker.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}