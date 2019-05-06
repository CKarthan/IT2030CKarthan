using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventFinalProject.Models {
    public class Organizer {
        public virtual int OrganizerId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string PhoneNumber { get; set; }
    }
}