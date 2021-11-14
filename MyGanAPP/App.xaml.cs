
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyGanAPP.Models;
using MyGanAPP.Services;
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
        public User CurrUser { get; set; }
        public Lookups LookupTables { get; set; }
        public App()
        {
            InitializeComponent();

            CurrUser = null;

            MainPage = new NavigationPage(new HomePageView());

        }

        public static bool IsDevEnv
        {
            get
            {
                return true;
            }
        }
        protected async override void OnStart()
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            this.LookupTables = await proxy.GetLookupsAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
