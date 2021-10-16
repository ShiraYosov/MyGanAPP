﻿using System;
using System.Collections.Generic;



namespace MyGanAPP.Models
{
    public partial class StatusType
    {
        public StatusType()
        {
            Approvals = new HashSet<Approval>();
        }

        public int StatusId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Approval> Approvals { get; set; }
    }
}
