using LiveAssessment.Models;

namespace LiveAssessment.Interfaces
{
    public interface IProductRepository
    {
        Product GetProductById(int productId);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product admin);
        bool Save();

    }
}
