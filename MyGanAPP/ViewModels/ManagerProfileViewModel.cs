using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using MyGanAPP.Services;
using MyGanAPP.Models;
using Xamarin.Essentials;
using System.Linq;
using MyGanAPP.Views;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Collections;

namespace MyGanAPP.ViewModels
{
     class ManagerProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region UserImgSrc
        private string userImgSrc;

        public string UserImgSrc
        {
            get => userImgSrc;
            set
            {
                userImgSrc = value;
                OnPropertyChanged("UserImgSrc");
            }
        }
        private const string DEFAULT_PHOTO_SRC = "user.png";
        #endregion

        #region Constructor
        public ManagerProfileViewModel()
        {
            App a = (App)App.Current;
            if (a.CurrUser.PhotoURL == null)
            {
                userImgSrc = DEFAULT_PHOTO_SRC;
            }
            else
                userImgSrc = a.CurrUser.PhotoURL;

        }

        #endregion

        

    }
}
