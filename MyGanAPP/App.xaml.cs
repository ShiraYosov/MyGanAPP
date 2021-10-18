
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyGanAPP.Services;
using MyGanAPP.Models;
using System.Threading.Tasks;
using MyGanAPP.Views;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace MyGanAPP
{
    public partial class App : Application, INotifyPropertyChanged
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
