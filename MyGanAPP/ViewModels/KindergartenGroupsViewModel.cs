using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MyGanAPP.Services;
using System.Text.Json;
using MyGanAPP.Models;
using MyGanAPP.Views;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace MyGanAPP.ViewModels
{
    internal class KindergartenGroupsViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private object selectedItem;
        public object SelectedItem
        {
            get => selectedItem;
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }

            }
        }

        #region GroupName
        private string groupName;
        public string GroupName
        {
            get => groupName;
            set
            {
                if (this.groupName != value)
                {
                    this.groupName = value;
                    OnPropertyChanged("GroupName");
                }

            }
        }
        #endregion


        public ObservableCollection<Group> GroupsList { get; }

        public KindergartenGroupsViewModel()
        {
            GroupsList = new ObservableCollection<Group>();
            GroupName = "";
            CreateCollection();
        }

        private void CreateCollection()
        {

            GroupsList.Clear();
            App a = (App)App.Current;

            KindergartenManager km = a.CurrUser.KindergartenManagers.Where(k => k.KindergartenId == a.SelectedKindergarten.KindergartenId).FirstOrDefault();
            ICollection<Group> theGroups = km.Kindergarten.Groups;
            foreach (Group g in theGroups)
            {
                this.GroupsList.Add(g);
            }


        }

        public ICommand AddGroupCommand => new Command(OnAddGroup);
        public async void OnAddGroup()
        {
            if (!string.IsNullOrEmpty(GroupName))
            {
                App a = (App)App.Current;
                Group group = new Group()
                {
                    GroupName = GroupName,
                    KindergartenId = a.SelectedKindergarten.KindergartenId,
                    Kindergarten = a.SelectedKindergarten,
                    Teacher = a.CurrUser,
                    TeacherId = a.CurrUser.UserId
                };

                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                bool success = await proxy.AddGroup(group);


                if (!success) { await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת קבוצה נכשלה", "בסדר"); }
                else
                    CreateCollection();

            }

            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת קבוצה נכשלה", "בסדר");
            }

        }

        public ICommand SelectionChanged => new Command(OnSelection);
        public async void OnSelection()
        {
            if (SelectedItem != null && SelectedItem is Group)
            {
                App a = (App)App.Current;
                a.SelectedGroup = (Group)SelectedItem;
                await App.Current.MainPage.Navigation.PushAsync(new GroupTab());
                SelectedItem = null;
            }
        }
    }
}
