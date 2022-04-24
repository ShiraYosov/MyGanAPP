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

        private Event selectedEvent;

        public Event SelectedEvent
        {
            get => selectedEvent;
            set
            {
                selectedEvent = value;
                OnPropertyChanged("SelectedEvent");
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

        #region Photo description
        private string photoDescription;

        public string PhotoDescription
        {
            get => photoDescription;
            set
            {
                photoDescription = value;
                ValidateDescription();
                OnPropertyChanged("PhotoDescription");
            }
        }

        private bool showDescriptionError;

        public bool ShowDescriptionError
        {
            get => showDescriptionError;
            set
            {
                showDescriptionError = value;
                OnPropertyChanged("ShowDescriptionError");
            }
        }



        private string descriptionError;

        public string DescriptionError
        {
            get => descriptionError;
            set
            {
                descriptionError = value;
                OnPropertyChanged("DescriptionError");
            }
        }

        private void ValidateDescription()
        {
            this.ShowDescriptionError = string.IsNullOrEmpty(PhotoDescription);
            if (this.ShowDescriptionError)
            {
                this.DescriptionError = "זהו שדה חובה";
            }


        }
        #endregion

        #region SelectedImgSrc
        private string selectedimgSrc;

        public string SelectedImgSrc
        {
            get => selectedimgSrc;
            set
            {
                selectedimgSrc = value;
                OnPropertyChanged("SelectedImgSrc");
            }
        }
        private const string DEFAULT_PHOTO_SRC = "user.png";
        #endregion

        #region ServerStatus
        private string serverStatus;
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }
        #endregion

        #region Name
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        #region NotParent
        private bool notParent;
        public bool NotParent
        {
            get { return notParent; }
            set
            {
                notParent = value;
                OnPropertyChanged("NotParent");
            }
        }
        #endregion

        public ObservableCollection<Event> EventList { get; }

        public PhotoGalleryViewModel()
        {
            App a = (App)App.Current;

            if (a.SelectedStudent != null) { Name = $"{a.SelectedStudent.FirstName} {a.SelectedStudent.LastName}"; }
            else if(a.CurrUser != null) { Name = $"{a.CurrUser.Fname} {a.CurrUser.LastName}"; }

            NotParent = false;
            this.showDescriptionError = false;
            EventDate = DateTime.Now;
            PhotoDescription = "";
            EventList = new ObservableCollection<Event>();
            CreateCollection();
        }

        private void CreateCollection()
        {

            EventList.Clear();
            App a = (App)App.Current;
            if(a.SelectedStudent == null) { NotParent = true; } 

            foreach (Event ev in a.SelectedGroup.Events)
            {
                this.EventList.Add(ev);
            }


        }

        public ICommand BackgroundClicked => new Command(OnClick);
        public void OnClick()
        {
            PhotoDescription = "";
            SelectedImgSrc = null;
        }
        public ICommand TapPhotoCommand => new Command(OnTapPhoto);
        public async void OnTapPhoto(object pic)
        {
            if (pic != null)
            {
                App a = (App)App.Current;
                Photo selectedPhoto = (Photo)pic;
                Name = $"{selectedPhoto.User.Fname} {selectedPhoto.User.LastName}";
                PhotoDescription = selectedPhoto.Description;
                SelectedImgSrc = selectedPhoto.PhotoURL;

                await PopupNavigation.Instance.PushAsync(new PhotoPopupView(this));
            }

        }

        public ICommand DeleteEventCommand => new Command(OnDeleteEvent);
        public async void OnDeleteEvent(object ev)
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            App a = (App)App.Current;

            Event e = (Event)ev;
           
            bool success = await proxy.DeleteEvent(e);

            if (success)
            {
                a.SelectedGroup.Events.Remove(e);
                CreateCollection();
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "מחיקת הודעה נכשלה", "בסדר");
            }
        }

        public ICommand DeletePhotoCommand => new Command(OnDeletePhoto);
        public async void OnDeletePhoto(object p)
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            App a = (App)App.Current;
            if(p is Photo)
            {
                Photo ph = (Photo)p;
                bool success = await proxy.DeletePhoto(ph.Id);

                if (success)
                {
                    a.SelectedGroup.Events.Where(e => e.EventId == ph.EventId).FirstOrDefault().Photos.Remove(ph);
                    CreateCollection();
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "מחיקת הודעה נכשלה", "בסדר");
                }
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
                    EventDate = EventDate.ToShortDateString(),
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
                }


            }

            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת אירוע נכשלה", "בסדר");
            }

        }

        public ICommand AddPhotoCommand => new Command(OnAddPhoto);
        public async void OnAddPhoto()
        {
            if (SelectedEvent != null)
            {
                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                await PopupNavigation.Instance.PopAsync();

                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

                if (this.imageFileResult != null)
                {
                    ServerStatus = "מעלה תמונה...";

                    App theApp = (App)App.Current;
                    Photo newPhoto = new Photo()
                    {
                        Event = SelectedEvent,
                        EventId = SelectedEvent.EventId,
                        User = theApp.CurrUser,
                        Name = Name,
                        UserId = theApp.CurrUser.UserId,
                        Description = PhotoDescription
                    };

                    PhotoDescription = "";
                    Photo photo = await proxy.AddPhoto(newPhoto);

                    if (photo == null)
                    {
                        await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת תמונה נכשלה", "בסדר");
                    }
                    else
                    {
                        bool success = await proxy.UploadImage(new FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"Events\\{photo.Id}.jpg");

                        theApp.SelectedGroup.Events.Where(e => e.EventId == photo.EventId).FirstOrDefault().Photos.Add(photo);
                        CreateCollection();

                    }


                }
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }

        #region PhotoButton

        FileResult imageFileResult;
        public event Action<ImageSource> SetImageSourceEvent;
        public ICommand PickImageCommand => new Command(OnPickImage);
        public async void OnPickImage(object obj)
        {
            if (obj != null)
            {
                SelectedEvent = (Event)obj;
            }

            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "בחר תמונה"
            });

            if (result != null)
            {
                await PopupNavigation.Instance.PushAsync(new ShowPhotoPopup(this));

                this.imageFileResult = result;

                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);

            }
        }


        //The following command handle the take photo button
        public ICommand CameraImageCommand => new Command(OnCameraImage);
        public async void OnCameraImage()
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
            {
                Title = "צלם תמונה"
            });

            if (result != null)
            {
                this.imageFileResult = result;
                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }
        #endregion
    }




}

