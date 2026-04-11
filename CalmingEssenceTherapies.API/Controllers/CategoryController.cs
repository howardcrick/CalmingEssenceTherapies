using CalmingEssenceTherapies.Services.Categories;
using CalmingEssenceTherapies.Services.Treatments.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CalmingEssenceTherapies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        [HttpGet("GetCategories")]
        public async Task<List<CategoryDto>> GetAllCategories(ICategoryService categoryService)
        {
            return await categoryService.GetAllCategories();
        }
    }
}
