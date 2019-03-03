using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models {
    public class Enrollment {
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        [Required(ErrorMessage = "You must enter the students grade for this course.")]
        [RegularExpression(@"[A-Fa-f]", ErrorMessage = "Please enter a valid grade.")]
        public string Grade { get; set; }

        public Student Student { get; set; }

        public Course Course { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Please enter the assigned campus.")]
        public string AssignedCampus { get; set; }

        [Required(ErrorMessage = "Please select a semester.")]
        public string EnrollmentSemester { get; set; }

        [Required(ErrorMessage = "Please select an enrollment year.")]
        [Range(2018, 2099, ErrorMessage = "The enrollment year must be 2018 or later.")]
        public int EnrollmentYear { get; set; }

        [InvalidCharsAttribute("@", ErrorMessage ="@ is not a valid character.")]
        public virtual string Notes { get; set; }

    }
}