using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyGanAPP.Models;
using MyGanAPP.Views;
using System.Collections.Generic;

namespace MyGanAPP
{
    public partial class App : Application
    {
        public User User { get; set; }
        public App()
        {
            InitializeComponent();
            User = null;
            
            MainPage = new LoginView();
           
        }

        public static bool IsDevEnv
        {
            get
            {
                return true;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
