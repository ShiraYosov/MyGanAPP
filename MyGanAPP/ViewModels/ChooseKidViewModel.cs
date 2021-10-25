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
    class ChooseKidViewModel: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        public ObservableCollection<Student> ChildrenList { get; set; }
        public ObservableCollection<Group> GroupsList { get; set; }
        public ObservableCollection<Kindergarten> KindergartensList { get; set; }

        public ChooseKidViewModel()
        {
            ChildrenList = new ObservableCollection<Student>();
            GroupsList = new ObservableCollection<Group>();
            KindergartensList = new ObservableCollection<Kindergarten>();
        }

        private void CreateStudentCollection()
        {
            App a = (App)App.Current;
           
            List<StudentOfUser> theStudents = a.User.StudentOfUsers;
            foreach (StudentOfUser s in theStudents)
            {
                this.ChildrenList.Add(s.Student);
            }

            List<Group> theGroups = a.User.Groups;
            foreach (Group g in theGroups )
            {
                this.GroupsList.Add(g);
            }

            List<KindergartenManager> Kindergartens = a.User.KindergartenManagers;
            foreach (KindergartenManager k in Kindergartens)
            {
                this.KindergartensList.Add(k.Kindergarten);
            }
        }

    }

   

}
