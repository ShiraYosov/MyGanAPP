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
    public partial class PhotoGalleryView : ContentPage
    {
        public PhotoGalleryView()
        {
            PhotoGalleryViewModel viewModel = new PhotoGalleryViewModel();
            this.BindingContext = viewModel;
            InitializeComponent();
        }

        private void AddEvent_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AddEventPopup(this.BindingContext));
        }

        private void MyPhotos_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new UserPhotosView(this.BindingContext));
        }
    }
}