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

        public async Task AddTreatment(string name, string? description, decimal price, int categoryId, FileResult? treatmentImage)
        {
            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(name), "Name");
            requestContent.Add(new StringContent(description ?? string.Empty), "Description");
            requestContent.Add(new StringContent(price.ToString(CultureInfo.InvariantCulture)), "Price");
            requestContent.Add(new StringContent(categoryId.ToString()), "CategoryId");

            if (treatmentImage != null)
            {
                var stream = await treatmentImage.OpenReadAsync();
                var streamContent = new StreamContent(stream);
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(treatmentImage.ContentType);
                requestContent.Add(streamContent, "TreatmentImage", treatmentImage.FileName);
            }

            await _client.PostAsync("/Treatment", requestContent);
        }

    }
}
