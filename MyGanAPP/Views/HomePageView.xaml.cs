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
    public partial class HomePageView : ContentPage
    {
        public HomePageView()
        {
            InitializeComponent();
            this.BindingContext = new HomePageViewModel();
        }

        private void Login_Pressed(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new LoginView());
        }
    }
}