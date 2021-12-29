﻿using System;
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

namespace MyGanAPP.ViewModels
{
    class AddManagerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region ManagerFirstName
        private bool showManagerFirstNameError;

        public bool ShowManagerFirstNameError
        {
            get => showManagerFirstNameError;
            set
            {
                showManagerFirstNameError = value;
                OnPropertyChanged("ShowManagerFirstNameError");
            }
        }

        private string managerFirstName;

        public string ManagerFirstName
        {
            get => managerFirstName;
            set
            {
                managerFirstName = value;
                ValidateManagerFirstName();
                OnPropertyChanged("ManagerFirstName");
            }
        }

        private string managerFirstNameError;

        public string ManagerFirstNameError
        {
            get => managerFirstNameError;
            set
            {
                managerFirstNameError = value;
                OnPropertyChanged("ManagerFirstNameError");
            }
        }

        private void ValidateManagerFirstName()
        {
            this.ShowManagerFirstNameError = string.IsNullOrEmpty(ManagerFirstName);
        }
        #endregion

        #region ManagerLastName
        private bool showManagerLastNameError;

        public bool ShowManagerLastNameError
        {
            get => showManagerLastNameError;
            set
            {
                showManagerLastNameError = value;
                OnPropertyChanged("ShowManagerLastNameError");
            }
        }

        private string managerLastName;

        public string ManagerLastName
        {
            get => managerLastName;
            set
            {
                managerLastName = value;
                ValidateManagerLastName();
                OnPropertyChanged("ManagerLastName");
            }
        }

        private string managerLastNameError;

        public string ManagerLastNameError
        {
            get => managerLastNameError;
            set
            {
                managerLastNameError = value;
                OnPropertyChanged("ManagerLastNameError");
            }
        }

        private void ValidateManagerLastName()
        {
            this.ShowManagerLastNameError = string.IsNullOrEmpty(ManagerLastName);
        }
        #endregion

        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }
            else
                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region PhoneNumber
        private bool showPhoneNumberError;

        public bool ShowPhoneNumberError
        {
            get => showPhoneNumberError;
            set
            {
                showPhoneNumberError = value;
                OnPropertyChanged("ShowPhoneNumberError");
            }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                ValidatePhoneNumber();
                OnPropertyChanged("PhoneNumber");
            }
        }

        private string phoneNumberError;

        public string PhoneNumberError
        {
            get => phoneNumberError;
            set
            {
                phoneNumberError = value;
                OnPropertyChanged("PhoneNumberError");
            }
        }

        private void ValidatePhoneNumber()
        {
            this.ShowPhoneNumberError = string.IsNullOrEmpty(PhoneNumber);
            if (!this.ShowPhoneNumberError)
            {
                if (this.PhoneNumber.Length < 10)
                {
                    this.ShowPhoneNumberError = true;
                    this.PhoneNumberError = ERROR_MESSAGES.BAD_PHONE_NUMBER;
                }

            }
            else
                this.PhoneNumberError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            this.ShowPasswordError = string.IsNullOrEmpty(Password);
            if (!this.ShowPasswordError)
            {
                if (this.Password.Length < 6)
                {
                    this.ShowPasswordError = true;
                    this.PasswordError = ERROR_MESSAGES.SHORT_PASS;
                }
            }
            else
                this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

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

        #region ServerStatus
        private string serverStatus;
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }
        #endregion

        #region KindergartenName
        private bool showKindergartenNameError;

        public bool ShowKindergartenNameError
        {
            get => showKindergartenNameError;
            set
            {
                showKindergartenNameError = value;
                OnPropertyChanged("ShowKindergartenNameError");
            }
        }

        private string kindergartenName;

        public string KindergartenName
        {
            get => kindergartenName;
            set
            {
                kindergartenName = value;
                ValidateKindergartenName();
                OnPropertyChanged("KindergartenName");
            }
        }

        private string kindergartenNameError;

        public string KindergartenNameError
        {
            get => kindergartenNameError;
            set
            {
                kindergartenNameError = value;
                OnPropertyChanged("KindergartenNameError");
            }
        }

        private void ValidateKindergartenName()
        {
            this.ShowKindergartenNameError = string.IsNullOrEmpty(KindergartenName);
        }
        #endregion


        public AddManagerViewModel()
        {
            // Setup default image photo
            this.UserImgSrc = DEFAULT_PHOTO_SRC;
            this.imageFileResult = null; //mark that no picture was chosen
            ManagerFirstName = "";
            ManagerLastName = "";
            email = "";
            phoneNumber = "";
            password = "";

            this.ManagerFirstNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ManagerLastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.showManagerFirstNameError = false;
            this.showManagerLastNameError = false;
            this.showEmailError = false;
            this.showPhoneNumberError = false;
            this.showPasswordError = false;

        }

        #region Register

        private bool ValidateForm()
        {
            //Validate all fields first

            ValidateManagerLastName();
            ValidateManagerFirstName();
            ValidateKindergartenName();
            ValidateEmail();
            ValidatePassword();
            ValidatePhoneNumber();



            //check if any validation failed
            if (showManagerLastNameError || showManagerFirstNameError || showKindergartenNameError || showEmailError ||
                 showPasswordError || showPhoneNumberError)

                return false;
            return true;
        }
        public ICommand RegisterCommand { protected set; get; }

        public async void Register()
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();

            App app = (App)App.Current;
            if (ValidateForm())
            {
                User newUser = new User
                {
                    Email = Email,
                    Password = Password,
                    Fname = ManagerFirstName,
                    LastName = ManagerLastName,
                    PhoneNumber = PhoneNumber,
                };
                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                User newU = await proxy.Register(newUser);

                if(newU != null)
                {

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה","הרשמה נכשלה", "בסדר");
                }
            }

        }
        #endregion

        public void ChangeBools()
        {
            this.ShowKindergartenNameError = false;
            this.ShowManagerLastNameError = false;
            this.ShowManagerFirstNameError = false;
            this.ShowEmailError = false;
            this.ShowPhoneNumberError = false;
            this.ShowPasswordError = false;
        }

        #region PhotoButton

        FileResult imageFileResult;
        public event Action<ImageSource> SetImageSourceEvent;
        public ICommand PickImageCommand => new Command(OnPickImage);
        public async void OnPickImage()
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "בחר תמונה"
            });

            if (result != null)
            {
                this.imageFileResult = result;

                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }

        //The following command handle the take photo button
        public ICommand CameraImageCommand => new Command(OnCameraImage);
        public async void OnCameraImage()
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
            {
                Title = "צלם תמונה"
            });

            if (result != null)
            {
                this.imageFileResult = result;
                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }
        #endregion

    }
}