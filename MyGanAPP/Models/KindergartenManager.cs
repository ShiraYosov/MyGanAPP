using System;
using System.Collections.Generic;


namespace MyGanAPP.Models
{
    public partial class KindergartenManager
    {
        public int UserId { get; set; }
        public int KindergartenId { get; set; }

        public virtual Kindergarten Kindergarten { get; set; }
        public virtual User User { get; set; }
    }
}
