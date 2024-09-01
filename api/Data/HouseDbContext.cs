using Microsoft.EntityFrameworkCore;

public class HouseDbContext : DbContext
{
    public DbSet<HouseEntity> Houses => Set<HouseEntity>();

    public DbSet<BidEntity> Bids => Set<BidEntity>();

    public HouseDbContext(DbContextOptions<HouseDbContext> options): 
        base(options)   {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);

        optionsBuilder.UseSqlite($"Data Source={Path.Join(path, "houses2.db")}");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeetData.Seed(modelBuilder);
    }
}