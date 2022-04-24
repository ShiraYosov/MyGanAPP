using System;
using System.Collections.Generic;
using MyGanAPP.Services;

namespace MyGanAPP.Models
{ 
    public partial class Photo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public int EventId { get; set; }

        public string Name { get; set; }
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }

        public string PhotoURL
        {
            get
            {
                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                Random r = new Random();
                return $"{proxy.GetBasePhotoUri()}Events/{this.Id}.jpg?" + r.Next();
            }
        }
    }
}
