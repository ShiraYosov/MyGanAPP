using System;
using System.Collections.Generic;



namespace MyGanAPP.Models
{
    public partial class RelationToStudent
    {
        public RelationToStudent()
        {
            StudentOfUsers = new HashSet<StudentOfUser>();
        }

        public int RelationToStudentId { get; set; }
        public string RelationType { get; set; }

        public virtual ICollection<StudentOfUser> StudentOfUsers { get; set; }
    }
}
