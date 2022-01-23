using System;
using System.Collections.Generic;


namespace MyGanAPP.Models
{
    public partial class StudentAllergy
    {
        public string StudentId { get; set; }
        public int AllergyId { get; set; }

        public virtual Allergy Allergy { get; set; }
        public virtual Student Student { get; set; }
    }
}
