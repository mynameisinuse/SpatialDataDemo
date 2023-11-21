namespace SpatialDataDemo
{
    public interface IGeoCoordinatesService
    {
        Task<GeoLocation> GetGeoLocationAsync(string query,
            CancellationToken cancellationToken);
    }
}
