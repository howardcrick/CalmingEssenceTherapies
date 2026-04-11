using CalmingEssenceTherapies.Data;
using CalmingEssenceTherapies.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CalmingEssenceTherapies.API;

public static class EnvironmentSeeder
{
    public static IApplicationBuilder InitialiseDatabase(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationContext>();
            Initialize(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return app;
    }

    private static void Initialize(ApplicationContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        dbContext.Database.Migrate();

        if (dbContext.Treatments.Any()) return;

        var categories = new Category[]
        {
            new Category {Id = 1, Name = "Massage Therapy" },
            new Category {Id = 2, Name = "Facials" },
            new Category {Id = 3, Name = "Waxing" }
        };

        var treatments = new Treatment[]
        {
            new Treatment
            {
                Name = "Swedish Back Massage",
                Description = "A therapeutic massage that uses a combination of long gliding strokes, kneading, and circular movements to promote relaxation, relieve muscle tension, and improve circulation.",
                Price = 35m,
                CategoryId = 1,
            },
            new Treatment
            {
                Name = "Hot Stone Back Massage",
                Description = "Using smooth, heated stones to allow for a deeper and more effective massage, while also promoting deep relaxation and helping to relieve stress and pain.",
                Price = 35m,
                CategoryId = 1,
            },
            new Treatment
            {
                Name = "Express Facial",
                Description = "Achieve glowing skin results with medic grade customised skincare. Depending on your skin needs this express facial will include dermaplaning, oxygen on the face, mini microdermabrasion, extractions and a customised sheet or face mask.",
                Price = 35m,
                CategoryId = 2,
            },
            new Treatment
            {
                Name = "Eyebrow Wax",
                Description = "Massage using essential oils to promote relaxation and emotional wellbeing.",
                Price = 11.5m,
                CategoryId = 3
            }
        };

        foreach (var category in categories)
            dbContext.Categories.Add(category);

        dbContext.SaveChanges();

        foreach (var treatment in treatments)
            dbContext.Treatments.Add(treatment);

        dbContext.SaveChanges();
    }
}
