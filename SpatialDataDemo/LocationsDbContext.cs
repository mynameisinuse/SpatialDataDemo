using Microsoft.EntityFrameworkCore;

namespace SpatialDataDemo
{
    public class LocationsDbContext: DbContext
    {
        public LocationsDbContext(DbContextOptions options) : base(options){}
        public DbSet<Location> Locations { get; set; } = default!;
    }
}
