using System;
using System.Collections.Generic;
using System.Text;

namespace MyGanAPP.Models
{
    public class RegisterUserDto
    {
        public User User { get; set; }
        public Student Student { get; set; }
        public StudentOfUser StudentOfUser { get; set; }
    }
}
