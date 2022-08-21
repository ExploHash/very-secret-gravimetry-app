using Xamarin.Forms;
using Gravimetry.ViewModels;


namespace Gravimetry.Views
{
    public partial class ActiveIncidentsPage : ContentPage
    {
        ActiveIncidentsViewModel _viewModel;

        public ActiveIncidentsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ActiveIncidentsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.IsRefreshing = true; //Turn on refresh when reentering page
            _viewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.IsRefreshing = false; //Turn off refresh
        }
    }
}
