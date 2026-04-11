using CalmingEssenceTherapies.Services.Categories;

namespace CalmingEssenceTherapies.Services.Treatments.Abstractions
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategories();
    }
}
