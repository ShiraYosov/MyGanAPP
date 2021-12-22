using System;
using System.Collections.Generic;
using MyGanAPP.Services;


namespace MyGanAPP.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentAllergies = new HashSet<StudentAllergy>();
            StudentOfUsers = new HashSet<StudentOfUser>();
        }

        public int StudentId { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public int GradeId { get; set; }
        public int GroupId { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<StudentAllergy> StudentAllergies { get; set; }
        public virtual ICollection<StudentOfUser> StudentOfUsers { get; set; }

        public string PhotoURL
        {
            get
            {
                MyGanAPIProxy proxy = MyGanAPIProxy.CreateProxy();
                string url = $"{proxy.BaseKidsPhotosUri}{this.StudentId}.jpg";
                return url;
            }
        }
    }
}
