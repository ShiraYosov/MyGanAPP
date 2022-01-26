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

namespace MyGanAPP.ViewModels
{

    class AddTeacherViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region TeacherFirstName
        private bool showTeacherFirstNameError;

        public bool ShowTeacherFirstNameError
        {
            get => showTeacherFirstNameError;
            set
            {
                showTeacherFirstNameError = value;
                OnPropertyChanged("ShowTeacherFirstNameError");
            }
        }

        private string teacherFirstName;

        public string TeacherFirstName
        {
            get => teacherFirstName;
            set
            {
                teacherFirstName = value;
                ValidateTeacherFirstName();
                OnPropertyChanged("TeacherFirstName");
            }
        }

        private string teacherFirstNameError;

        public string TeacherFirstNameError
        {
            get => teacherFirstNameError;
            set
            {
                teacherFirstNameError = value;
                OnPropertyChanged("TeacherFirstNameError");
            }
        }

        private void ValidateTeacherFirstName()
        {
            this.ShowTeacherFirstNameError = string.IsNullOrEmpty(TeacherFirstName);
        }
        #endregion

        #region TeacherLastName
        private bool showTeacherLastNameError;

        public bool ShowTeacherLastNameError
        {
            get => showTeacherLastNameError;
            set
            {
                showTeacherLastNameError = value;
                OnPropertyChanged("ShowTeacherLastNameError");
            }
        }

        private string teacherLastName;

        public string TeacherLastName
        {
            get => teacherLastName;
            set
            {
                teacherLastName = value;
                ValidateTeacherLastName();
                OnPropertyChanged("TeacherLastName");
            }
        }

        private string teacherLastNameError;

        public string TeacherLastNameError
        {
            get => teacherLastNameError;
            set
            {
                teacherLastNameError = value;
                OnPropertyChanged("TeacherLastNameError");
            }
        }

        private void ValidateTeacherLastName()
        {
            this.ShowTeacherLastNameError = string.IsNullOrEmpty(TeacherLastName);
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

        #region GanCode

        private bool showCodeError;

        public bool ShowCodeError
        {
            get => showCodeError;
            set
            {
                showCodeError = value;
                OnPropertyChanged("ShowCodeError");
            }
        }

        private string code;

        public string Code
        {
            get => code;
            set
            {
                code = value;
                ValidateCode();
                OnPropertyChanged("Code");
            }
        }

        private string codeError;

        public string CodeError
        {
            get => codeError;
            set
            {
                codeError = value;
                OnPropertyChanged("CodeError");
            }
        }

        private void ValidateCode()
        {
            this.ShowCodeError = string.IsNullOrEmpty(Code);
        }

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


        #region Register

        private bool ValidateForm()
        {
            //Validate all fields first

            ValidateTeacherLastName();
            ValidateTeacherFirstName();
            ValidateCode();
            ValidateEmail();
            ValidatePassword();
            ValidatePhoneNumber();



            //check if any validation failed
            if (showTeacherLastNameError || showTeacherFirstNameError || showCodeError || showEmailError ||
                 showPasswordError || showPhoneNumberError)

                return false;
            return true;
        }
        public ICommand RegisterCommand => new Command(Register);

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
                    Fname = TeacherFirstName,
                    LastName = TeacherLastName,
                    PhoneNumber = PhoneNumber,
                };

                int groupID = GanCode.CodeToGroupID(Code);

                newUser.Groups.Add(new Models.Group 
                {
                    GroupId = groupID
                });

               
                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                User newU = await proxy.TeacherRegister(newUser);

                if (newU == null)
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "הרשמה נכשלה", "בסדר");
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    if (this.imageFileResult != null)
                    {
                        ServerStatus = "מעלה תמונה...";

                        bool success = await proxy.UploadImage(new FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"{newU.UserId}.jpg");
                    }
                    ServerStatus = "שומר נתונים...";
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    await App.Current.MainPage.Navigation.PopToRootAsync();
                    await App.Current.MainPage.Navigation.PushAsync(new LoginView());
                }


            }
        }

        #endregion

        public AddTeacherViewModel()
        {
            // Setup default image photo
            this.UserImgSrc = DEFAULT_PHOTO_SRC;
            this.imageFileResult = null; //mark that no picture was chosen
            teacherFirstName = "";
            teacherLastName = "";
            email = "";
            phoneNumber = "";
            password = "";

            this.teacherFirstNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.teacherLastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.showTeacherFirstNameError = false;
            this.showTeacherLastNameError = false;
            this.showEmailError = false;
            this.showPhoneNumberError = false;
            this.showPasswordError = false;

        }
        public void ChangeBools()
        {
            this.ShowTeacherFirstNameError = false;
            this.ShowTeacherLastNameError = false;
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

