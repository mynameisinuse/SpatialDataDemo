

using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace SpatialDataDemo
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;

        [Column (TypeName = "geography")]
        public Point GeoLocation { get; set; } = default!;
    }
}
