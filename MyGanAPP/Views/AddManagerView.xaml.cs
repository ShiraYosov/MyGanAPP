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
    public partial class AddManagerView : ContentPage
    {
        public AddManagerView()
        {
            AddManagerViewModel vm = new AddManagerViewModel();
            vm.SetImageSourceEvent += Vm_SetImageSourceEvent;
            this.BindingContext = vm;
            InitializeComponent();

            vm.ChangeBools();
        }
        private void Vm_SetImageSourceEvent(ImageSource obj)
        {
            theImage.Source = obj;
        }
    }
}