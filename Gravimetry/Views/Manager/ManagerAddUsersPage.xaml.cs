using Xamarin.Forms;
using Gravimetry.ViewModels.Manager;


namespace Gravimetry.Views.Manager
{
    public partial class ManagerAddUsersPage : ContentPage
    {
        public ManagerAddUsersPage()
        {
            InitializeComponent();

            BindingContext = new ManagerAddUsersViewModel();
        }
    }
}