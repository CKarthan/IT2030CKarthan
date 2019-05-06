using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventFinalProject.Models {
    public class Category {
        public virtual int CategoryId { get; set; }

        [MaxLength(50, ErrorMessage = "Darn...event type cannot exceed 50 characters, try again.")]
        public virtual string Name { get; set; }
    }
}