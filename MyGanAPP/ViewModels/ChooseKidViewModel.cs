using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MyGanAPP.Services;
using System.Text.Json;
using MyGanAPP.Models;
using MyGanAPP.Views;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace MyGanAPP.ViewModels
{
    public class ChooseKidViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public const int PERMITTED_STATUS = 2;
        public ObservableCollection<Student> ChildrenList { get; }
        public ObservableCollection<Group> GroupsList { get; }
        public ObservableCollection<Kindergarten> KindergartensList { get; }

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

        private bool visible3;
        public bool Visible3
        {
            get => visible3;
            set
            {
                visible3 = value;
                OnPropertyChanged("Visible3");
            }
        }
        #endregion

        #region MessageVisibility
       
        private bool messageVisible;

        public bool MessageVisible
        {
            get => messageVisible;
            set
            {
                messageVisible = value;
                OnPropertyChanged("MessageVisible");
            }
        }
        #endregion
        public ChooseKidViewModel()
        {
            Visible1 = false; Visible2 = false; Visible3 = false; MessageVisible = false;
            ChildrenList = new ObservableCollection<Student>();
            GroupsList = new ObservableCollection<Group>();
            KindergartensList = new ObservableCollection<Kindergarten>();
            CreateCollection();
        }

        private void CreateCollection()
        {
            App a = (App)App.Current;

            ICollection<StudentOfUser> theStudents = a.CurrUser.StudentOfUsers;
            foreach (StudentOfUser s in theStudents)
            {
                if(s.StatusId == PERMITTED_STATUS)
                this.ChildrenList.Add(s.Student);
            }
            if (this.ChildrenList.Count > 0) { Visible1 = true; }

            ICollection<Group> theGroups = a.CurrUser.Groups;
            foreach (Group g in theGroups)
            {
               this.GroupsList.Add(g);
            }
            if (this.GroupsList.Count > 0) { Visible2 = true; }

            ICollection<KindergartenManager> Kindergartens = a.CurrUser.KindergartenManagers;
            foreach (KindergartenManager k in Kindergartens)
            {
                this.KindergartensList.Add(k.Kindergarten);
            }
            if (this.KindergartensList.Count > 0) { Visible3 = true; }

            if (!Visible1 && !Visible2 && !Visible3)
            {
                MessageVisible = true;
            }
                
        }

        public ICommand SelectionChanged => new Command(OnSelection);
        public async void OnSelection(object obj)
        {
            App a = (App)App.Current;
            a.SelectedGroup = null;
            a.SelectedKindergarten = null;
            a.SelectedStudent = null;

            if (obj is Group)
            {
                Group chosenGroup = (Group)obj;
                a.SelectedGroup = chosenGroup;
                await App.Current.MainPage.Navigation.PopToRootAsync();
                await App.Current.MainPage.Navigation.PushModalAsync(new TeacherMainTab());
            }
            if (obj is Kindergarten)
            {
                Kindergarten chosenKindergarten = (Kindergarten)obj;
                a.SelectedKindergarten = chosenKindergarten;
                await App.Current.MainPage.Navigation.PopToRootAsync();
                await App.Current.MainPage.Navigation.PushModalAsync(new ManagerMainTab());
            }
            if (obj is Student)
            {
                Student chosenStudent = (Student)obj;
                a.SelectedStudent = chosenStudent;
                await App.Current.MainPage.Navigation.PopToRootAsync();
                await App.Current.MainPage.Navigation.PushModalAsync(new ParentMainTab());
            }

        }

    }



}
