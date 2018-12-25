using System;
using System.Collections.Generic;

namespace StDb2EFRP.Models
{
    public partial class Courses
    {
        public Courses()
        {
            CoursesTaken = new HashSet<CoursesTaken>();
            Enrollment = new HashSet<Enrollment>();
            PrerequisitesCourseNumNavigation = new HashSet<Prerequisites>();
            PrerequisitesPrereqCnumNavigation = new HashSet<Prerequisites>();
        }

        public string CourseNum { get; set; }
        public string CourseName { get; set; }
        public int? CreditHours { get; set; }

        public CoursesOffered CoursesOffered { get; set; }
        public ICollection<CoursesTaken> CoursesTaken { get; set; }
        public ICollection<Enrollment> Enrollment { get; set; }
        public ICollection<Prerequisites> PrerequisitesCourseNumNavigation { get; set; }
        public ICollection<Prerequisites> PrerequisitesPrereqCnumNavigation { get; set; }
    }
}
