using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Shipping
    {
        public int ShippingId { get; set; }
        public string? OrderCode { get; set; }
        public string? ShippingAddress { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string? ShippingStatus { get; set; }

        public virtual OrderDetail? OrderCodeNavigation { get; set; }
    }
}
