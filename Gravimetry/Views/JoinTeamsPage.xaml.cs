using Xamarin.Forms;
using Gravimetry.ViewModels;


namespace Gravimetry.Views
{
    public partial class JoinTeamsPage : ContentPage
    {
        JoinTeamsViewModel _viewModel;

        public JoinTeamsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new JoinTeamsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
