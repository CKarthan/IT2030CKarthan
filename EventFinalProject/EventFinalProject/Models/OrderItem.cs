using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventFinalProject.Models {
    public class OrderItem {
        public virtual int OrderItemId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int EventId { get; set; }
        public virtual int Quantity { get; set; }
    }
}