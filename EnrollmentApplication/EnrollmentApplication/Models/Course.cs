using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnrollmentApplication.Models {
    public class Course {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Credits { get; set; }
    }
}