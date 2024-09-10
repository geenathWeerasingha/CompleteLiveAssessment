using LiveAssessment.Models;

namespace LiveAssessment.Dto
{
    public class ProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Sku { get; set; }
        public int VendorId { get; set; }

        public ICollection<VariationDto> Variations { get; set; }

    }
}
