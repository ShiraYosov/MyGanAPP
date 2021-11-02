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
        public const string BAD_DATE = "עלייך להיות מעל גיל 18";
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

            this.ShowChildNameError = false;
            this.ShowChildLastNameError = false;

            this.ChildLastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ChildNameError = ERROR_MESSAGES.REQUIRED_FIELD;


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

