using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Gravimetry.Models;
using Gravimetry.ViewModels;

namespace Gravimetry.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Team Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}
