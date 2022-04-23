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
    public partial class ShowPhotoPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ShowPhotoPopup(PhotoGalleryViewModel context)
        {
            context.SetImageSourceEvent += Vm_SetImageSourceEvent;
            this.BindingContext = context;
            InitializeComponent();
        }

        private void Vm_SetImageSourceEvent(ImageSource obj)
        {
            theImage.Source = obj;
        }
    }
}