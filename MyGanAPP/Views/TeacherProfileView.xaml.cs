﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGanAPP.Services;
using MyGanAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyGanAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherProfileView : ContentPage
    {
        public TeacherProfileView()
        {
            this.BindingContext = new TeacherProfileViewModel();
            InitializeComponent();
        }

        private  void LogOut_Clicked(object sender, EventArgs e)
        {
            App a = (App)App.Current;
            a.SelectedGroup = null;
            a.SelectedKindergarten = null;
            a.SelectedStudent = null;
            a.CurrUser = null;

           App.Current.MainPage.Navigation.PopToRootAsync();
        }

    }
}