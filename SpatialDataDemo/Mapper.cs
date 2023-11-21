using NetTopologySuite.Geometries;

namespace SpatialDataDemo
{
    public static class Mapper
    {
        public static Location Map(LocationDto dto)
        {
            return new Location
            {
                Name = dto.Name,
                GeoLocation = new Point(dto.Longitude, dto.Latitude) { SRID = 4326 }
            };
        }

        public static IEnumerable<LocationDto> Map(IEnumerable<Location> locations)
        {
            return locations.Select(Map);
        }

        public static LocationDto Map(Location location)
        {
            return new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Latitude = location.GeoLocation.Y,
                Longitude = location.GeoLocation.X,
            };
        }
    }
}
