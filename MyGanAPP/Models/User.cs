﻿using System;
using System.Collections.Generic;
using MyGanAPP.Services;

namespace MyGanAPP.Models
{
    public partial class User
    {
        public User()
        {
            Groups = new HashSet<Group>();
            KindergartenManagers = new HashSet<KindergartenManager>();
            PendingTeachers = new HashSet<PendingTeacher>();
            Photos = new HashSet<Photo>();
            Signatures = new HashSet<Signature>();
            StudentOfUsers = new HashSet<StudentOfUser>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fname { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSystemManager { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<KindergartenManager> KindergartenManagers { get; set; }
        public virtual ICollection<PendingTeacher> PendingTeachers { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Signature> Signatures { get; set; }
        public virtual ICollection<StudentOfUser> StudentOfUsers { get; set; }

        public string PhotoURL
        {
            get
            {
                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                Random r = new Random();
                return $"{proxy.GetBasePhotoUri()}users/{this.UserId}.jpg?" + r.Next();
            }
        }
    }
}
