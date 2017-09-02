
using System;
using NUnit.Framework;

namespace Proj4Net.Tests
{
    /// <summary>
    /// Tests for implementation of PROJ4 features (such as projection parameters and datum transformations).
    /// This test set is not intended to test for numerical correctness of transformations.
    /// <para/>
    /// It is expected that many of these test will fail, until
    /// the tested features are implemented.
    /// </summary>
    /// <author>Martin Davis</author>
    [TestFixture]
    public class FeatureTest : BaseCoordinateTransformTest
    {
        static readonly CoordinateTransformTester tester = new CoordinateTransformTester(true);

        public FeatureTest()
            :base("FeatureTest")
        {
        }

        [Test]
        public void testDatumConversion()
        {
            //  datum conversions not yet supported
            //  +proj=tmerc +lat_0=0 +lon_0=6 +k=1 +x_0=2500000 +y_0=0 +ellps=bessel +datum=potsdam +units=m +no_defs     
            CheckTransformFromWGS84("EPSG:31466", 6.685, 51.425, 2547685.01212, 5699155.7345, 10);
        }
        [Test]
        public void testPrimeMeridian()
        {
            //# NTF (Paris) / Lambert Sud France
            //<27563> +proj=lcc +lat_1=44.10000000000001 +lat_0=44.10000000000001 +lon_0=0 +k_0=0.999877499 +x_0=600000 +y_0=200000 +a=6378249.2 +b=6356515 +towgs84=-168,-60,320,0,0,0,0 +pm=paris +units=m +no_defs  <>
            CheckTransformFromWGS84("EPSG:27563", 3.005, 43.89, 653704.865208, 176887.660037, 2);
        }
        [Test, Ignore("Need to implement +gamma parameter")]
        public void testGamma()
        {
            // from Proj4.JS
            CheckTransformFromGeo("EPSG:2057", -53.0, 5.0, -1.160832226E7, 1.828261223E7, 0.1);
        }

        [Test]
        public void testR_A()
        {
            // from proj4.js - result is out by 50,000 m
            {
                //EPSG:54003
                String prj =
                    "+proj=mill +lat_0=0 +lon_0=0 +x_0=0 +y_0=0 +R_A +ellps=WGS84 +datum=WGS84 +units=m +no_defs";
                CheckTransformFromGeo(prj, 11.0, 53.0, 1223145.57, 6491218.13, 50000);
            }
        }

        [Test]
        public void testTowgs84()
        {
            //# MGI / M31
            //<31285> +proj=tmerc +lat_0=0 +lon_0=13.33333333333333 +k=1.000000 +x_0=450000 +y_0=0 +ellps=bessel +towgs84=577.326,90.129,463.919,5.137,1.474,5.297,2.4232 +units=m +no_defs  <>
            // towgs not implemented
            CheckTransformFromGeo("EPSG:31285", 13.33333333333, 47.5, 450055.70, 5262356.33, 100);
        }

        [Test]
        public void testSouth()
        {
            // <2736> +proj=utm +zone=36 +south +ellps=clrk66 +units=m +no_defs  <>
            //from spatialreference.org
            CheckTransformFromGeo("EPSG:2736", 33.115, -19.14, 512093.765437, 7883804.406911, 0.000001);
            // from proj4.js - result is out by 200 m
            CheckTransformFromGeo("EPSG:2736", 34.0, -21.0, 603933.40, 7677505.64, 200);
        }

        [Test]//[Ignore]
        public void testLambertEqualArea()
        {
            CheckTransformFromGeo("EPSG:3035", 11.0, 53.0, 4388138.60, 3321736.46, 0.005);  //laea
            CheckTransformFromGeo("EPSG:3573", 9.84375, 61.875, 2923052.02009, 1054885.46559);
        }

        [Test]
        public void testSwissObliqueMercator()
        {
            // from proj4.js 
            CheckTransformFromGeo("EPSG:21781", 8.23, 46.82, 660389.52, 185731.63, 200);
        }

    }
}