using CalmingEssenceTherapies.Data;
using CalmingEssenceTherapies.Services.Treatments.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CalmingEssenceTherapies.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationContext _context;

        public CategoryService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            return await _context.Categories
                .Select(x =>
                new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
        }
    }

    public class CategoryDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
