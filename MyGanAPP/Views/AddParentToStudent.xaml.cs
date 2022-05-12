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
    public partial class AddParentToStudent : Rg.Plugins.Popup.Pages.PopupPage
    {
        public AddParentToStudent(Object context)
        {
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}