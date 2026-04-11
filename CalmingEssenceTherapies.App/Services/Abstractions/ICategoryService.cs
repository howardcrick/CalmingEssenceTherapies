using CalmingEssenceTherapies.App.Models;

namespace CalmingEssenceTherapies.App.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<List<Category>?> GetCategories();
    }
}
