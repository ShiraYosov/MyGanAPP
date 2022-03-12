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

        public ObservableCollection<Group> GroupsList { get; }

        public KindergartenGroupsViewModel()
        {
            GroupsList = new ObservableCollection<Group>();
            CreateCollection(); 
        }

        private void CreateCollection()
        {
            
            GroupsList.Clear();
            App a = (App)App.Current;

            ICollection<Group> theGroups = a.SelectedKindergarten.Groups;
            foreach (Group g in theGroups)
            {
                this.GroupsList.Add(g);
            }
           

        }

        public ICommand SelectionChanged => new Command(OnSelection);
        public async void OnSelection(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new GroupTab());
        }
    }
}
