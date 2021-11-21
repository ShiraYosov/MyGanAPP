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
using System.Text.RegularExpressions;

namespace MyGanAPP.ViewModels
{
    public static class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "זהו שדה חובה";
        public const string BAD_EMAIL = "אימייל לא תקין";
        public const string SHORT_PASS = "הסיסמה חייבת להכיל לפחות 6 תווים";
        public const string BAD_PHONE = "טלפון לא תקין";
        public const string BAD_DATE = "על הילד להיות מעל גיל שנה";
    }

    class ParentRegistrationViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private const string Source1 = "arrow.png";
        private const string Source2 = "arrow2.png";

        #region ChildLastName
        private bool showChildLastNameError;

        public bool ShowChildLastNameError
        {
            get => showChildLastNameError;
            set
            {
                showChildLastNameError = value;
                OnPropertyChanged("ShowChildLastNameError");
            }
        }

        private string childLastName;

        public string ChildLastName
        {
            get => childLastName;
            set
            {
                childLastName = value;
                ValidateChildLastName();
                OnPropertyChanged("ChildLastName");
            }
        }

        private string childLastNameError;

        public string ChildLastNameError
        {
            get => childLastNameError;
            set
            {
                childLastNameError = value;
                OnPropertyChanged("ChildLastNameError");
            }
        }

        private void ValidateChildLastName()
        {
            this.ShowChildLastNameError = string.IsNullOrEmpty(ChildLastName);
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

        #region ChildFirstName
        private bool showChildNameError;

        public bool ShowChildNameError
        {
            get => showChildNameError;
            set
            {
                showChildNameError = value;
                OnPropertyChanged("ShowChildNameError");
            }
        }

        private string childName;

        public string ChildName
        {
            get => childName;
            set
            {
                childName = value;
                ValidateChildName();
                OnPropertyChanged("ChildName");
            }
        }

        private string childNameError;

        public string ChildNameError
        {
            get => childNameError;
            set
            {
                childNameError = value;
                OnPropertyChanged("ChildNameError");
            }
        }

        private void ValidateChildName()
        {
            this.ShowChildNameError = string.IsNullOrEmpty(ChildName);
        }
        #endregion

        #region ChildID
        private bool showChildIDError;

        public bool ShowChildIDError
        {
            get => showChildIDError;
            set
            {
                showChildIDError = value;
                OnPropertyChanged("ShowChildIDError");
            }
        }

        private string childID;

        public string ChildID
        {
            get => childID;
            set
            {
                childID = value;
                ValidateChildID();
                OnPropertyChanged("ChildID");
            }
        }

        private string childIDError;

        public string ChildIDError
        {
            get => childIDError;
            set
            {
                childIDError = value;
                OnPropertyChanged("ChildIDError");
            }
        }

        private void ValidateChildID()
        {
            this.ShowChildIDError = string.IsNullOrEmpty(ChildID);
        }
        #endregion

        #region BirthDate
        private bool showBirthDateError;

        public bool ShowBirthDateError
        {
            get => showBirthDateError;
            set
            {
                showBirthDateError = value;
                OnPropertyChanged("ShowBirthDateError");
            }
        }

        private DateTime birthDate;

        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                ValidateBirthDate();
                OnPropertyChanged("BirthDate");
            }
        }

        private string birthDateError;

        public string BirthDateError
        {
            get => birthDateError;
            set
            {
                birthDateError = value;
                OnPropertyChanged("BirthDateError");
            }
        }

        private const int MIN_AGE = 1;
        private void ValidateBirthDate()
        {
            TimeSpan ts = DateTime.Now - this.BirthDate;
            this.ShowBirthDateError = ts.TotalDays < (MIN_AGE * 365);
        }
        #endregion

        #region ButtonPressed
        private bool button1;
        public bool Button1
        {
            get { return this.button1; }

            set
            {
                if (this.button1 != value)
                {
                    this.button1 = value;
                    OnPropertyChanged(nameof(Button1));
                }
            }
        }

        private bool button2;
        public bool Button2
        {
            get { return this.button2; }

            set
            {
                if (this.button2 != value)
                {
                    this.button2 = value;
                    OnPropertyChanged(nameof(Button2));
                }
            }
        }
        #endregion

        #region imgChange
        private string imgSource1;
        public string ImgSource1
        {
            get { return this.imgSource1; }

            set
            {
                if (this.imgSource1 != value)
                {
                    this.imgSource1 = value;
                    OnPropertyChanged(nameof(ImgSource1));
                }
            }
        }

        private string imgSource2;
        public string ImgSource2
        {
            get { return this.imgSource2; }

            set
            {
                if (this.imgSource2 != value)
                {
                    this.imgSource2 = value;
                    OnPropertyChanged(nameof(ImgSource2));
                }
            }
        }
        #endregion

        #region Gender
        private bool showGenderError;

        public bool ShowGenderError
        {
            get => showGenderError;
            set
            {
                showGenderError = value;
                OnPropertyChanged("ShowGenderError");
            }
        }

        private string gender;

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                ValidateGender();
                OnPropertyChanged("Gender");
            }
        }

        private string genderError;

        public string GenderError
        {
            get => genderError;
            set
            {
                genderError = value;
                OnPropertyChanged("GenderError");
            }
        }

        private void ValidateGender()
        {
            this.ShowGenderError = string.IsNullOrEmpty(Gender);
        }
        #endregion

        #region Grade

        private Grade grade;

        public Grade Grade
        {
            get => grade;
            set
            {
                grade = value;
                OnPropertyChanged("Grade");
            }
        }

        public List<Grade> GradeTypes
        {
            get
            {
                App theApp = (App)App.Current;
                List<Grade> grades = new List<Grade>();
                foreach (Grade g in theApp.LookupTables.Grades)
                {
                    grades.Add(g);
                }
                return grades;
            }
        }
        #endregion

        #region UserName
        private bool showUserNameError;

        public bool ShowUserNameError
        {
            get => showUserNameError;
            set
            {
                showUserNameError = value;
                OnPropertyChanged("ShowUserNameError");
            }
        }

        private string userName;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                ValidateUserName();
                OnPropertyChanged("UserName");
            }
        }

        private string userNameError;

        public string UserNameError
        {
            get => userNameError;
            set
            {
                userNameError = value;
                OnPropertyChanged("UserNameError");
            }
        }

        private void ValidateUserName()
        {
            this.ShowUserNameError = string.IsNullOrEmpty(UserName);
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

        //This contact is a reference to the updated or new created contact
        private User theUser;
        public ParentRegistrationViewModel(User u = null)
        {
            //create a new user contact if this is an add operation
            if (theUser == null)
            {
                App theApp = (App)App.Current;
                u = new User()
                {
                    LastName = "",
                    Fname = "",
                    Email = "",
                    PhoneNumber = "",
                    Password = "",
                    Groups = new List<Models.Group>(),
                    Signatures = new List<Signature>(),
                    IsSystemManager = false,
                    StudentOfUsers = new List<StudentOfUser>(),
                };

                //Setup default image photo
                this.UserImgSrc = DEFAULT_PHOTO_SRC;
                this.imageFileResult = null; //mark that no picture was chosen

            }
            else
            {
                //set the path url to the contact photo
                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                //Create a source with cache busting!
                Random r = new Random();
                this.UserImgSrc = proxy.GetBasePhotoUri() + ChildID + $".jpg?{r.Next()}";
            }

            this.theUser = u;
            Button1 = false;
            Button2 = false;
            ImgSource1 = Source1;
            ImgSource2 = Source1;
            Button1PressedCommand = new Command(Button1Pressed);
            Button2PressedCommand = new Command(Button2Pressed);
            this.BirthDate = DateTime.Today;

            this.ShowChildNameError = false;
            this.ShowChildLastNameError = false;
            this.ShowChildIDError = false;
            this.ShowBirthDateError = false;
            this.ShowGenderError = false;
            this.ShowUserNameError = false;
            this.ShowEmailError = false;
            this.ShowPasswordError = false;

            this.ChildLastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ChildNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ChildIDError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.BirthDateError = ERROR_MESSAGES.BAD_DATE;
            this.GenderError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.UserNameError = ERROR_MESSAGES.REQUIRED_FIELD;

            //Setup default image photo
            this.UserImgSrc = DEFAULT_PHOTO_SRC;
            this.imageFileResult = null; //mark that no picture was chosen

        }

        //The following command handle the pick photo button
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

        #region ButtonCommands
        public ICommand Button1PressedCommand { protected set; get; }
        public void Button1Pressed()
        {
            if (Button1 == false) { Button1 = true; }
            else { Button1 = false; }

            if (ImgSource1 == Source1) { ImgSource1 = Source2; }
            else { ImgSource1 = Source1; }
        }

        public ICommand Button2PressedCommand { protected set; get; }
        public void Button2Pressed()
        {
            if (Button2 == false) { Button2 = true; }
            else { Button2 = false; }

            if (ImgSource2 == Source1) { ImgSource2 = Source2; }
            else { ImgSource2 = Source1; }
        }
        #endregion
    }
}

