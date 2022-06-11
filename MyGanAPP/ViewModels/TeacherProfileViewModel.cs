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
    internal class TeacherProfileViewModel : INotifyPropertyChanged
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

        #region TeacherFirstName
      
        private string teacherFirstName;

        public string TeacherFirstName
        {
            get => teacherFirstName;
            set
            {
                teacherFirstName = value;
                OnPropertyChanged("TeacherFirstName");
            }
        }

       
        #endregion

        #region TeacherLastName
       private string teacherLastName;

        public string TeacherLastName
        {
            get => teacherLastName;
            set
            {
                teacherLastName = value;
                OnPropertyChanged("TeacherLastName");
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

        #region GroupName

        private string groupName;

        public string GroupName
        {
            get => groupName;
            set
            {
                groupName = value;
                OnPropertyChanged("GroupName");
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
            User teacher = a.CurrUser;

            if (teacher != null)
            {
                UserImgSrc = teacher.PhotoURL;
                TeacherFirstName = teacher.Fname;
                TeacherLastName = teacher.LastName;
                Email = teacher.Email;
                PhoneNumber = teacher.PhoneNumber;
                GroupName = a.SelectedGroup.GroupName;
            }
            IsRefreshing = false;
        }
        #endregion

        #region Constructor
        public TeacherProfileViewModel()
        {
            ((App)App.Current).RefreshUI += OnRefresh;
            IsRefreshing = false;
            App a = (App)App.Current;
            User teacher = a.CurrUser;
            if (teacher != null)
            {
                UserImgSrc = teacher.PhotoURL;
                TeacherFirstName = teacher.Fname;
                TeacherLastName = teacher.LastName;
                Email = teacher.Email;
                PhoneNumber = teacher.PhoneNumber;
                GroupName = a.SelectedGroup.GroupName;
            }
        }

        #endregion

        public ICommand SaveCommand => new Command(OnSave);

        public async void OnSave()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.AddTeacherView());
        }

        public ICommand LogOutCommand => new Command(OnLogout);

        public async void OnLogout()
        {
           MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            bool ok = await proxy.Logout();

        }

           
    }
}
