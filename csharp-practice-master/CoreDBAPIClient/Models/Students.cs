using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CoreDBAPIClient.Models
{
    public partial class Students
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }

        public int CourseId { get; set; }

        public DateTime Doj { get; set; }

        
        public virtual Courses Course { get; set; }
    }
}
