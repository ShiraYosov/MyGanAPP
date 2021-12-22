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
    public static class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "זהו שדה חובה";
        public const string BAD_EMAIL = "אימייל לא תקין";
        public const string SHORT_PASS = "הסיסמה חייבת להכיל לפחות 6 תווים";
        public const string BAD_PHONE = "טלפון לא תקין";
        public const string BAD_DATE = "על הילד להיות מעל גיל שנה";
        public const string BAD_PHONE_NUMBER = "מספר הטלפון חייב להכיל 10 מספרים";
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

        private Grade chosenGrade;

        public Grade ChosenGrade
        {
            get => chosenGrade;
            set
            {
                chosenGrade = value;
                OnPropertyChanged("ChosenGrade");
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

        private bool showGradeError;

        public bool ShowGradeError
        {
            get => showGradeError;
            set
            {
                showGradeError = value;
                OnPropertyChanged("ShowGradeError");
            }
        }

        

        private string gradeError;

        public string GradeError
        {
            get => gradeError;
            set
            {
                gradeError = value;
                OnPropertyChanged("GradeError");
            }
        }

        private void ValidateGrade()
        {
            this.ShowGradeError = (ChosenGrade == null);
        }


        #endregion

        #region UserName1
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

        #region Password1
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

        #region UserName2
        private string userName2;

        public string UserName2
        {
            get => userName2;
            set
            {
                userName2 = value;
                OnPropertyChanged("UserName2");
            }
        }

        #endregion

        #region Password2
        private string password2;

        public string Password2
        {
            get => password2;
            set
            {
                password2 = value;
                OnPropertyChanged("Password2");
            }
        }
        #endregion

        #region Email2
        private string email2;

        public string Email2
        {
            get => email2;
            set
            {
                email2 = value;
                OnPropertyChanged("Email2");
            }
        }
        #endregion

        #region PhoneNumber1
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

        #region PhoneNumber2
        private bool showPhoneNumber2Error;

        public bool ShowPhoneNumber2Error
        {
            get => showPhoneNumber2Error;
            set
            {
                showPhoneNumber2Error = value;
                OnPropertyChanged("ShowPhoneNumber2Error");
            }
        }

        private string phoneNumber2;

        public string PhoneNumber2
        {
            get => phoneNumber2;
            set
            {
                phoneNumber2 = value;
                ValidatePhoneNumber2();
                OnPropertyChanged("PhoneNumber2");
            }
        }

        private string phoneNumber2Error;

        public string PhoneNumber2Error
        {
            get => phoneNumber2Error;
            set
            {
                phoneNumber2Error = value;
                OnPropertyChanged("PhoneNumber2Error");
            }
        }

        private void ValidatePhoneNumber2()
        {
            if (this.PhoneNumber2.Length < 10)
            {
                this.ShowPhoneNumber2Error = true;
                this.PhoneNumber2Error = ERROR_MESSAGES.BAD_PHONE_NUMBER;
            }
        }
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

        #region Allergies

        private List<Allergy> allAllergies;
        private ObservableCollection<Allergy> filteredAllergies;
        public ObservableCollection<Allergy> FilteredAllergies
        {
            get
            {
                return this.filteredAllergies;
            }
            set
            {
                if (this.filteredAllergies != value)
                {

                    this.filteredAllergies = value;
                    OnPropertyChanged("FilteredAllergies");
                }
            }
        }

        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return this.searchTerm;
            }
            set
            {
                if (this.searchTerm != value)
                {

                    this.searchTerm = value;
                    OnTextChanged(value);
                    OnPropertyChanged("SearchTerm");
                }
            }
        }


        private void InitAllergies()
        {
            IsRefreshing = true;
            App theApp = (App)App.Current;
            this.allAllergies = theApp.LookupTables.Allergies;


            //Copy list to the filtered list
            this.FilteredAllergies = new ObservableCollection<Allergy>(this.allAllergies.OrderBy(a => a.AllergyName));
            SearchTerm = String.Empty;
            IsRefreshing = false;
        }

        private string newAllergy;
        public string NewAllergy
        {
            get => newAllergy;
            set
            {
               newAllergy = value;
                OnPropertyChanged("NewAllergy");
            }
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

        #region RelationToStudent

        private RelationToStudent chosenRelation;

        public RelationToStudent ChosenRelation
        {
            get => chosenRelation;
            set
            {
                chosenRelation = value;
                OnPropertyChanged("ChosenRelation");
            }
        }

        public List<RelationToStudent> RelationTypes
        {
            get
            {
                App theApp = (App)App.Current;
                List<RelationToStudent> relations = new List<RelationToStudent>();
                foreach (RelationToStudent r in theApp.LookupTables.Relations)
                {
                    relations.Add(r);
                }
                return relations;
            }
        }

        private bool showRelationError;

        public bool ShowRelationError
        {
            get => showRelationError;
            set
            {
                showRelationError = value;
                OnPropertyChanged("ShowRelationError");
            }
        }



        private string relationError;

        public string RelationError
        {
            get => relationError;
            set
            {
                relationError = value;
                OnPropertyChanged("RelationError");
            }
        }

        private void ValidateRelation()
        {
            this.ShowRelationError = (ChosenRelation == null);
        }


        #endregion

        //Commands

        #region Search
        public void OnTextChanged(string search)
        {
            //Filter the list of contacts based on the search term
            if (this.allAllergies == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                foreach (Allergy a in this.allAllergies)
                {
                    if (!this.FilteredAllergies.Contains(a))
                        this.FilteredAllergies.Add(a);


                }
            }
            else
            {
                foreach (Allergy a in this.allAllergies)
                {
                    string allergyString = $"{a.AllergyName}";

                    if (!this.FilteredAllergies.Contains(a) &&
                        allergyString.Contains(search))
                        this.FilteredAllergies.Add(a);
                    else if (this.FilteredAllergies.Contains(a) &&
                        !allergyString.Contains(search))
                        this.FilteredAllergies.Remove(a);
                }
            }

            this.FilteredAllergies = new ObservableCollection<Allergy>(this.FilteredAllergies.OrderBy(a => a.AllergyName));
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
            InitAllergies();
        }
        #endregion

        #region AllergySelection
        List<Allergy> selectedAllergies;
        public List<Allergy> SelectedAllergies
        {
            get
            {
                return selectedAllergies;
            }
            set
            {
                if (selectedAllergies != value)
                {
                    selectedAllergies = value;
                }
            }
        }

        public ICommand UpdateAllergy => new Command(OnPressedAllergy);
        public async void OnPressedAllergy(object allergyList)
        {
            if (allergyList is List<Allergy>)
            {
                SelectedAllergies.Clear();
                List<Allergy> alergies = (List<Allergy>)allergyList;
                foreach(Allergy a in alergies)
                {
                    SelectedAllergies.Add(a);
                }
            }
        }
        #endregion

        #region Add New Allergy
        public ICommand AddAllergy => new Command(OnAddAllergy);
        public async void OnAddAllergy()
        {

            if(string.IsNullOrEmpty(NewAllergy))
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "לא ניתן להוסיף ערך זה!", "בסדר");
                return;
            }

            Allergy NewAllrg = new Allergy
            {
                AllergyName = NewAllergy
            };
            
            bool IsExist = false;
            if( allAllergies.Contains(NewAllrg)) { IsExist = true; }

            if (!IsExist)
            {
                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                bool ok = await proxy.AddAllergy(NewAllrg);

                if (ok)
                {
                    await App.Current.MainPage.DisplayAlert("", "הוספת אלרגיה בהצלחה!", "בסדר");
                }
                else if (!ok)
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת אלרגיה נכשלה", "בסדר");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "אלרגיה זו כבר קיימת במערכת", "בסדר");
            }

        }
        #endregion

        #region Register
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateChildID();
            ValidateBirthDate();
            ValidateChildLastName();
            ValidateChildName();
            ValidateCode();
            ValidateEmail();
            ValidateGender();
            ValidatePassword();
            ValidatePhoneNumber();
            ValidateUserName();
            ValidateGrade();


            //check if any validation failed
            if (showBirthDateError || showChildIDError || showChildLastNameError || showChildNameError || showCodeError || showEmailError || showGenderError||
                showGradeError || showPasswordError || showPhoneNumberError || showUserNameError)

                return false;
            return true;
        }

        public ICommand RegisterCommand { protected set; get; }

        public async void Register()
        {
            int groupId = GanCode.CodeToGroupID(Code);
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();

            if (ValidateForm())
            {
                App app = (App)App.Current;
                if ( ValidateForm())
                {
                    User newUser = new User
                    {
                        Email = Email,
                        Password = Password,
                        Fname = UserName,
                        LastName = ChildLastName,
                        PhoneNumber = PhoneNumber,
                    };

                    ServerStatus = "מתחבר לשרת...";
                    await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                    User newU = await proxy.Register(newUser);

                    if (newU != null)
                    {
                        Student newStudent = new Student
                        {
                            FirstName = ChildName,
                            LastName = ChildLastName,
                            BirthDate = BirthDate,
                            StudentId = int.Parse(ChildID),
                            Gender = Gender,
                            GroupId = groupId,
                            GradeId = ChosenGrade.GradeId
                        };

                        //newU.StudentOfUsers.Add(newStudent);
                    }
                    else
                    {
                        //SubmitError = "ההרשמה נכשלה! נסה שנית";
                        //ShowError = true;
                    }
                }
                
               

                
            }

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

            //RegisterCommand = new Command(Register);
            this.SearchTerm = String.Empty;
            InitAllergies();

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
            this.ShowPhoneNumberError = false;
            this.ShowPhoneNumber2Error = false;
            this.ShowCodeError = false;
            this.showGradeError = false;
            this.showRelationError = false;

            this.ChildLastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ChildNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ChildIDError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.BirthDateError = ERROR_MESSAGES.BAD_DATE;
            this.GenderError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.UserNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.CodeError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.GradeError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.RelationError = ERROR_MESSAGES.REQUIRED_FIELD;

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

