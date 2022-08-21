using Xamarin.Forms;
using Gravimetry.ViewModels.Manager;


namespace Gravimetry.Views.Manager
{
    public partial class ManagerTeamQRPage : ContentPage
    {
        public ManagerTeamQRPage()
        {
            InitializeComponent();

            BindingContext = new ManagerTeamQRViewModel();
        }
    }
}
