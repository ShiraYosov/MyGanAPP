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
        public ObservableCollection<Message> GroupMessages { get; }

        public MessagesViewModel()
        {
            GroupMessages = new ObservableCollection<Message>();
            CreateCollection();
        }

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
            isRefreshing = false;

        }

        public ICommand DeleteMessageCommand => new Command(OnDelete);
        public async void OnDelete()
        {
            MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();

            //bool ok = await proxy.ChangeUserStatus(u);
            //if (ok) { OnRefresh(); }
            //else
            //{
            //    await App.Current.MainPage.DisplayAlert("שגיאה", "פעולה נכשלה!", "בסדר");
            //}

        }
    }
}
