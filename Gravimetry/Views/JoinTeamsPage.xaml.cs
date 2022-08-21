using Xamarin.Forms;
using Gravimetry.ViewModels;
using Gravimetry.Services;


namespace Gravimetry.Views
{
    public partial class JoinTeamsPage : ContentPage
    {
        JoinTeamsViewModel _viewModel;

        public JoinTeamsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new JoinTeamsViewModel(
                new TeamsService(),
                new UserService()
            );
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
