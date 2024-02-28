using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            Shippings = new HashSet<Shipping>();
        }

        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string OrderCode { get; set; } = null!;

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
