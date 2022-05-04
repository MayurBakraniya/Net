using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CoreDBAPI.Models
{
    public partial class Courses
    {
        public Courses()
        {
            //Students = new HashSet<Students>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Duration { get; set; }

        //public virtual ICollection<Students> Students { get; set; }
    }
}
