using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGanAPP.ViewModels;
using MyGanAPP.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyGanAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowGroupView : ContentPage
    {
        public ShowGroupView()
        {
            this.BindingContext = new ShowGroupViewModel(); 
            InitializeComponent();
        }

        private async void ApproveStudents_Clicked(object sender, EventArgs e)
        {
            App a = (App)App.Current;
           await App.Current.MainPage.Navigation.PushAsync(new ApproveUsersView());
        }
    }
}