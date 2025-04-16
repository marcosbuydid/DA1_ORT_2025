using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /*
     * 1. Table Per Hierarchy (TPH)
     * In TPH, all entities are stored in a single table with a discriminator column.
     */

    /*public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .ToTable("Persons")
            .HasDiscriminator<string>("PersonType")
            .HasValue<Student>("Student")
            .HasValue<Professor>("Professor");

        //seed data
        modelBuilder.Entity<Student>().HasData(new Student(1, "Student Name", "Computer Engineer"));
        modelBuilder.Entity<Professor>().HasData(new Professor(2, "Professor Name", 1500));
    }*/

    /*
     * 2. Table Per Type (TPT)
     * In TPT, each type in the hierarchy is stored in its own table.
     */

    /*public DbSet<Person> Persons { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Professor> Professors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().ToTable("Persons");
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Professor>().ToTable("Professors");

        //seed data
        modelBuilder.Entity<Student>().HasData(new Student(1, "Student Name", "Computer Engineer"));
        modelBuilder.Entity<Professor>().HasData(new Professor(2, "Professor Name", 1500));
    }*/

    /*
     * 3. Table Per Concrete Type (TPC)
     * In TPC, each concrete type has its own table, and there’s no table for the base class.
     */

    public DbSet<Student> Students { get; set; }
    public DbSet<Professor> Professors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Professor>().ToTable("Professors");

        //seed data
        modelBuilder.Entity<Student>().HasData(new Student(1, "Student Name", "Computer Engineer"));
        modelBuilder.Entity<Professor>().HasData(new Professor(2, "Professor Name", 1500));
    }
}