using Gravimetry.ViewModels.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gravimetry.Views.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagerCreateTeamPage : ContentPage
    {
        public ManagerCreateTeamPage()
        {
            InitializeComponent();
            this.BindingContext = new ManagerCreateTeamViewModel();
        }
    }

}
