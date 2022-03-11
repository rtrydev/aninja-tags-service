using aninja_tags_service.Configuration;
using aninja_tags_service.Models;
using Microsoft.EntityFrameworkCore;

namespace aninja_tags_service.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Anime> Animes { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Anime>(new AnimeConfiguration());
        modelBuilder.ApplyConfiguration<Tag>(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new AnimeTagConfiguration());
    }
}