using System;
using System.Collections.Generic;

#nullable disable

namespace DemoCrudCS.Models
{
    public partial class Student
    {
        public int Sid { get; set; }
        public int? Id { get; set; }
        public string Sname { get; set; }

        public virtual Course IdNavigation { get; set; }
    }
}
