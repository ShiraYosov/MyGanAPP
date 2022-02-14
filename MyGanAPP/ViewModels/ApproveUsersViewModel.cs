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
    class ApproveUsersViewModel : INotifyPropertyChanged
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
        public ObservableCollection<User> TeachersList { get; }

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

        public ApproveUsersViewModel()
        {
            StudentOfUsersList = new ObservableCollection<StudentOfUser>();
            TeachersList = new ObservableCollection<User>();
            CreateCollection();
        }

        public ICommand SelectionChanged => new Command(OnSelection);
        public async void OnSelection(object obj)
        {
           
           
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
                        if (sou.User.StatusId == WAITING_STATUS)
                            this.StudentOfUsersList.Add(sou);
                    }
                    
                }
            }
            else { Visible1 = false; }


            if (a.SelectedKindergarten != null)
            {
                Visible2 = true;
                TeachersList.Clear();

                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                List<User> teachers = await proxy.GetTeachersWithWaitStatusAsync(a.SelectedKindergarten.KindergartenId);

                foreach (User user in teachers)
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

            if (obj is User)
            {
                User u = (User)obj;
                u.StatusId = UNPERMITTED_STATUS;
                bool ok = await proxy.ChangeUserStatus(u);
                if (ok) { OnRefresh(); }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "פעולה נכשלה!", "בסדר");
                }
            }

            else if (obj is StudentOfUser)
            {
                StudentOfUser u = (StudentOfUser)obj;
                u.User.StatusId = UNPERMITTED_STATUS;
                bool ok = await proxy.ChangeUserStatus(u.User);
                if (ok) { OnRefresh(); }
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
    

        public ICommand ApproveCommand => new Command(OnApprove);
        public async void OnApprove(object obj)
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();

            if (obj is User)
            {
                User u = (User)obj;
                u.StatusId = PERMITTED_STATUS;
                bool ok = await proxy.ChangeUserStatus(u);
                if (ok) { OnRefresh(); }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "פעולה נכשלה!", "בסדר");
                }
            }

            else if (obj is StudentOfUser)
            {
                StudentOfUser u = (StudentOfUser)obj;
                u.User.StatusId = PERMITTED_STATUS;
                bool ok = await proxy.ChangeUserStatus(u.User);
                if (ok) { OnRefresh(); }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "פעולה נכשלה!", "בסדר");
                    //await App.Current.MainPage.Navigation.PopAsync();
                }
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "פעולה נכשלה!", "בסדר");
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }
    }
}
