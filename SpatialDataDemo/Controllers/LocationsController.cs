using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

namespace SpatialDataDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly LocationsDbContext _context;
        private readonly IGeoCoordinatesService _geoCoordinatesService;

        public LocationsController(LocationsDbContext context, IGeoCoordinatesService geoCoordinatesService)
        {
            _context = context;
            _geoCoordinatesService = geoCoordinatesService;
        }


        [HttpGet]
        public IActionResult GetLocations()
        {
            var locations = _context.Set<Location>().ToList();
            return Ok(Mapper.Map(locations));
        }

        [HttpGet("distance")]
        public IActionResult GetLocationsInArea(double maxDistance, double latitude, double longitude)
        {
            maxDistance *= 1000;
            var center = new Point(longitude, latitude) { SRID = 4326 };
            var locations = _context.Set<Location>().Where(x => x.GeoLocation.Distance(center) <= maxDistance).ToList();
            return Ok(Mapper.Map(locations));
        }

        [HttpGet("distanceLocal")]
        public IActionResult GetLocationsInAreaLocal(double maxDistance, double latitude, double longitude)
        {
            maxDistance *= 1000;
            var center = new Point(longitude, latitude) { SRID = 4326 }.ProjectTo(20936);
            var locations = _context.Set<Location>().ToList();
            locations.ForEach(x => x.GeoLocation = x.GeoLocation.ProjectTo(20936));
            var result = locations.Where(x => x.GeoLocation.Distance(center) <= maxDistance);
            return Ok(Mapper.Map(result));
        }

        [HttpGet("distanceFromLocation")]
        public IActionResult GetLocationsAroundLocation(double maxDistance, string locationName)
        {
            maxDistance *= 1000;
            var location = _context.Set<Location>().FirstOrDefault(x => x.Name == locationName);
            if (location == null) return NotFound();
            var locationMapped = Mapper.Map(location);
            var center = new Point(locationMapped.Longitude, locationMapped.Latitude) { SRID = 4326 };
            var locations = _context.Set<Location>().Where(x => x.GeoLocation.Distance(center) <= maxDistance).ToList();
            return Ok(Mapper.Map(locations));
        }


        [HttpPost]
        public IActionResult PostLocation(LocationDto dto)
        {
            var location = Mapper.Map(dto);
            _context.Set<Location>().Add(location);
            _context.SaveChanges();
            dto.Id = location.Id;
            return Ok(dto);
        }

        [HttpPost("api")]
        public async Task<IActionResult> PostLocationQuery(string query)
        {
            GeoLocation coordinates;
            try
            {
                coordinates = await _geoCoordinatesService.GetGeoLocationAsync(query, new CancellationToken());
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            var dto = new LocationDto
            {
                Name = coordinates.Name!,
                Latitude = coordinates.Latitude,
                Longitude = coordinates.Longitude
            };
            var location = Mapper.Map(dto);
            _context.Set<Location>().Add(location);
            await _context.SaveChangesAsync();
            dto.Id = location.Id;
            return Ok(dto);
        }

        [HttpDelete("all")]
        public IActionResult Delete()
        {
            var locations = _context.Set<Location>();

            _context.Set<Location>().RemoveRange(locations);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var location = _context.Set<Location>().FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Set<Location>().Remove(location);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
