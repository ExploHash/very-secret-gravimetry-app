using Xamarin.Forms;
using Gravimetry.ViewModels.Manager;


namespace Gravimetry.Views.Manager
{
    public partial class ManagerAddMonitorsPage : ContentPage
    {
        public ManagerAddMonitorsPage()
        {
            InitializeComponent();

            BindingContext = new ManagerAddMonitorViewModel();
        }
    }
}
