using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class AppDbContext: DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}