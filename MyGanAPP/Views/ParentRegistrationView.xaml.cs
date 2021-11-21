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
    public partial class ParentRegistrationView : ContentPage
    {
       
        public ParentRegistrationView()
        {
            ParentRegistrationViewModel vm = new ParentRegistrationViewModel();
            vm.SetImageSourceEvent += Vm_SetImageSourceEvent;
            InitializeComponent();
            this.BindingContext = vm;
        }

        private void Vm_SetImageSourceEvent(ImageSource obj)
        {
            theImage.Source = obj;
        }
    }
}