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
    public partial class ManagerProfileView : ContentPage
    {
        ManagerProfileViewModel context;
        public ManagerProfileView()
        {
            context = new ManagerProfileViewModel();
            this.BindingContext = context;
            InitializeComponent();

        }


        private void Update_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new LoginView());
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    context.OnRefresh();

        //}
    }
}