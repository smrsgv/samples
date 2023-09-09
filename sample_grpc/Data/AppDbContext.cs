using Microsoft.EntityFrameworkCore;
using sample_grpc.Models;

namespace sample_grpc.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>().HasKey(x => x.Id);

        modelBuilder.Entity<ToDoItem>().Property(x => x.Title)
            .HasMaxLength(100);

        modelBuilder.Entity<ToDoItem>().Property(x => x.Description)
            .HasMaxLength(500);

        modelBuilder.Entity<ToDoItem>().Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(20);

        base.OnModelCreating(modelBuilder);
    }
}