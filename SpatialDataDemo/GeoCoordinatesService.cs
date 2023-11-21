using Newtonsoft.Json;


namespace SpatialDataDemo;
public class GeoCoordinatesService : IGeoCoordinatesService
{
    private readonly IConfiguration _config;

    public GeoCoordinatesService(IConfiguration config)
    {
        this._config = config;
    }

    public async Task<GeoLocation> GetGeoLocationAsync(string query, CancellationToken cancellationToken)
    {
        return await MakeApiCallBing(query, cancellationToken);
    }

    private async Task<GeoLocation> MakeApiCallBing(string query, CancellationToken cancellationToken)
    {
        var key = _config.GetConnectionString("BingMaps");
        var uri = new Uri(string.Format("http://dev.virtualearth.net/REST/v1/Locations?q={0}&key={1}", query, key));
        var json = await GetResponse(uri); 
        var result = JsonConvert.DeserializeObject<Response>(json);
        var point = result.ResourceSets.First().Resources.First();
        var location = new GeoLocation()
        {
            Latitude = point.Point.Coordinates[0],
            Longitude = point.Point.Coordinates[1],
            Name = point.Name,
        };

        return location;
    }

    //private async Task<GeoLocation> MakeApiCallAzure(string query, CancellationToken cancellationToken)
    //{
    //    var credential = new AzureKeyCredential(_config
    //        .GetConnectionString("AzureMaps")!);
    //    var mapsClient = new MapsSearchClient(credential);

    //    var searchResult = await mapsClient.SearchAddressAsync(query, null, cancellationToken);
    //    if (!searchResult.Value.Results.Any())
    //    {
    //        throw new KeyNotFoundException("Could not determine location for the given query.");
    //    }

    //    var searchAddressResultItem = searchResult.Value.Results[0];
    //    var location = new GeoLocation()
    //    {
    //        Latitude = searchAddressResultItem.Position.Latitude,
    //        Longitude = searchAddressResultItem.Position.Longitude
    //    };

    //    return location;
    //}

    private async Task<string> GetResponse(Uri uri)
    {
        HttpClient client = new();

        var response = await client.GetStringAsync(uri);
        return response;
    }
}