using CalmingEssenceTherapies.App.Models;
using CalmingEssenceTherapies.App.Services.Abstractions;
using System.Globalization;
using System.Net.Http.Json;

namespace CalmingEssenceTherapies.App.Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly HttpClient _client;
        public TreatmentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Treatment?> GetTreatmentDetails(int treatmentId)
        {
            return await _client.GetFromJsonAsync<Treatment>($"/Treatment/{treatmentId}");
        }

        public async Task<List<ManageTreatment>?> GetAllTreatments()
        {
            return await _client.GetFromJsonAsync<List<ManageTreatment>>("/Treatment/GetTreatments");
        }

        public async Task AddTreatment(string name, string? description, decimal price, int categoryId, int duration, FileResult? treatmentImage)
        {
            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(name), "Name");
            requestContent.Add(new StringContent(description ?? string.Empty), "Description");
            requestContent.Add(new StringContent(duration.ToString()), "Duration");
            requestContent.Add(new StringContent(price.ToString(CultureInfo.InvariantCulture)), "Price");
            requestContent.Add(new StringContent(categoryId.ToString()), "CategoryId");

            if (treatmentImage != null)
            {
                var stream = await treatmentImage.OpenReadAsync();
                var streamContent = new StreamContent(stream);
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(treatmentImage.ContentType);
                requestContent.Add(streamContent, "TreatmentImage", treatmentImage.FileName);
            }

            await _client.PostAsync("/Treatment/Add", requestContent);
        }

        public async Task EditTreatment(int id, string name, string? description, decimal price, int categoryId, int duration)
        {
            var treatmentDetails = new EditTreatment
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price,
                CategoryId = categoryId,
                Duration = duration
            };

            await _client.PostAsJsonAsync<EditTreatment>("/Treatment/Edit", treatmentDetails);
        }
    }

}
