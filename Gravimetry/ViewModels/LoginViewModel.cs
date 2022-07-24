using Gravimetry.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Gravimetry.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            
        }

        public string Username;

        public string Password;
    }
}
