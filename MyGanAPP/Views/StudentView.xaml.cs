using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGanAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace MyGanAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentView : ContentPage
    {
        public StudentView(object context)
        {
            this.Title = "פרטי תלמיד";
            this.BindingContext = context;
            InitializeComponent();
        }

        private void AddParent_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AddParentToStudent(this.BindingContext));
        }
    }
}