using System;
using System.Collections.Generic;
using MyGanAPP.Services;

namespace MyGanAPP.Models
{
    public partial class User
    {
        public User()
        {
            Groups = new List<Group>();
            KindergartenManagers = new List<KindergartenManager>();
            Signatures = new List<Signature>();
            StudentOfUsers = new List<StudentOfUser>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fname { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSystemManager { get; set; }
        public bool IsApproved { get; set; }

        public virtual List<Group> Groups { get; set; }
        public virtual List<KindergartenManager> KindergartenManagers { get; set; }
        public virtual List<Signature> Signatures { get; set; }
        public virtual List<StudentOfUser> StudentOfUsers { get; set; }

        public string PhotoURL
        {
            get
            {
                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                Random r = new Random();
                return $"{proxy.GetBasePhotoUri()}{this.UserId}.jpg?" + r.Next();
            }
        }
    }
}
