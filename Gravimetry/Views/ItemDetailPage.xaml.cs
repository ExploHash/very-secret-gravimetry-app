using System.ComponentModel;
using Xamarin.Forms;
using Gravimetry.ViewModels;

namespace Gravimetry.Views
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
