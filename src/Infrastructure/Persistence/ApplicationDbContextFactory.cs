using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=RestaurantDb;Username=postgres;Password=postgres", 
            b => b.MigrationsAssembly("Infrastructure")
        );

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}