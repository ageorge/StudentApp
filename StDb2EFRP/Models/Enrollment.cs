using System;
using System.Collections.Generic;

namespace StDb2EFRP.Models
{
    public partial class Enrollment
    {
        public string CourseNum { get; set; }
        public int StudentId { get; set; }
        public int? SectionNum { get; set; }
        public int Cnum { get; set; }

        public Courses CourseNumNavigation { get; set; }
        public Students Student { get; set; }
    }
}
