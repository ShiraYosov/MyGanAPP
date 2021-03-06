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
    public partial class ShowUserPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ShowUserPopup(ApproveUsersViewModel context)
        {
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}