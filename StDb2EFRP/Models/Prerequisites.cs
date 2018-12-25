using System;
using System.Collections.Generic;

namespace StDb2EFRP.Models
{
    public partial class Prerequisites
    {
        public string CourseNum { get; set; }
        public string PrereqCnum { get; set; }
        public int Cnum { get; set; }

        public Courses CourseNumNavigation { get; set; }
        public Courses PrereqCnumNavigation { get; set; }
    }
}
