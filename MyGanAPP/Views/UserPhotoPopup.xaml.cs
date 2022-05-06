using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyGanAPP.ViewModels;

namespace MyGanAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPhotoPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public UserPhotoPopup(object context)
        {
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}