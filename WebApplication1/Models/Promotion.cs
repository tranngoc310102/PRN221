using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Promotion
    {
        public int PromotionId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? DiscountPercentage { get; set; }
    }
}
