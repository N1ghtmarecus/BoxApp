using BoxApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxApp.Data;

public class BoxAppDbContext : DbContext
{
    public DbSet<Box> Boxes => Set<Box>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("StorageAppDb");
    }
}