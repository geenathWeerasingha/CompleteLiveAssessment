﻿namespace LiveAssessment.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Sku { get; set; }
        public int VendorId { get; set; }
        public ICollection<Variation> Variations { get; set; }

    }
}
