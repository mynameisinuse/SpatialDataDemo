using System.Resources;
using System.Runtime.Serialization;

namespace SpatialDataDemo
{

    [DataContract]
    public class Response
    {
        [DataMember(Name = "copyright", EmitDefaultValue = false)]
        public string Copyright { get; set; }

        [DataMember(Name = "brandLogoUri", EmitDefaultValue = false)]
        public string BrandLogoUri { get; set; }

        [DataMember(Name = "statusCode", EmitDefaultValue = false)]
        public int StatusCode { get; set; }

        [DataMember(Name = "statusDescription", EmitDefaultValue = false)]
        public string StatusDescription { get; set; }

        [DataMember(Name = "authenticationResultCode", EmitDefaultValue = false)]
        public string AuthenticationResultCode { get; set; }

        [DataMember(Name = "errorDetails", EmitDefaultValue = false)]
        public string[] errorDetails { get; set; }

        [DataMember(Name = "traceId", EmitDefaultValue = false)]
        public string TraceId { get; set; }

        [DataMember(Name = "resourceSets", EmitDefaultValue = false)]
        public ResourceSets[] ResourceSets { get; set; }
    }

    public class ResourceSets
    {
        [DataMember(Name = "estimatedTotal", EmitDefaultValue = false)]
        public long EstimatedTotal { get; set; }

        [DataMember(Name = "resources", EmitDefaultValue = false)]
        public Resource[] Resources { get; set; }
    }

    public class Resource
    {
        [DataMember(Name = "bbox", EmitDefaultValue = false)]
        public double[] BoundingBox { get; set; }

        [DataMember(Name = "__type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "point", EmitDefaultValue = false)]
        public BingPoint Point { get; set; }

        [DataMember(Name = "entityType", EmitDefaultValue = false)]
        public string EntityType { get; set; }

        [DataMember(Name = "address", EmitDefaultValue = false)]
        public Address Address { get; set; }

        [DataMember(Name = "confidence", EmitDefaultValue = false)]
        public string Confidence { get; set; }

        [DataMember(Name = "matchCodes", EmitDefaultValue = false)]
        public string[] MatchCodes { get; set; }

        [DataMember(Name = "geocodePoints", EmitDefaultValue = false)]
        public BingPoint[] GeocodePoints { get; set; }

        [DataMember(Name = "queryParseValues", EmitDefaultValue = false)]
        public QueryParseValue[] QueryParseValues { get; set; }
    }

    [DataContract]
    public class QueryParseValue
    {
        [DataMember(Name = "property", EmitDefaultValue = false)]
        public string Property { get; set; }

        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    [DataContract]
    public class BingPoint
    {
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>  
        /// Latitude,Longitude  
        /// </summary>  
        [DataMember(Name = "coordinates", EmitDefaultValue = false)]
        public double[] Coordinates { get; set; }

        [DataMember(Name = "calculationMethod", EmitDefaultValue = false)]
        public string CalculationMethod { get; set; }

        [DataMember(Name = "usageTypes", EmitDefaultValue = false)]
        public string[] UsageTypes { get; set; }
    }
    public class Address
    {
        [DataMember(Name = "addressLine", EmitDefaultValue = false)]
        public string AddressLine { get; set; }

        [DataMember(Name = "adminDistrict", EmitDefaultValue = false)]
        public string AdminDistrict { get; set; }

        [DataMember(Name = "adminDistrict2", EmitDefaultValue = false)]
        public string AdminDistrict2 { get; set; }

        [DataMember(Name = "countryRegion", EmitDefaultValue = false)]
        public string CountryRegion { get; set; }

        [DataMember(Name = "countryRegionIso2", EmitDefaultValue = false)]
        public string CountryRegionIso2 { get; set; }

        [DataMember(Name = "formattedAddress", EmitDefaultValue = false)]
        public string FormattedAddress { get; set; }

        [DataMember(Name = "locality", EmitDefaultValue = false)]
        public string Locality { get; set; }

        [DataMember(Name = "postalCode", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        [DataMember(Name = "neighborhood", EmitDefaultValue = false)]
        public string Neighborhood { get; set; }

        [DataMember(Name = "landmark", EmitDefaultValue = false)]
        public string Landmark { get; set; }
    }
}
