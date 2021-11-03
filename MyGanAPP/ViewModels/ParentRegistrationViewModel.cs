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
        public const string BAD_HOUSE_NUM = "מספר בית לא תקין";
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
        #endregion

        #region imgChange
        private string imgSource;
        public string ImgSource
        {
            get { return this.imgSource; }

            set
            {
                if (this.imgSource != value)
                {
                    this.imgSource = value;
                    OnPropertyChanged(nameof(ImgSource));
                }
            }
        }
        #endregion
        public ParentRegistrationViewModel()
        {
            Button1 = false;
            ImgSource = Source1;
            ButtonPressedCommand = new Command(ButtonPressed);
            this.BirthDate = DateTime.Today;

            this.ShowChildNameError = false;
            this.ShowChildLastNameError = false;
            this.ShowChildIDError = false;
            this.ShowBirthDateError = false;

            this.ChildLastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ChildNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ChildIDError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.BirthDateError = ERROR_MESSAGES.BAD_DATE;


        }

        public ICommand ButtonPressedCommand { protected set; get; }
        public void ButtonPressed()
        {
            if (Button1 == false) { Button1 = true; }
            else { Button1 = false; }

            if (ImgSource == Source1) { ImgSource = Source2; }
            else { ImgSource = Source1; }
        }
    }
}

