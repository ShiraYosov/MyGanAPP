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

        
        public ObservableCollection<Student> ChildrenList { get;}
        public ObservableCollection<Group> GroupsList { get;}
        public ObservableCollection<Kindergarten> KindergartensList { get;}

        public ChooseKidViewModel()
        {
            ChildrenList = new ObservableCollection<Student>();
            GroupsList = new ObservableCollection<Group>();
            KindergartensList = new ObservableCollection<Kindergarten>();
            CreateCollection();
        }

        private void CreateCollection()
        {
            App a = (App)App.Current;
           
            List<StudentOfUser> theStudents = a.CurrUser.StudentOfUsers;
            foreach (StudentOfUser s in theStudents)
            {
                this.ChildrenList.Add(s.Student);
            }

            List<Group> theGroups = a.CurrUser.Groups;
            foreach (Group g in theGroups )
            {
                this.GroupsList.Add(g);
            }

            List<KindergartenManager> Kindergartens = a.CurrUser.KindergartenManagers;
            foreach (KindergartenManager k in Kindergartens)
            {
                //Kindergarten onList= 
                this.KindergartensList.Add(k.Kindergarten);
            }
        }

        public ICommand SelctionChanged => new Command<Object>(OnSelection);
        public void OnSelection(Object obj)
        {
            if (obj is Kindergarten)
            {
                Kindergarten chosenKindergarten = (Kindergarten)obj;
                //Page monkeyPage = new ShowMonkey();
                //ShowMonkeyViewModel monkeyContext = new ShowMonkeyViewModel
                //{
                //    Name = chosenMonkey.Name,
                //    ImageUrl = chosenMonkey.ImageUrl,
                //    Details = chosenMonkey.Details
                //};
                //monkeyPage.BindingContext = monkeyContext;
                //monkeyPage.Title = monkeyContext.Name;
                //if (NavigateToPageEvent != null)
                //    NavigateToPageEvent(monkeyPage);
            }
        }

    }

   

}
