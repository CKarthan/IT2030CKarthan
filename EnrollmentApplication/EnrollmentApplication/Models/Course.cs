using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models {
    public class Course {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Course title is required")]
        [MaxLength(150, ErrorMessage = "The course title cannot be more than 150 characters.")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "You must enter the number of credits for this course.")]
        [Range(1,4, ErrorMessage = "The number of credits for a course is 1-4.")]
        public decimal Credits { get; set; }
    }
}