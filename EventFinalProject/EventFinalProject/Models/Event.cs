using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventFinalProject.Models {
    public class Event {
        public virtual int EventId { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual int OrganizerId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Oops...title cannot exceed 50 characters, try again.")]
        public virtual string Title { get; set; }
        [MaxLength(500, ErrorMessage = "Uh oh...description cannot exceed 150 characters, try again.")]
        public virtual string Description { get; set; }
        [Required]
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        [Required]
        public virtual string City { get; set; }
        [Required]
        public virtual string State { get; set; }
        [Required]
        public virtual int Zipcode { get; set; }
        [Required]
        public virtual DateTime StartDateTime { get; set; }
        [Required]
        public virtual DateTime EndDateTime { get; set; }
        public virtual Category Category { get; set; }
        public virtual Organizer Organizer { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Oopsie daisy...Maximum number of tickets must be greater than zero.")]
        public virtual int MaxTickets { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Oh no...Number of available tickets must be greater than zero.")]
        public virtual int AvailableTickets { get; set; }
        public virtual decimal Price { get; set; }
    }
}