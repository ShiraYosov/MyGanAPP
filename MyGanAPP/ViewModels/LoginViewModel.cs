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


        private const string OPENEYE_PHOTO_SRC = "OpenEye.png";
        private const string CLOSEDEYE_PHOTO_SRC = "ClosedEye.png";


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

        private bool showPass;
        public bool ShowPass
        {
            get { return showPass; }

            set
            {
                if (this.showPass != value)
                {
                    this.showPass = value;
                    OnPropertyChanged(nameof(ShowPass));
                }
            }
        }

        private string imgSource1;
        public string ImgSource1
        {
            get => imgSource1;
            set
            {
                imgSource1 = value;
                OnPropertyChanged("ImgSource1");
            }
        }
        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
            PassCommand = new Command(OnShowPass);
            ShowPass = true;
            imgSource1 = CLOSEDEYE_PHOTO_SRC;
        }

        public ICommand LoginCommand { protected set; get; }
        public ICommand PassCommand { protected set; get; }

        public void OnShowPass()
        {
            if (ShowPass == false) 
            { ShowPass = true; }
           
            else { ShowPass = false; }

            if (imgSource1 == CLOSEDEYE_PHOTO_SRC) 
            { ImgSource1 = OPENEYE_PHOTO_SRC; }

            else { ImgSource1 = CLOSEDEYE_PHOTO_SRC; }
        }

       
        public async void Login()
        {

            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            User user = await proxy.LoginAsync(Email, Password);

            if (user != null)
            {
                App a = (App)App.Current;
                a.CurrUser = user;

               await App.Current.MainPage.Navigation.PushAsync(new ChooseView());

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "התחברות נכשלה, בדוק שם משתמש וסיסמה ונסה שוב", "בסדר");
            }

        }


    }
}
