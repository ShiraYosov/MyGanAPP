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
using Rg.Plugins.Popup.Services;

namespace MyGanAPP.ViewModels
{
    public class UserPhotosViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Photo> Photos { get; set; } 
        public UserPhotosViewModel()
        {
            Photos = new ObservableCollection<Photo>();
            CreateCollection();  
        }

        private void CreateCollection()
        {
            Photos.Clear();
            App theApp = (App)App.Current;

            if(theApp.CurrUser != null && theApp.SelectedStudent != null)
            {
                foreach(Photo p in theApp.CurrUser.Photos)
                {
                    if(p.Name == $"{theApp.SelectedStudent.FirstName} {theApp.SelectedStudent.LastName}")
                        Photos.Add(p);
                }
            }

            else if(theApp.CurrUser != null)
            {
                foreach (Photo p in theApp.CurrUser.Photos)
                {
                    if(p.Event.GroupId == theApp.SelectedGroup.GroupId)
                    {
                        Photos.Add(p);
                    }
                   
                }
            }
        }

        public ICommand DeletePhotoCommand => new Command(OnDeletePhoto);
        public async void OnDeletePhoto(object p)
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            App a = (App)App.Current;
            if (p is Photo)
            {
                Photo ph = (Photo)p;
                bool success = await proxy.DeletePhoto(ph.Id);

                if (success)
                {
                    a.SelectedGroup.Events.Where(e => e.EventId == ph.EventId).FirstOrDefault().Photos.Remove(ph);
                    a.CurrUser.Photos.Remove(ph);
                    CreateCollection();
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "מחיקת תמונה נכשלה", "בסדר");
                }
            }

        }

        public ICommand EditPhotoCommand => new Command(OnEditPhoto);
        public async void OnEditPhoto(object p)
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            App a = (App)App.Current;
            if (p is Photo)
            {
                Photo ph = (Photo)p;
                bool success = await proxy.DeletePhoto(ph.Id);

                if (success)
                {
                    a.SelectedGroup.Events.Where(e => e.EventId == ph.EventId).FirstOrDefault().Photos.Remove(ph);
                    a.CurrUser.Photos.Remove(ph);
                    CreateCollection();
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "מחיקת תמונה נכשלה", "בסדר");
                }
            }

        }

        public ICommand TapPhotoCommand => new Command(OnTapPhoto);
        public async void OnTapPhoto(object pic)
        {
            if (pic != null)
            {
                await PopupNavigation.Instance.PushAsync(new UserPhotoPopup(this));
            }

        }


    }
}
