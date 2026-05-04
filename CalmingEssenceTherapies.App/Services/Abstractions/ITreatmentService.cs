using CalmingEssenceTherapies.App.Models;

namespace CalmingEssenceTherapies.App.Services.Abstractions
{
    public interface ITreatmentService
    {
        Task<Treatment?> GetTreatmentDetails(int treatmentId);
        Task<List<ManageTreatment>?> GetAllTreatments();
        Task AddTreatment(string name, string? description, decimal price, int categoryId, int duration, FileResult? treatmentImage);
        Task EditTreatment(int id, string name, string? description, decimal price, int categoryId, int duration);
    }
}
