using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Reviews = new HashSet<Review>();
        }

        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int? BrandId { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? ImageUrl { get; set; }
        public string? Ram { get; set; }
        public string? Color { get; set; }
        public decimal? ScreenSize { get; set; }
        public string? CameraResolution { get; set; }
        public int? BatteryCapacity { get; set; }
        public int? CategoryId { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
