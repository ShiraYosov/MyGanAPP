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
    internal class ParentProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



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
            User parent = a.CurrUser;
            Student student = a.SelectedStudent;

            if (parent != null && student != null)
            {
                UserImgSrc = student.PhotoURL;
                UserName = parent.Fname;
                LastName = parent.LastName;
                Email = parent.Email;
                PhoneNumber = parent.PhoneNumber;
                GroupName = student.Group.GroupName;

                Allergies = "";
                foreach (StudentAllergy sa in student.StudentAllergies)
                {
                    Allergies += sa.Allergy.AllergyName + "," + " ";
                }

                if (student.StudentAllergies.Count == 0) { Allergies = "לא נבחרו אלרגיות"; }
                else
                    Allergies = Allergies.Substring(0, Allergies.Length - 2);
            }
            IsRefreshing = false;
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

        #endregion

        #region ParentFirstName

        private string userName;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }


        #endregion

        #region LastName

        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
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

        #region StudentName

        private string studentName;

        public string StudentName
        {
            get => studentName;
            set
            {
                studentName = value;
                OnPropertyChanged("StudentName");
            }
        }


        #endregion

        #region StudentID

        private string studentID;

        public string StudentID
        {
            get => studentID;
            set
            {
                studentID = value;
                OnPropertyChanged("StudentID");
            }
        }


        #endregion

        #region BirthDate


        private string birthDate;

        public string BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;

                OnPropertyChanged("BirthDate");
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

        #region Gender

        private string gender;

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;

                OnPropertyChanged("Gender");
            }
        }


        #endregion

        #region Grade

        private string gradeName;

        public string GradeName
        {
            get => gradeName;
            set
            {
                gradeName = value;
                OnPropertyChanged("GradeName");
            }
        }


        #endregion

        #region Allergies
        private string allergies;
        public string Allergies
        {
            get => allergies;
            set
            {
                allergies = value;
                OnPropertyChanged("Allergies");
            }
        }
        #endregion

        public ParentProfileViewModel()
        {
            IsRefreshing = false;

            App a = (App)App.Current;
            User parent = a.CurrUser;
            Student student = a.SelectedStudent;

            if (parent != null && student != null)
            {
                UserImgSrc = student.PhotoURL;
                UserName = parent.Fname;
                LastName = parent.LastName;
                Email = parent.Email;
                PhoneNumber = parent.PhoneNumber;
                GroupName = student.Group.GroupName;

                StudentName = student.FirstName;
                StudentID = student.StudentId;
                BirthDate = student.BirthDate.ToShortDateString();
                Gender = student.Gender;

                GradeName = student.Grade.GradeName;
                //Create allergy string
                foreach (StudentAllergy sa in student.StudentAllergies)
                {
                    Allergies += sa.Allergy.AllergyName + "," + " ";
                }

                if (student.StudentAllergies.Count == 0) { Allergies = "לא נבחרו אלרגיות"; }
                else
                    Allergies = Allergies.Substring(0, Allergies.Length - 2);
            }
           ((App)App.Current).RefreshUI += OnRefresh;

        }


        public ICommand SaveCommand => new Command(OnSave);

        public async void OnSave()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.ParentRegistrationView());
        }

        public ICommand LogOutCommand => new Command(OnLogout);

        public async void OnLogout()
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            bool ok = await proxy.Logout();

        }
    }
}
