namespace SpatialDataDemo
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
