using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CalmingEssenceTherapies.Data;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

        var dbPath = Path.Combine(AppContext.BaseDirectory, "database.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new ApplicationContext(optionsBuilder.Options);
    }
}
