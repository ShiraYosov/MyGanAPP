using System;
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
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            this.BindingContext = new LoginViewModel();
            this.Title = "התחברות";
            InitializeComponent();
            
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    App a = (App)App.Current;
        //    a.SelectedGroup = null;
        //    a.SelectedKindergarten = null;
        //    a.SelectedStudent = null;
        //}

        private void Parent_Pressed(object sender, EventArgs e)
        {
            App a = (App)App.Current;
            a.SelectedGroup = null;
            a.SelectedKindergarten = null;
            a.SelectedStudent = null;

            App.Current.MainPage.Navigation.PushAsync(new ParentRegistrationView());
        }

        private void Teacher_Preseed(object sender, EventArgs e)
        {
            App a = (App)App.Current;
            a.SelectedGroup = null;
            a.SelectedKindergarten = null;
            a.SelectedStudent = null;

            App.Current.MainPage.Navigation.PushAsync(new AddTeacherView());
        }

        private void Manager_Pressed(object sender, EventArgs e)
        {
            App a = (App)App.Current;
            a.SelectedGroup = null;
            a.SelectedKindergarten = null;
            a.SelectedStudent = null;

            App.Current.MainPage.Navigation.PushAsync(new AddManagerView());
        }
    }
}