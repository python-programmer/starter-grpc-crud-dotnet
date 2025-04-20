using Microsoft.EntityFrameworkCore;
using ProtobufCRUDServer.Models;

namespace ProtobufCRUDServer.Data;

public class AppDbContext: DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PersonEntity>(entity =>
        {
            entity.Property(p => p.NationalCode)
                .HasMaxLength(10);
        });
        modelBuilder.Entity<PersonEntity>()
            .HasIndex(u => u.NationalCode)
            .IsUnique();
    }

    public DbSet<PersonEntity> People => Set<PersonEntity>();
}