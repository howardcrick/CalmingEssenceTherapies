using CalmingEssenceTherapies.App.Models;

namespace CalmingEssenceTherapies.App.Services.Abstractions
{
    public interface ITreatmentService
    {
        Task<Treatment?> GetTreatmentDetails(int treatmentId);
        Task AddTreatment(string name, string? description, decimal price, int categoryId, FileResult? treatmentImage);
    }
}
