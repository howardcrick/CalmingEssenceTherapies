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

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> AddTreatment([FromForm] AddTreatmentDto treatment, ITreatmentService treatmentService)
        {
            await treatmentService.AddTreatment(treatment.Name, treatment.Description, treatment.Price, treatment.CategoryId, treatment.Duration, treatment.TreatmentImage);
            return Results.Ok();
        }

        [HttpPost("Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> EditTreatment([FromBody] EditTreatmentDto treatment, ITreatmentService treatmentService)
        {
            await treatmentService.EditTreatment(treatment.Id, treatment.Name, treatment.Description, treatment.Price, treatment.CategoryId, treatment.Duration);
            return Results.Ok();
        }

        [HttpGet("GetTreatments")]
        public async Task<List<ManageTreatmentDto>> GetTreatments(ITreatmentService treatmentService)
        {
            return await treatmentService.GetAllTreatments();
        }
    }
}
