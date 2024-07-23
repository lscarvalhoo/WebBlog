using Microsoft.EntityFrameworkCore;
using System.Xml;

public class DatabaseConfig : DbContext
{
    public DatabaseConfig(DbContextOptions<DatabaseConfig> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEWID()");
    }
}
