using System.Threading.Tasks;
using Xamarin.Forms;
using Gravimetry.ViewModels;


namespace Gravimetry.Views
{
    public partial class MonitorOverviewPage : ContentPage
    {
        MonitorsViewModel _viewModel;

        public MonitorOverviewPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MonitorsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(async () =>
            {
                await _viewModel.ExecuteLoadItemsCommand();
            });
        }

        private void OnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            // Has Backspace or Cancel has been pressed?
            if (textChangedEventArgs.NewTextValue == string.Empty)
            {
                Task.Run(async () =>
                {
                    await _viewModel.ExecuteLoadItemsCommand();
                });
            }
        }

    }
}
