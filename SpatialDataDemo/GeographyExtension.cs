using NetTopologySuite.Geometries;
using ProjNet;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace SpatialDataDemo
{
    public static class GeographyExtensions
    {
        // Taken from: https://learn.microsoft.com/en-us/ef/core/modeling/spatial

        private static readonly CoordinateSystemServices CoordinateSystemServices
            = new(
                new Dictionary<int, string>
                {
                    // Coordinate systems:

                    [4326] = GeographicCoordinateSystem.WGS84.WKT,
                    [2855] =
                        @"
                        PROJCS[""NAD83(HARN) / Washington North"",
                            GEOGCS[""NAD83(HARN)"",
                                DATUM[""NAD83_High_Accuracy_Regional_Network"",
                                    SPHEROID[""GRS 1980"",6378137,298.257222101,
                                        AUTHORITY[""EPSG"",""7019""]],
                                    AUTHORITY[""EPSG"",""6152""]],
                                PRIMEM[""Greenwich"",0,
                                    AUTHORITY[""EPSG"",""8901""]],
                                UNIT[""degree"",0.01745329251994328,
                                    AUTHORITY[""EPSG"",""9122""]],
                                AUTHORITY[""EPSG"",""4152""]],
                            PROJECTION[""Lambert_Conformal_Conic_2SP""],
                            PARAMETER[""standard_parallel_1"",48.73333333333333],
                            PARAMETER[""standard_parallel_2"",47.5],
                            PARAMETER[""latitude_of_origin"",47],
                            PARAMETER[""central_meridian"",-120.8333333333333],
                            PARAMETER[""false_easting"",500000],
                            PARAMETER[""false_northing"",0],
                            UNIT[""metre"",1,
                                AUTHORITY[""EPSG"",""9001""]],
                            AUTHORITY[""EPSG"",""2855""]]
                    ",
                    [20936] = "PROJCS[\"Arc 1950 / UTM zone 36S\",\r\n    GEOGCS[\"Arc 1950\",\r\n        DATUM[\"Arc_1950\",\r\n            SPHEROID[\"Clarke 1880 (Arc)\",6378249.145,293.4663077],\r\n            TOWGS84[-143,-90,-294,0,0,0,0]],\r\n        PRIMEM[\"Greenwich\",0,\r\n            AUTHORITY[\"EPSG\",\"8901\"]],\r\n        UNIT[\"degree\",0.0174532925199433,\r\n            AUTHORITY[\"EPSG\",\"9122\"]],\r\n        AUTHORITY[\"EPSG\",\"4209\"]],\r\n    PROJECTION[\"Transverse_Mercator\"],\r\n    PARAMETER[\"latitude_of_origin\",0],\r\n    PARAMETER[\"central_meridian\",33],\r\n    PARAMETER[\"scale_factor\",0.9996],\r\n    PARAMETER[\"false_easting\",500000],\r\n    PARAMETER[\"false_northing\",10000000],\r\n    UNIT[\"metre\",1,\r\n        AUTHORITY[\"EPSG\",\"9001\"]],\r\n    AXIS[\"Easting\",EAST],\r\n    AXIS[\"Northing\",NORTH],\r\n    AUTHORITY[\"EPSG\",\"20936\"]]"
                });

        public static Point ProjectTo(this Point geometry, int srid)
        {
            var transformation = CoordinateSystemServices.CreateTransformation(geometry.SRID, srid);

            var result = (Point)geometry.Copy();
            result.Apply(new MathTransformFilter(transformation.MathTransform));

            return result;
        }
        
        private class MathTransformFilter : ICoordinateSequenceFilter
        {
            private readonly MathTransform _transform;

            public MathTransformFilter(MathTransform transform)
                => _transform = transform;

            public bool Done => false;
            public bool GeometryChanged => true;

            public void Filter(CoordinateSequence seq, int i)
            {
                var x = seq.GetX(i);
                var y = seq.GetY(i);
                var z = seq.GetZ(i);
                _transform.Transform(ref x, ref y, ref z);
                seq.SetX(i, x);
                seq.SetY(i, y);
                seq.SetZ(i, z);
            }
        }
    }
}
