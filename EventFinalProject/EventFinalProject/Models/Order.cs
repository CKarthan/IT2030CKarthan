using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventFinalProject.Models {
    public class Order {
        public virtual int OrderId { get; set; }
        public virtual int UserId { get; set; }
        public virtual decimal OrderTotal { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}