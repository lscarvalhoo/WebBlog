using Microsoft.EntityFrameworkCore;
using System.Xml;
using WebBlog.Post.Models;

public class DatabaseConfig : DbContext
{
    public DatabaseConfig(DbContextOptions<DatabaseConfig> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserPost> UserPost { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEWID()");

        modelBuilder.Entity<UserPost>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEWID()");
    }
}
