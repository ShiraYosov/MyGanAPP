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

    public class PhotoGalleryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Event info
        private string eventName;

        public string EventName
        {
            get => eventName;
            set
            {
                eventName = value;
                OnPropertyChanged("EventName");
            }
        }

        private DateTime eventDate;

        public DateTime EventDate
        {
            get => eventDate;
            set
            {
                eventDate = value;
                OnPropertyChanged("EventDate");
            }
        }

        #endregion

        public ObservableCollection<Event> EventList { get; }

        public PhotoGalleryViewModel()
        {
            EventList = new ObservableCollection<Event>();
            CreateCollection();
        }

        private void CreateCollection()
        {

            EventList.Clear();
            App a = (App)App.Current;

          
            foreach (Event ev in a.SelectedGroup.Events)
            {
                this.EventList.Add(ev);
            }


        }

        public ICommand AddEventCommand => new Command(OnAddEvent);
        public async void OnAddEvent()
        {
            await PopupNavigation.Instance.PopAsync();
            if (!string.IsNullOrEmpty(EventName) && EventDate != null)
            {
                App a = (App)App.Current;
                Event e = new Event()
                {
                    EventName = EventName,
                    EventDate = EventDate.Date,
                    Duration = 1,
                    GroupId = a.SelectedGroup.GroupId,
                    Group = a.SelectedGroup
                };

                EventName = "";
                EventDate = DateTime.Now;

                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                Event newEvent = await proxy.AddEvent(e);
               
                if (newEvent == null)
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת אירוע נכשלה", "בסדר");
                }
                else
                {
                    this.EventList.Add(newEvent);
                    a.SelectedGroup.Events.Add(newEvent);
                    await App.Current.MainPage.DisplayAlert("הודעה", "הוספת אירוע צלחה", "בסדר");
                }


            }

            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת אירוע נכשלה", "בסדר");
            }

        }
    }




}

