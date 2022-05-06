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

        #region SelectedImg
        private Photo selectedUserimg;

        public Photo SelectedUserImg
        {
            get => selectedUserimg;
            set
            {
                selectedUserimg = value;
                OnPropertyChanged("SelectedUserImg");
            }
        }
        #endregion
        public ObservableCollection<Event> EventList { get; }

        public ObservableCollection<Photo> Photos { get; set; }

        public PhotoGalleryViewModel()
        {
            App a = (App)App.Current;

            if (a.SelectedStudent != null) { Name = $"{a.SelectedStudent.FirstName} {a.SelectedStudent.LastName}"; }
            else if (a.CurrUser != null) { Name = $"{a.CurrUser.Fname} {a.CurrUser.LastName}"; }

            SelectedUserImg = null;
            NotParent = false;
            this.showDescriptionError = false;
            EventDate = DateTime.Now;
            PhotoDescription = "";
            Photos = new ObservableCollection<Photo>();
            EventList = new ObservableCollection<Event>();
            CreateEventCollection();
            CreatePhotoCollection();
        }

        private void CreateEventCollection()
        {

            EventList.Clear();
            App a = (App)App.Current;
            if (a.SelectedStudent == null)
            {
                NotParent = true;

                foreach (Event ev in a.SelectedGroup.Events)
                {
                    this.EventList.Add(ev);
                }
            }

            else
            {
                NotParent = false;

                foreach (Event ev in a.SelectedStudent.Group.Events)
                {
                    this.EventList.Add(ev);
                }
            }



        }
        private void CreatePhotoCollection()
        {
            Photos.Clear();
            App theApp = (App)App.Current;

            if (theApp.CurrUser != null && theApp.SelectedStudent != null)
            {
                Models.Group studentGroup = theApp.SelectedStudent.Group;
                foreach (Photo p in theApp.CurrUser.Photos)
                {
                    if (p.Event.Group.GroupId == studentGroup.GroupId)
                        Photos.Add(p);
                }
            }

            else if (theApp.CurrUser != null)
            {
                foreach (Photo p in theApp.CurrUser.Photos)
                {
                    if (IsEventExist(p.Event) && p.Event.GroupId == theApp.SelectedGroup.GroupId)
                    {
                        Photos.Add(p);
                    }

                }
            }
        }

        private bool IsEventExist(Event e)
        {
            foreach (Event ev in EventList)
            {
                if (ev.EventId == e.EventId)
                    return true;
            }
            return false;
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
                Name = selectedPhoto.Name;
                PhotoDescription = selectedPhoto.Description;
                SelectedImgSrc = selectedPhoto.PhotoURL;

                await PopupNavigation.Instance.PushAsync(new PhotoPopupView(this));
            }

        }

        public ICommand TapUserPhotoCommand => new Command(OnTapUserPhoto);
        public async void OnTapUserPhoto(object pic)
        {
            if (pic != null)
            {
                this.SelectedUserImg = (Photo)pic;
                await PopupNavigation.Instance.PushAsync(new UserPhotoPopup(this));
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
                CreateEventCollection();
                CreatePhotoCollection();
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "מחיקת הודעה נכשלה", "בסדר");
            }
        }

        public ICommand DeletePhotoCommand => new Command(OnDeletePhoto);
        public async void OnDeletePhoto()
        {
            await PopupNavigation.Instance.PopAsync();
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            App a = (App)App.Current;
            if (SelectedUserImg != null)
            {
                Photo ph = (Photo)SelectedUserImg;
                bool success = await proxy.DeletePhoto(ph.Id);

                if (success)
                {
                    a.SelectedGroup.Events.Where(e => e.EventId == ph.EventId).FirstOrDefault().Photos.Remove(ph);
                    Photo p = a.CurrUser.Photos.Where(po => po.Id == ph.Id).FirstOrDefault();
                    a.CurrUser.Photos.Remove(p);
                    CreatePhotoCollection();
                    CreateEventCollection();
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "מחיקת תמונה נכשלה", "בסדר");
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

        public ICommand EditPhotoCommand => new Command(OnEditPhoto);
        public async void OnEditPhoto()
        {
            this.selectedimgSrc = selectedUserimg.PhotoURL;
            this.photoDescription = selectedUserimg.Description;
            await PopupNavigation.Instance.PushAsync(new ShowPhotoPopup(this));
        }

        public ICommand ChangeDescriptionCommand => new Command(OnChange);
        public async void OnChange()
        {
            App a = (App)App.Current;
            Photo toChange = a.CurrUser.Photos.Where(p => p.Id == selectedUserimg.Id).FirstOrDefault();
            toChange.Description = PhotoDescription;

            await PopupNavigation.Instance.PushAsync(new ShowPhotoPopup(this));
            PhotoDescription = "";

            //add change photo description function
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
                        PhotoDescription = "";
                        await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת תמונה נכשלה", "בסדר");
                    }
                    else
                    {
                        bool success = await proxy.UploadImage(new FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"Events\\{photo.Id}.jpg");

                        theApp.CurrUser.Photos.Add(photo);
                        if (notParent)
                        {
                            theApp.SelectedGroup.Events.Where(e => e.EventId == photo.EventId).FirstOrDefault().Photos.Add(photo);
                        }
                        else
                        {
                            theApp.SelectedStudent.Group.Events.Where(e => e.EventId == photo.EventId).FirstOrDefault().Photos.Add(photo);
                        }

                        CreatePhotoCollection();
                        CreateEventCollection();

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

