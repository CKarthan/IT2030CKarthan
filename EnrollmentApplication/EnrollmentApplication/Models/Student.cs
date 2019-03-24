using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models {
    public class Student : IValidatableObject {

        public int StudentId { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(50, ErrorMessage = "Last Name must be 50 characters or less")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50, ErrorMessage = "First Name must be 50 characters or less")]
        public string FirstName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }
       
        public string State { get; set; }
      
        public string Zipcode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {

            //Validation 1 Check to see if Address2 is the same as Address1, if so show error
            if (Address1 != null && Address2 != null) {
                if(Address2 == Address1) {
                    yield return new ValidationResult("Address 2 cannot be the same as Address 1.", new [] {"Address2"});
                }
            }

            //Validation 2 Check to see the State field is only 2 letters
            if(State.Length != 2) {
                yield return new ValidationResult("Enter a 2 digit State code.", new [] {"State"});
            }

            //Validation 3 Check to see the Zipcode field is only 5 digits
            if(Zipcode.Length != 5) {
              yield return new ValidationResult("Engter a 5 digit Zipcode", new [] {"Zipcode"});
            }
        }
    }
}