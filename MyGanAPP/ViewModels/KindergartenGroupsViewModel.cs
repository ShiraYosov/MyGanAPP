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
using Rg.Plugins.Popup.Services;
using System.Security.Cryptography;




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

        #region ManagerInfo

        #region ManagerFirstName

        private string managerFirstName;

        public string ManagerFirstName
        {
            get => managerFirstName;
            set
            {
                managerFirstName = value;
                OnPropertyChanged("ManagerFirstName");
            }
        }


        #endregion

        #region ManagerLastName

        private string managerLastName;

        public string ManagerLastName
        {
            get => managerLastName;
            set
            {
                managerLastName = value;
                OnPropertyChanged("ManagerLastName");
            }
        }


        #endregion

        #region Email

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        #endregion

        #region PhoneNumber

        private string phoneNumber;

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        #endregion

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

        private bool ValidateManagerInfo()
        {

            //validate user name and last name
            if (string.IsNullOrEmpty(ManagerLastName) || string.IsNullOrEmpty(managerFirstName) || string.IsNullOrEmpty(Email))
            {
                return false;
            }

            //validate phone number
            if (this.PhoneNumber.Length != 10)
            {
                return false;
            }

            int num;
            bool ok = int.TryParse(PhoneNumber, out num);
            if (!ok)
            {
                return false;
            }

            //validate email
            if (!System.Text.RegularExpressions.Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                return false;
            }

            return true;

        }

        //When adding new group
        public ICommand AddGroupCommand => new Command(OnAddGroup);
        public async void OnAddGroup()
        {
            await PopupNavigation.Instance.PopAsync();
            //Validate all fields
            if (!string.IsNullOrEmpty(GroupName))
            {
                App a = (App)App.Current;
                //Create new group
                Group group = new Group()
                {
                    GroupName = GroupName,
                    KindergartenId = a.SelectedKindergarten.KindergartenId,
                    Kindergarten = a.SelectedKindergarten,
                    Teacher = a.CurrUser,
                    TeacherId = a.CurrUser.UserId
                };

                
                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                Group newGroup = await proxy.AddGroup(group);
                GroupName = "";

                //Checking if registration was ok. If not - display alert
                if (newGroup == null) { await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת קבוצה נכשלה", "בסדר"); }
                else
                {
                    this.GroupsList.Add(newGroup);
                    a.CurrUser.Groups.Add(newGroup);
                    ((App)App.Current).UIRefresh();
                }


            }

            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "לא ניתן להוסיף קבוצה ללא שם!", "בסדר");
            }

        }

        //When adding manager to a kindergarten
        public ICommand AddManagerToKindergartenCommand => new Command(OnAddManager);
        public async void OnAddManager()
        {
            //Validate all fields
            if (ValidateManagerInfo())
            {
                App a = (App)App.Current;

                //Create new manager
                User m = new User()
                {
                    Fname = ManagerFirstName,
                    LastName = ManagerLastName,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    Password= ""
                };
                //Create new kindergartenManager with the current kindergarten
                KindergartenManager KM = new KindergartenManager
                {
                    User = m,
                    KindergartenId = a.SelectedKindergarten.KindergartenId,
                    Kindergarten = a.SelectedKindergarten
                };

                //Add the Kindergarten Manager object to the manager usere
                m.KindergartenManagers.Add(KM);

                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                User newU = await proxy.Register(m);

                //Check if the register is ok
                if (newU == null)
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "הרשמה נכשלה", "בסדר");
                    await PopupNavigation.Instance.PopAsync();
                } 

                else if(newU != null)
                {
                    await App.Current.MainPage.DisplayAlert("הרשמה בוצעה בהצלחה", "", "בסדר");
                    await PopupNavigation.Instance.PopAsync();

                    ManagerFirstName = "";
                    ManagerLastName = "";
                    Email = "";
                    PhoneNumber = "";
 
                }
                
                
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "לא כל פרטי המשתמש תקינים!", "בסדר");
            }

        }

        //On selection of a kindergarten group - sends user to group tab
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
