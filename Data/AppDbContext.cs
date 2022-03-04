using aninja_tags_service.Models;
using Microsoft.EntityFrameworkCore;

namespace aninja_tags_service.Data;

public class AppDbContext : DbContext
{
    public DbSet<Anime> Animes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

}