using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class InMemoryAppContextFactory
{
    public AppDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase("TestingDB");

        return new AppDbContext(optionsBuilder.Options);
    }
}