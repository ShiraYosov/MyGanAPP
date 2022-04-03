using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGanAPP.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyGanAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KindergartenGroupsView : ContentPage
    {
        public KindergartenGroupsView()
        {
            this.BindingContext = new KindergartenGroupsViewModel();
            InitializeComponent();
        }

      
        private void AddGroup_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AddGroupPopup(this.BindingContext));
        }

        private void AddManager_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AddManagerToKindergartenView(this.BindingContext));
        }
    }
}