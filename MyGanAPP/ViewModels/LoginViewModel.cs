using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MyGanAPP.Services;
using System.Text.Json;
using MyGanAPP.Models;
using MyGanAPP.Views;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace MyGanAPP.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string message;
        public string Message
        {
            get { return this.message; }

            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        private string email;
        public string Email
        {
            get { return this.email; }

            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        private string password;
        public string Password
        {
            get { return this.password; }

            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public LoginViewModel()
        {
            this.Email = "";
            this.Password = "";
            this.Message = "";
        }

        public ICommand LoginCommand => new Command(Login);

        public async void Login()
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            User u = await proxy.LoginAsync(Email, Password);

            if (u != null)
            {
                App a = (App)App.Current;
                a.User = new User
                {
                    Fname= u.Fname,
                    LastName= u.LastName,
                    Email=u.Email,
                    Password=u.Password,
                    IsSystemManager=u.IsSystemManager,
                    PhoneNumber=u.PhoneNumber,
                    Groups = u.Groups,
                    KindergartenManagers = u.KindergartenManagers,
                    Signatures = u.Signatures,
                    StudentOfUsers = u.StudentOfUsers

                };

                this.Message = "הנך מחובר למערכת";
                //Page p = new UsersPageView();

                //await a.MainPage.Navigation.PushAsync(p);

            }
            else
            {
                this.Message = "פרטי משתמש אינם נכונים!";
            }

        }

        
    }
}
