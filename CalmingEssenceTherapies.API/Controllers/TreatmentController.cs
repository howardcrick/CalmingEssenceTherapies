using CalmingEssenceTherapies.Services.Treatments;
using CalmingEssenceTherapies.Services.Treatments.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CalmingEssenceTherapies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TreatmentController : Controller
    {
        [HttpGet("{treatmentId}")]
        public async Task<TreatmentDto> GetTreatmentDetails(int treatmentId, ITreatmentService treatmentService)
        {
            return await treatmentService.GetTreatmentDetails(treatmentId);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> AddTreatment([FromForm] AddTreatmentDto treatment, ITreatmentService treatmentService)
        {
            await treatmentService.AddTreatment(treatment.Name, treatment.Description, treatment.Price, treatment.CategoryId, treatment.TreatmentImage);
            return Results.Ok();
        }

        [HttpGet("GetTreatments")]
        public async Task<List<ManageTreatmentDto>> GetTreatments(ITreatmentService treatmentService)
        {
            return await treatmentService.GetAllTreatments();
        }
    }
}
