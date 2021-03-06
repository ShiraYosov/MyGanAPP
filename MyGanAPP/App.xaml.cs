
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
        public Student SelectedStudent { get; set; }
        public Kindergarten SelectedKindergarten { get; set; }
        public Group SelectedGroup { get; set; }

        public event Action RefreshUI;

        public Lookups LookupTables { get; set; }
        public App()
        {
            
            InitializeComponent();

            CurrUser = null;
            SelectedStudent = null;
            SelectedGroup = null;
            SelectedKindergarten = null;
            
            ViewModels.ServerStatusPageViewModel vm = new ViewModels.ServerStatusPageViewModel();
            vm.ServerStatus = "מתחבר לשרת...";
            MainPage = new Views.ServerStatusPage(vm);

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
            if (LookupTables != null)
            {
                MainPage = new NavigationPage(new LoginView());
            }
            else
            {
                ViewModels.ServerStatusPageViewModel vm = new ViewModels.ServerStatusPageViewModel();
                vm.ServerStatus = "אירעה שגיאה בהתחברות לשרת";
                MainPage = new Views.ServerStatusPage(vm);
            }

        }

        public void UIRefresh() { this.RefreshUI.Invoke(); }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
