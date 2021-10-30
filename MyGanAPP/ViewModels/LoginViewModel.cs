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
            //this.Email = "";
            //this.Password = "";
            LoginCommand = new Command(Login);
        }

        public ICommand LoginCommand { protected set; get; }
       

        public async void Login()
        {

            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            User user = await proxy.LoginAsync(Email, Password);

            if (user != null)
            {
                App a = (App)App.Current;
                a.User = new User
                {
                    Fname= user.Fname,
                    LastName= user.LastName,
                    Email= user.Email,
                    Password= user.Password,
                    IsSystemManager= user.IsSystemManager,
                    PhoneNumber= user.PhoneNumber,
                    Groups = user.Groups,
                    KindergartenManagers = user.KindergartenManagers,
                    Signatures = user.Signatures,
                    StudentOfUsers = user.StudentOfUsers

                };

               

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "התחברות נכשלה, בדוק שם משתמש וסיסמה ונסה שוב", "בסדר");
            }

        }

        
    }
}
