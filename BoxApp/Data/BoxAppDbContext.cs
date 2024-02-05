using BoxApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxApp.Data;

public class BoxAppDbContext(DbContextOptions<BoxAppDbContext> options) : DbContext(options)
{
    public DbSet<Box> Boxes { get; set; }
}