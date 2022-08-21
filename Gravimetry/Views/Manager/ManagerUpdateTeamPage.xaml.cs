using Gravimetry.ViewModels.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gravimetry.Views.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagerUpdateTeamPage : ContentPage
    {
        ManagerUpdateTeamViewModel _viewModel;

        public ManagerUpdateTeamPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new ManagerUpdateTeamViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }

}
