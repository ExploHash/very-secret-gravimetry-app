using Xamarin.Forms;
using Gravimetry.ViewModels;


namespace Gravimetry.Views
{
    public partial class TeamsOverviewPage : ContentPage
    {
        TeamsViewModel _viewModel;

        public TeamsOverviewPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TeamsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
