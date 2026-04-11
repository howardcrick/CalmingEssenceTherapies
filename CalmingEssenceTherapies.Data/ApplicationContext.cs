using CalmingEssenceTherapies.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CalmingEssenceTherapies.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
    {
    }

    public DbSet<Treatment> Treatments { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
}
