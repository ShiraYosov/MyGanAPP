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

        #endregion

        #region ManagerFirstName

        private string managerFirstName;

        public string ManagerFirstName
        {
            get => managerFirstName;
            set
            {
                managerFirstName = value;
                OnPropertyChanged("ManagerFirstName");
            }
        }


        #endregion

        #region ManagerLastName

        private string managerLastName;

        public string ManagerLastName
        {
            get => managerLastName;
            set
            {
                managerLastName = value;
                OnPropertyChanged("ManagerLastName");
            }
        }
        #endregion

        #region Email

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }


        #endregion

        #region PhoneNumber

        private string phoneNumber;

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }


        #endregion

        #region KindergartenName

        private string kindergartenName;

        public string KindergartenName
        {
            get => kindergartenName;
            set
            {
                kindergartenName = value;
                OnPropertyChanged("KindergartenName");
            }
        }
        #endregion
       
        #region Refresh
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        public ICommand RefreshCommand => new Command(OnRefresh);
        public void OnRefresh()
        {
            IsRefreshing = true;
            App a = (App)App.Current;
            User manager = a.CurrUser;
           
            if (manager != null)
            {
                UserImgSrc = manager.PhotoURL;
                ManagerFirstName = manager.Fname;
                ManagerLastName = manager.LastName;
                Email = manager.Email;
                PhoneNumber = manager.PhoneNumber;
                KindergartenName = a.SelectedKindergarten.Name;
            }
            IsRefreshing = false;
        }
        #endregion

        #region Constructor
        public ManagerProfileViewModel()
        {
            IsRefreshing=false;
            App a = (App)App.Current;
            User manager = a.CurrUser;
            if (manager != null)
            {
                UserImgSrc = manager.PhotoURL;
                ManagerFirstName = manager.Fname;
                ManagerLastName = manager.LastName;
                Email = manager.Email;
                PhoneNumber = manager.PhoneNumber;
                KindergartenName = a.SelectedKindergarten.Name;
            }
            ((App)App.Current).RefreshUI += OnRefresh;
            
        }

        #endregion

        
        public ICommand SaveCommand => new Command(OnSave);

        public async void OnSave()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.AddManagerView());
        }

        public ICommand LogOutCommand => new Command(OnLogout);

        public async void OnLogout()
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            bool ok = await proxy.Logout();

        }
    }
}
