﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGanAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyGanAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagerProfileView : ContentPage
    {
        public ManagerProfileView()
        {
            InitializeComponent();
            this.BindingContext = new ManagerProfileViewModel();
        }
    }
}