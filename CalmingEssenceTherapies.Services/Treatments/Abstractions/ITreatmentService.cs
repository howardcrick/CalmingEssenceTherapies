using Microsoft.AspNetCore.Http;

namespace CalmingEssenceTherapies.Services.Treatments.Abstractions
{
    public interface ITreatmentService
    {
        Task<TreatmentDto> GetTreatmentDetails(int treatmentId);
        Task AddTreatment(string name, string? description, decimal price, int categoryId, IFormFile? treatmentImage);
    }
}
