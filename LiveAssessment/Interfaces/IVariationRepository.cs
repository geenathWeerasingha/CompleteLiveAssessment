using LiveAssessment.Models;

namespace LiveAssessment.Interfaces
{
    public interface IVariationRepository
    {
        Variation GetVariationById(int VariationId);
        bool CreateVariation(Variation Variation);
        bool UpdateVariation(Variation admin);
        bool Save();
    }
}
