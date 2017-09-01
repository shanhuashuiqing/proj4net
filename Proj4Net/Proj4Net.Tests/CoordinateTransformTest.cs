/**
 * Tests correctness and accuracy of Coordinate System transformations.
 * 
 * @author Martin Davis
 *
 */
using System;
using NUnit.Framework;

namespace Proj4Net.Tests
{
    [TestFixture]
    public class CoordinateTransformTest : BaseCoordinateTransformTest
    {
        public CoordinateTransformTest()
            : base("CoordinateTransformTest")
        {
        }

        [Test]
        public void testFirst()
        {
            CheckTransform("EPSG:4230", 5, 58, "EPSG:2192", 764566.84, 3343948.93, 0.01);
            CheckTransform("EPSG:4258", 5.0, 70.0,    "EPSG:3035", 4041548.12525335, 4109791.65987687, 0.1 );

            CheckTransform("EPSG:4326", 3.8142776, 51.285914, "EPSG:23031", 556878.9016076007, 5682145.166264554, 0.1);

            CheckTransformFromWGS84("+proj=sterea +lat_0=52.15616055555555 +lon_0=5.38763888888889 +k=0.9999079 +x_0=155000 +y_0=463000 +ellps=bessel +towgs84=565.237,50.0087,465.658,-0.406857,0.350733,-1.87035,4.0812 +units=m +no_defs",
                5.387638889, 52.156160556, 155029.78919920223, 463109.9541111593);
            //CheckTransformToGeo("EPSG:28992", 148312.15, 457804.79, 5.29, 52.11, 0.01);
            //checkTransformFromWGS84("EPSG:3785",     -76.640625, 49.921875,  -8531595.34908, 6432756.94421   );  
            //CheckTransformFromGeo("EPSG:3785", -76.640625, 49.921875, -8531595.34908, 6432756.94421);
        }

        [Test]
        public void testEPSG_27700()
        {
            CheckTransform("EPSG:4326", -2.89, 55.4, "EPSG:27700", 343733.1404, 612144.530677, 0.1);
            CheckTransform("EPSG:27700", 398089, 383867, "EPSG:4326", -2, 53, 1);
        }

        [Test, Description("Issue 7809")]
        public void testEPSG_27572()
        {
            CheckTransform("EPSG:27572", 599203.06000596, 2430245.5504736, "EPSG:4326", 2.3256481, 48.8705277, 0.001);
        }

        /**
         * Tests use of 3 param transform
         */
        [Test]
        public void TestEPSG_23031()
        {
            CheckTransform("EPSG:4326", 3.8142776, 51.285914, "EPSG:23031", 556878.9016076007, 5682145.166264554, 0.1);
        }

        /**
         * Tests use of 7 param transform
         */
        [Test]
        public void TestAmersfoort_RD_New()
        {
            CheckTransformFromWGS84("EPSG:28992", 5.387638889, 52.156160556, 155029.79409195564, 463109.95436430885);
        }

        [Test]
        public void testPROJ4_SPCS_NAD27()
        {
            // AK 2
            CheckTransform("+proj=longlat +datum=NAD27 +to_meter=0.3048006096012192", -142.0, 56.50833333333333, "ESRI:26732", 500000.000, 916085.508, 0.1);
            /**
             * EPSG:4267 is the CRS for NAD27 Geographics.
             * Even though ESRI:26732 is also NAD27,  
             * the transform fails, because EPSG:4267 specifies datum transform params.
             * This causes a datum transformation to be attempted, 
             * which fails because the target does not specify datum transform params
             * A more sophisticated check for datum equivalence might prevent this failure
             */
            //    checkTransform("EPSG:4267", -142.0, 56.50833333333333,    "ESRI:26732", 500000.000,    916085.508, 0.1 );    
        }

        [Test]
        public void testPROJ4_SPCS_NAD83()
        {
            CheckTransform("EPSG:4269", -142.0, 56.50833333333333, "ESRI:102632", 1640416.667, 916074.825, 0.1);
            CheckTransform("EPSG:4269", -146.0, 56.50833333333333, "ESRI:102633", 1640416.667, 916074.825, 0.1);
            CheckTransform("EPSG:4269", -150.0, 56.50833333333333, "ESRI:102634", 1640416.667, 916074.825, 0.1);
            CheckTransform("EPSG:4269", -152.48225944444445, 60.89132361111111, "ESRI:102635", 1910718.662, 2520810.68, 0.1);

            // AK 2 using us-ft
            CheckTransform("EPSG:4269", -142.0, 56.50833333333333, "+proj=tmerc +datum=NAD83 +lon_0=-142 +lat_0=54 +k=.9999 +x_0=500000 +y_0=0 +units=us-ft", 1640416.667, 916074.825, 0.1);
        }
        [Test]
        public void testLambertConformalConic()
        {
            // Landon's test pt 
            CheckTransformFromWGS84("EPSG:2227", -121.3128278, 37.95657778, 6327319.23, 2171792.15, 0.01);

            // from GIGS Test Suite - seems to have a very large discrepancy
            //checkTransform("EPSG:4230", 5, 58, "EPSG:2192", 764566.84, 3343948.93, 0.01 );

            CheckTransformFromGeo("+proj=lcc +lat_1=30.0 +lon_0=-50.0 +datum=WGS84 +units=m +no_defs",
                -123.1, 49.2166666666, -5287947.56661412, 3923289.38044914, 0.01);
    

        }

        [Test]
        public void testStereographic()
        {
            CheckTransformFromWGS84("EPSG:3031", 0, -75, 0, 1638783.238407);
            CheckTransformFromWGS84("EPSG:3031", -57.65625, -79.21875, -992481.633786, 628482.06328);
        }

        [Test]
        public void testUTM()
        {
            CheckTransformFromGeo("EPSG:23030", -3, 49.95, 500000, 5533182.925903, 0.1);
            CheckTransformFromWGS84("EPSG:32615", -93, 42, 500000, 4649776.22482);
            CheckTransformFromWGS84("EPSG:32612", -113.109375, 60.28125, 383357.429537, 6684599.06392);
        }

        [Test]
        public void testMercator()
        {
            //    google CRS
            CheckTransformFromWGS84("EPSG:3785", -76.640625, 49.921875, -8531595.34908, 6432756.94421);
        }

        [Test]
        public void testSterea()
        {
            CheckTransformToGeo("EPSG:28992", 148312.15, 457804.79, 5.29, 52.11, 0.001);
        }
  
        [Test]
        public void testAlbersEqualArea()
        {
            CheckTransformFromWGS84("EPSG:3005", -126.54, 54.15, 964813.103719, 1016486.305862);
            // # NAD83(CSRS) / BC Albers
            CheckTransformFromWGS84("EPSG:3153", -127.0, 52.11, 931625.9111828626, 789252.646454557);
        }

        [Test]
        public void testLambertAzimuthalEqualArea()
        {
            CheckTransformFromGeo("EPSG:3573", 9.84375, 61.875, 2923052.02009, 1054885.46559);
            // Proj4js
            CheckTransform("EPSG:4258", 11.0, 53.0, "EPSG:3035", 4388138.60, 3321736.46, 0.1);
            CheckTransformAndInverse("EPSG:4258", 11.0, 53.0, "EPSG:3035", 4388138.60, 3321736.46, 0.1, 2 * ApproximateMeterInDegrees);

            // test values from GIGS test suite - which are suspect
            // Proj4J actual values agree with PROJ4
            //checkTransform("EPSG:4258", 5.0, 50.0,    "EPSG:3035", 3892127.02, 1892578.96, 0.1 );
            //checkTransform("EPSG:4258", 5.0, 70.0,    "EPSG:3035", 4041548.12525335, 4109791.65987687, 0.1 );
        }

        public void testSwissObliqueMercator()
        {
            // from PROJ.4
            CheckTransformFromAndToGeo("EPSG:21781", 8.23, 46.82, 660309.34, 185586.30, 0.1, 2 * ApproximateMeterInDegrees);
        }

        [Test]
        public void testEPSG_4326()
        {
            CheckTransformAndInverse(
                    "EPSG:4326", -126.54, 54.15,
                    "EPSG:3005", 964813.103719, 1016486.305862,
                    0.0001, 0.2 * ApproximateMeterInDegrees);

            CheckTransformAndInverse(
                    "EPSG:32633", 249032.839239894, 7183612.30572229,
                    "EPSG:4326", 9.735465995810884, 64.68347938257097,
                    0.000001, 0.3 * ApproximateMeterInDegrees);

            CheckTransformAndInverse(
                "EPSG:32636", 500000, 4649776.22482,
                "EPSG:4326", 33, 42,
                0.000001, 20 * ApproximateMeterInDegrees);

        }

        [Test]
        public void testParams()
        {
            CheckTransformFromWGS84("+proj=aea +lat_1=50 +lat_2=58.5 +lat_0=45 +lon_0=-126 +x_0=1000000 +y_0=0 +ellps=GRS80 +units=m ",
                -127.0, 52.11, 931625.9111828626, 789252.646454557, 0.0001);
        }

        /**
         * Values confirmed with PROJ.4 (Rel. 4.4.6, 3 March 2003)
         */
        [Test]
        public void testPROJ4()
        {
            CheckTransformFromGeo("EPSG:27492", -7.84, 39.58, 25260.78, -9668.93, 0.1);
            CheckTransformFromGeo("EPSG:27700", -2.89, 55.4, 343642.04, 612147.04, 0.1);
            CheckTransformFromGeo("EPSG:31285", 13.33333333333, 47.5, 450000.00, 5262298.75, 0.1);
            CheckTransformFromGeo("EPSG:31466", 6.685, 51.425, 2547638.72, 5699005.05, 0.1);
            CheckTransformFromGeo("EPSG:2736", 34.0, -21.0, 603934.39, 7677664.39, 0.1);
            CheckTransformFromGeo("EPSG:26916", -86.6056, 34.579, 536173.11, 3826428.04, 0.1);
            CheckTransformFromGeo("EPSG:21781", 8.23, 46.82, 660309.34, 185586.30, 0.1);
        }

        [Test]
        public void testPROJ4_LargeDiscrepancy()
        {
            CheckTransformFromGeo("EPSG:29100", -53.0, 5.0, 5110899.06, 10552971.67, 4000);
        }

        [Test, Ignore]
        public void XtestUndefined()
        {


            //runInverseTransform("EPSG:27492",    25260.493584, -9579.245052,    -7.84, 39.58);
            //runInverseTransform("EPSG:27563",    653704.865208, 176887.660037,    3.005, 43.89);
            //runInverseTransform("EPSG:54003",    1223145.57,6491218.13,-6468.21,    11.0, 53.0);


            //    runTransform("EPSG:31467",   9, 51.165,       3500072.082451, 5670004.744777   );

            CheckTransformFromWGS84("EPSG:54008", 11.0, 53.0, 738509.49, 5874620.38);


            CheckTransformFromWGS84("EPSG:102026", 40.0, 40.0, 3000242.40, 5092492.64);
            CheckTransformFromWGS84("EPSG:54032", -127.0, 52.11, -4024426.19, 6432026.98);

            CheckTransformFromWGS84("EPSG:42304", -99.84375, 48.515625, -358185.267976, -40271.099023);
            CheckTransformFromWGS84("EPSG:42304", -99.84375, 48.515625, -358185.267976, -40271.099023);
            //    runInverseTransform("EPSG:28992",    148312.15, 457804.79, 698.48,    5.29, 52.11);
        }
    }
}