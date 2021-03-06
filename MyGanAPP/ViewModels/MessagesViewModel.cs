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
    internal class MessagesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Refresh
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        public ICommand RefreshCommand => new Command(OnRefresh);
        public void OnRefresh()
        {
            CreateCollection();
        }
        #endregion

        #region Message
        private string message;
        public string Message
        {
            get { return this.message; }

            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }
        #endregion
        public ObservableCollection<Message> GroupMessages { get; }

        public MessagesViewModel()
        {
            GroupMessages = new ObservableCollection<Message>();
            ((App)App.Current).RefreshUI += CreateCollection;
            Message = "";
            CreateCollection();
        }

        //Create message collection
        private void CreateCollection()
        {

            App a = (App)App.Current;
            IsRefreshing = true;

            if (a.SelectedGroup != null)
            {

                GroupMessages.Clear();

                foreach (Message message in a.SelectedGroup.Messages)
                {
                    GroupMessages.Add(message);

                }
            }

            if (a.SelectedStudent != null)
            {

                GroupMessages.Clear();

                foreach (Message message in a.SelectedStudent.Group.Messages)
                {
                    GroupMessages.Add(message);

                }
            }

            isRefreshing = false;

        }

        //Send message
        public ICommand SendMessageCommand => new Command(OnSendMessage);
        public async void OnSendMessage()
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            //Validate message
            if (!string.IsNullOrEmpty(Message))
            {
               App a = (App)App.Current;
                //Create new message
                Message m = new Message()
                {
                    Content = Message,
                    GroupId = a.SelectedGroup.GroupId,
                    UserId = a.CurrUser.UserId,
                    Group = a.SelectedGroup,
                    User = a.CurrUser,
                };

                //Send message ton proxy
                Message newMsg = await proxy.SendMessage(m);
                Message = ""; 

                //Check if the message is ok
                if (newMsg != null)
                {
                    a.SelectedGroup.Messages.Add(newMsg);
                    //recreate the message collection
                    CreateCollection();
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "שליחת הודעה נכשלה", "בסדר");
                }

            }

        }

        public ICommand DeleteMessageCommand => new Command<Message>(OnDelete);
        public async void OnDelete(Message m)
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
            App a = (App)App.Current;

           
            if (m!= null && m.UserId == a.CurrUser.UserId)
            {
                bool success = await proxy.DeleteMessage(m);
               
                if (success)
                {
                    a.SelectedGroup.Messages.Remove(m);
                    CreateCollection();
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "מחיקת הודעה נכשלה", "בסדר");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "מחיקת הודעה לא אפשרית!", "בסדר");
            }
        }


    }
}
