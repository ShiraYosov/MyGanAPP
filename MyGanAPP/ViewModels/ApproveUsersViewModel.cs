﻿using System;
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

        public ObservableCollection<Student> StudentsList { get; }
        public ObservableCollection<User> TeachersList { get; }

        public ApproveUsersViewModel()
        {
            StudentsList = new ObservableCollection<Student>();
            TeachersList = new ObservableCollection<User>();
            CreateCollection();
        }

        private void CreateCollection()
        {
            App a = (App)App.Current;

            
            if(a.SelectedGroup != null)
            {
               foreach (Student s in a.SelectedGroup.Students)
               {
                 foreach(StudentOfUser sou in s.StudentOfUsers)
                    {
                        if(sou.User.IsApproved == false)
                            this.StudentsList.Add(s);
                    }
               }

            }

            if(a.SelectedKindergarten!= null)
            {
                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
               ICollection<User> teachers =  (ICollection<User>) proxy.GetTeachersAsync(a.SelectedKindergarten);

                foreach(User user in teachers)
                {
                    TeachersList.Add(user); 
                }
            }
           

            
        }
    }
}
