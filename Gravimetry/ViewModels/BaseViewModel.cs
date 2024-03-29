﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Gravimetry.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //Event watched by refreshviews to refresh their view
        public event PropertyChangedEventHandler PropertyChanged;

        //Holds raw private boolean for state
        bool isBusy = false;

        //Getter setter for public access
        public bool IsBusy
        {
            get { return isBusy; } //Just return value
            set {
                isBusy = value; //Set value
                OnPropertyChanged("IsBusy"); //Trigger event
            }
        }

        //Function which needs to be implemented by INotifyPropertyChanged
        //Triggers PropertyChanged event
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")//CallerMemberName is a annotation to automatically fill property name if executed from setter
        {
            //Actually invoke event for our refreshview
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
