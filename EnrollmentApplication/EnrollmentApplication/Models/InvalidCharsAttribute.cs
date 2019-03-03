using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models {
    public class InvalidCharsAttribute : ValidationAttribute{

        readonly string enteredValue;

        public InvalidCharsAttribute(string enteredValue) :base("{0} contains an unacceptable character.") {
            this.enteredValue = enteredValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if(value != null) {

                if((string)value == "@") {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);

                    return new ValidationResult(errorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}