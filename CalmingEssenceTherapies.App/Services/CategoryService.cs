using CalmingEssenceTherapies.App.Models;
using CalmingEssenceTherapies.App.Services.Abstractions;
using System.Net.Http.Json;

namespace CalmingEssenceTherapies.App.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;
        public CategoryService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<Category>?> GetCategories()
        {
            return await _client.GetFromJsonAsync<List<Category>>("/Category/GetCategories");
        }
    }
}
