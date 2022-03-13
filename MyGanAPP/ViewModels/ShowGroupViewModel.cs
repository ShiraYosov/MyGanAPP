using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using MyGanAPP.Services;
using MyGanAPP.Models;
using Xamarin.Essentials;
using System.Linq;
using MyGanAPP.Views;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Collections;

namespace MyGanAPP.ViewModels
{
    internal class ShowGroupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private const string Source1 = "arrow.png";
        private const string Source2 = "arrow2.png";

        #region ImgButton
        private string imgSource1;
        public string ImgSource1
        {
            get { return this.imgSource1; }

            set
            {
                if (this.imgSource1 != value)
                {
                    this.imgSource1 = value;
                    OnPropertyChanged(nameof(ImgSource1));
                }
            }
        }

        private bool button1;
        public bool Button1
        {
            get { return this.button1; }

            set
            {
                if (this.button1 != value)
                {
                    this.button1 = value;
                    OnPropertyChanged(nameof(Button1));
                }
            }
        }

        #endregion

        public ICommand Button1PressedCommand { protected set; get; }
        public void Button1Pressed()
        {
            if (Button1 == false) { Button1 = true; }
            else { Button1 = false; }

            if (ImgSource1 == Source1) { ImgSource1 = Source2; }
            else { ImgSource1 = Source1; }
        }
    }
}
