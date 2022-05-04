using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CoreDBAPIClient.Models
{
    public partial class Courses
    {
        public Courses()
        {
            Students = new HashSet<Students>();
        }

        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Duration { get; set; }

        
        public virtual ICollection<Students> Students { get; set; }
    }
}
