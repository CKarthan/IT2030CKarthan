using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnrollmentApplication.Models {
    public class Enrollment {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Grade { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public bool IsActive { get; set; }
        public string AssignedCampus { get; set; }
        public string EnrollmentSemester { get; set; }
        public int EnrollmentYear { get; set; }
    }
}