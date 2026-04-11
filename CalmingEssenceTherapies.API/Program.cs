using CalmingEssenceTherapies.Data;
using CalmingEssenceTherapies.Services.Categories;
using CalmingEssenceTherapies.Services.Treatments;
using CalmingEssenceTherapies.Services.Treatments.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace CalmingEssenceTherapies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var dbPath = Path.Combine(AppContext.BaseDirectory, "database.db");

            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddScoped<ITreatmentService>(sp =>
            {
                var context = sp.GetRequiredService<ApplicationContext>();
                var env = sp.GetRequiredService<IWebHostEnvironment>();

                return new TreatmentService(context, env.WebRootPath);
            });

            builder.Services.AddScoped<ICategoryService, CategoryService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.InitialiseDatabase();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
