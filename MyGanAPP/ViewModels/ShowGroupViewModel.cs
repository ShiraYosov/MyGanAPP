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
    internal class ShowGroupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private const string Source1 = "arrow.png";
        private const string Source2 = "arrow2.png";
        public const int PERMITTED_STATUS = 2;

        #region ImgButton
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

        #region GroupCode
        private string groupCode;
        public string GroupCode
        {
            get => groupCode;
            set
            {
                groupCode = value;
                OnPropertyChanged("GroupCode");
            }
        }
        #endregion

        #region NumberOfStudents
        private int numberOfStudents;
        public int NumberOfStudents
        {
            get => numberOfStudents;
            set
            {
                numberOfStudents = value;
                OnPropertyChanged("NumberOfStudents");
            }
        }
        #endregion

        #region TeacherDetails
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
        #region TeacherFirstName

        private string teacherFirstName;

        public string TeacherFirstName
        {
            get => teacherFirstName;
            set
            {
                teacherFirstName = value;
                OnPropertyChanged("TeacherFirstName");
            }
        }


        #endregion
        #region TeacherLastName
        private string teacherLastName;

        public string TeacherLastName
        {
            get => teacherLastName;
            set
            {
                teacherLastName = value;
                OnPropertyChanged("TeacherLastName");
            }
        }


        #endregion

        #endregion

        #region SelectedStudent
        #region StudentImgSrc
        private string studentImgSrc;

        public string StudentImgSrc
        {
            get => studentImgSrc;
            set
            {
                studentImgSrc = value;
                OnPropertyChanged("StudentImgSrc");
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

        #region StudentLastName

        private string studentLastName;

        public string StudentLastName
        {
            get => studentLastName;
            set
            {
                studentLastName = value;
                OnPropertyChanged("StudentLastName");
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

        #endregion
        public ObservableCollection<Student> StudentsList { get; }

        public ShowGroupViewModel()
        {
            App theApp = (App)App.Current;

            StudentsList = new ObservableCollection<Student>();
            User Teacher = theApp.SelectedGroup.Teacher;
            UserImgSrc = Teacher.PhotoURL;
            Email=Teacher.Email;
            PhoneNumber = Teacher.PhoneNumber;
            TeacherFirstName = Teacher.Fname;
            TeacherLastName = Teacher.LastName;
            GroupCode = GanCode.CreateGroupCode(theApp.SelectedGroup.GroupId);

            Button1 = false;
            Button2 = false;
            ImgSource1 = Source1;
            ImgSource2 = Source1;
            Button1PressedCommand = new Command(Button1Pressed);
            Button2PressedCommand = new Command(Button2Pressed);

            CreatCollection();
        }

        public Command<Student> SelectionChanged => new Command<Student>(OnSelection);
        public void OnSelection(object s)
        {
            if(s != null)
            {
                Student student = (Student)s;    

                StudentImgSrc = student.PhotoURL;
                StudentName = student.FirstName;
                StudentLastName = student.LastName;
                Gender = student.Gender;
                BirthDate = student.BirthDate.ToShortDateString();

                Allergies = "";
                foreach (StudentAllergy sa in student.StudentAllergies)
                {
                    Allergies += sa.Allergy.AllergyName + "," + " ";
                }

                if (student.StudentAllergies.Count == 0) { Allergies = "לא נבחרו אלרגיות"; }
                else
                    Allergies = Allergies.Substring(0, Allergies.Length - 2);

                App.Current.MainPage.Navigation.PushAsync(new StudentView(this));
            }
        }

            private void CreatCollection()
        {
            App theApp = (App)App.Current;
            bool IsApproved = false;
            foreach (Student student in theApp.SelectedGroup.Students)
            {
                foreach(StudentOfUser sou in student.StudentOfUsers)
                {
                    if (sou.StatusId == PERMITTED_STATUS)
                        IsApproved = true;  
                }
               
                if(IsApproved)  
                StudentsList.Add(student);

                IsApproved = false;
            }

            NumberOfStudents = StudentsList.Count();
            GroupName = theApp.SelectedGroup.GroupName;
        }

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
    }
}
