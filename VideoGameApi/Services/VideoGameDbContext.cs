using Microsoft.EntityFrameworkCore;
using VideoGameApi.model;

namespace VideoGameApi.Services;

public class VideoGameDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<VideoGame> Games { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<VideoGame>();
    }
}