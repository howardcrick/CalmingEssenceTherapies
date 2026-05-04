using CalmingEssenceTherapies.Data;
using CalmingEssenceTherapies.Data.Models;
using CalmingEssenceTherapies.Services.Categories;
using CalmingEssenceTherapies.Services.Treatments.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;

namespace CalmingEssenceTherapies.Services.Treatments;

public class TreatmentService : ITreatmentService
{
    private readonly ApplicationContext _context;
    private readonly string _webRootPath;

    // Allowed MIME types for uploaded treatment images
    private static readonly HashSet<string> AllowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg",
        "image/png",
        "image/webp"
    };

    // Maps MIME type → file extension
    private static readonly Dictionary<string, string> MimeToExtension = new(StringComparer.OrdinalIgnoreCase)
    {
        { "image/jpeg", ".jpg" },
        { "image/png",  ".png" },
        { "image/webp", ".webp" }
    };

    public TreatmentService(ApplicationContext context, string webRootPath)
    {
        _context = context;
        _webRootPath = webRootPath;
    }

    public async Task<TreatmentDto> GetTreatmentDetails(int treatmentId)
    {
        return await _context.Treatments.Where(x => x.Id == treatmentId)
            .Select(x =>
            new TreatmentDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                Duration = x.Duration,
                Category = new CategoryDto
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                }
            }).SingleAsync();
    }

    public async Task<List<ManageTreatmentDto>> GetAllTreatments()
    {
        return await _context.Treatments
            .Select(x => new ManageTreatmentDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            }).ToListAsync();
    }

    public async Task AddTreatment(string name, string? description, decimal price, int categoryId, int duration, IFormFile? treatmentImage)
    {
        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == categoryId);

        if (!categoryExists)
        {
            throw new ArgumentException(
                $"Category with Id {categoryId} does not exist.",
                nameof(categoryId));
        }

        string? imageUrl = null;

        if (treatmentImage != null)
        {
            imageUrl = await SaveTreatmentImageAsync(treatmentImage);
        }

        var newTreatment = new Treatment
        {
            Name = name,
            Description = description,
            Price = price,
            CategoryId = categoryId,
            Duration = duration,
            ImageUrl = imageUrl,
            ImageFileName = treatmentImage?.FileName,
        };

        _context.Treatments.Add(newTreatment);
        await _context.SaveChangesAsync();
    }

    public async Task EditTreatment(int id, string name, string? description, decimal price, int categoryId, int duration)
    {
        var treatment = await _context.Treatments.Where(t => t.Id == id).SingleAsync();

        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == categoryId);

        if (!categoryExists)
        {
            throw new ArgumentException(
                $"Category with Id {categoryId} does not exist.",
                nameof(categoryId));
        }

        treatment.Name = name;
        treatment.Description = description;
        treatment.Price = price;
        treatment.CategoryId = categoryId;
        treatment.Duration = duration;

        await _context.SaveChangesAsync();
    }

    private async Task DeletePreviousImage(int treatmentId)
    {
        var imageUrl = await _context.Treatments
            .Where(t => t.Id == treatmentId)
            .Select(t => t.ImageUrl)
            .SingleOrDefaultAsync();

        if (imageUrl == null) return;

        string filePath = Path.Combine(_webRootPath, imageUrl);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    private async Task<string> SaveTreatmentImageAsync(IFormFile image)
    {
        if (!AllowedContentTypes.Contains(image.ContentType))
        {
            throw new ArgumentException(
                $"Unsupported image type '{image.ContentType}'. " +
                $"Allowed types: {string.Join(", ", AllowedContentTypes)}");
        }

        string uploadsFolder = Path.Combine(
            _webRootPath, "images", "treatments");

        var fileName = Path.GetRandomFileName();

        string extension = MimeToExtension[image.ContentType];

        fileName = Path.ChangeExtension(fileName, extension);

        string filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            await image.CopyToAsync(stream);
        }

        return $"/images/treatments/{fileName}";
    }
}

public class TreatmentDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public required CategoryDto Category { get; set; }
    public required int Duration { get; set; }
}

public class ManageTreatmentDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}

public class AddTreatmentDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public required int CategoryId { get; set; }
    public IFormFile? TreatmentImage { get; set; }
    public required int Duration { get; set; }
}

public class EditTreatmentDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public required int CategoryId { get; set; }
    public required int Duration { get; set; }

}
