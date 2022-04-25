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
using Rg.Plugins.Popup.Services;

namespace MyGanAPP.ViewModels
{
    public class ApproveUsersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public const int UNPERMITTED_STATUS = 1;
        public const int PERMITTED_STATUS = 2;
        public const int WAITING_STATUS = 3;



        public ObservableCollection<StudentOfUser> StudentOfUsersList { get; }
        public ObservableCollection<PendingTeacher> TeachersList { get; }

        #region Visibility
        private bool visible1;

        public bool Visible1
        {
            get => visible1;
            set
            {
                visible1 = value;
                OnPropertyChanged("Visible1");
            }
        }

        private bool visible2;
        public bool Visible2
        {
            get => visible2;
            set
            {
                visible2 = value;
                OnPropertyChanged("Visible2");
            }
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
            CreateCollection();
        }
        #endregion

        #region ShowUserPopup

        #region Student
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

        private string grade;
        public string Grade
        {
            get => grade;
            set
            {
                grade = value;
                OnPropertyChanged("Grade");
            }
        }

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

        private string genderName;
        public string GenderName
        {
            get => genderName;
            set
            {
                genderName = value;
                OnPropertyChanged("GenderName");
            }
        }

        private bool studentVisible;

        public bool StudentVisible
        {
            get => studentVisible;
            set
            {
                studentVisible = value;
                OnPropertyChanged("StudentVisible");
            }
        }
        #endregion

        #region User
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

        private string userLastName;
        public string UserLastName
        {
            get => userLastName;
            set
            {
                userLastName = value;
                OnPropertyChanged("UserLastName");
            }
        }

        private string relationType;
        public string RelationType
        {
            get => relationType;
            set
            {
                relationType = value;
                OnPropertyChanged("RelationType");
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

        #endregion

        private object selectedItem;
        public object SelectedItem
        {
            get => selectedItem;
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }

            }
        }
        public ApproveUsersViewModel()
        {
            StudentOfUsersList = new ObservableCollection<StudentOfUser>();
            TeachersList = new ObservableCollection<PendingTeacher>();
            CreateCollection();
        }

        public ICommand SelectionChanged => new Command(OnSelection);
        public void OnSelection()
        {

            if (SelectedItem != null)
            {

                if (SelectedItem is StudentOfUser)
                {
                    StudentOfUser selectedStudent = (StudentOfUser)SelectedItem;
                    this.StudentVisible = true;
                    this.StudentName = selectedStudent.Student.FirstName;
                    this.GenderName = selectedStudent.Student.Gender;
                    this.UserImgSrc = selectedStudent.Student.PhotoURL;
                    this.GroupName = selectedStudent.Student.Group.GroupName;
                    this.StudentID = selectedStudent.Student.StudentId;
                    this.Grade = selectedStudent.Student.Grade.GradeName;

                    this.UserName = selectedStudent.User.Fname;
                    this.UserLastName = selectedStudent.User.LastName;
                    this.relationType = selectedStudent.RelationToStudent.RelationType;
                }

                else if (SelectedItem is PendingTeacher)
                {
                    PendingTeacher selectedTeacher = (PendingTeacher)SelectedItem;
                    this.StudentVisible = false;
                    this.UserImgSrc = selectedTeacher.User.PhotoURL;
                    this.GroupName = selectedTeacher.Group.GroupName;
                    this.UserLastName = selectedTeacher.User.LastName;
                    this.UserName = selectedTeacher.User.Fname;
                }
                PopupNavigation.Instance.PushAsync(new ShowUserPopup(this));
                SelectedItem = null;
            }
        }

        private async void CreateCollection()
        {

            App a = (App)App.Current;
            IsRefreshing = true;

            if (a.SelectedGroup != null)
            {
                Visible1 = true;
                StudentOfUsersList.Clear();

                foreach (Student s in a.SelectedGroup.Students)
                {
                    foreach (StudentOfUser sou in s.StudentOfUsers)
                    {
                        if (sou.StatusId == WAITING_STATUS)
                            this.StudentOfUsersList.Add(sou);
                    }

                }
            }
            else { Visible1 = false; }


            if (a.SelectedKindergarten != null && a.SelectedGroup == null)
            {
                Visible2 = true;
                TeachersList.Clear();

                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                List<PendingTeacher> teachers = await proxy.GetTeachersWithWaitStatusAsync(a.SelectedKindergarten.KindergartenId);

                foreach (PendingTeacher user in teachers)
                {
                    TeachersList.Add(user);
                }
            }
            else { Visible2 = false; }

            isRefreshing = false;
        }

        public ICommand DisapproveCommand => new Command(OnDisapprove);
        public async void OnDisapprove(object obj)
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();

            if (obj is PendingTeacher)
            {
                PendingTeacher u = (PendingTeacher)obj;
                u.StatusId = UNPERMITTED_STATUS;

                bool ok = await proxy.ChangeUserStatus(u);
                if (ok)
                {
                    OnRefresh();
                    ((App)App.Current).UIRefresh();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "פעולה נכשלה!", "בסדר");
                }
            }

            else if (obj is StudentOfUser)
            {
                StudentOfUser u = (StudentOfUser)obj;
                u.StatusId = UNPERMITTED_STATUS;

                bool ok = await proxy.ChangeUserStatus(u);
                if (ok)
                {
                    OnRefresh();
                    ((App)App.Current).UIRefresh();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "פעולה נכשלה!", "בסדר");

                }
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "פעולה נכשלה!", "בסדר");

            }
        }


        public ICommand ApproveCommand => new Command(OnApprove);
        public async void OnApprove(object obj)
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();

            if (obj is PendingTeacher)
            {
                PendingTeacher u = (PendingTeacher)obj;
                u.StatusId = PERMITTED_STATUS;

                bool ok = await proxy.ChangeUserStatus(u);
                if (ok)
                {
                    Models.Group ToDelete = ((App)App.Current).CurrUser.Groups.Where(g => g.GroupId == u.GroupId).FirstOrDefault();
                    if(ToDelete != null)
                    {
                        ((App)App.Current).CurrUser.Groups.Where(g => g.GroupId == ToDelete.GroupId).FirstOrDefault().Teacher = u.User;
                        ((App)App.Current).CurrUser.Groups.Remove(ToDelete);
                        OnRefresh();
                        ((App)App.Current).UIRefresh();
                    }
                 
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "פעולה נכשלה!", "בסדר");
                }
            }

            else if (obj is StudentOfUser)
            {
                StudentOfUser u = (StudentOfUser)obj;
                u.StatusId = PERMITTED_STATUS;

                bool ok = await proxy.ChangeUserStatus(u);
                if (ok)
                {
                    OnRefresh();
                    ((App)App.Current).UIRefresh();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "פעולה נכשלה!", "בסדר");
                    //await App.Current.MainPage.Navigation.PopAsync();
                }
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "פעולה נכשלה!", "בסדר");
                //await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }
    }
}
