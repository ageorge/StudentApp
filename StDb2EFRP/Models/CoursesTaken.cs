using System;
using System.Collections.Generic;

namespace StDb2EFRP.Models
{
    public partial class CoursesTaken
    {
        public int StudentId { get; set; }
        public string CourseNum { get; set; }
        public double? Grade { get; set; }
        public int Snum { get; set; }

        public Courses CourseNumNavigation { get; set; }
        public Students Student { get; set; }
    }
}
