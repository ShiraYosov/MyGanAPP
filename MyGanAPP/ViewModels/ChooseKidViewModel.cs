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




        public ObservableCollection<ImageSource> ChildrenPicsList { get; set; }


        public ChooseKidViewModel()
        {
            ChildrenPicsList = new ObservableCollection<ImageSource>();

        }

        private void CreateQuestionCollection()
        {
            App a = (App)App.Current;
            List<StudentOfUser> theStudents = a.User.StudentOfUsers;
            foreach (StudentOfUser s in theStudents)
            {
                this.ChildrenPicsList.Add(s.Student.PhotoURL);
            }
        }

    }

   

}
