using Xamarin.Forms;
using Gravimetry.ViewModels.Manager;


namespace Gravimetry.Views.Manager
{
    public partial class ManagerTeamsPage : ContentPage
    {
        ManagerTeamsViewModel _viewModel;

        public ManagerTeamsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ManagerTeamsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
