﻿using NUnit.Framework;

namespace Proj4Net.Tests
{
    public class Proj4JSTest : BaseCoordinateTransformTest
    {
        public Proj4JSTest()
            : base("Proj4JSTest")
        {
        }


        [Test]
        public void testGood()
        {
            CheckTransformFromGeo("EPSG:23030", -6.77432123185356, 37.88456231505968, 168035.13, 4199884.83, 100);
            CheckTransformFromGeo("EPSG:2403", 81.0, 37.92, 2.75E7, 4198690.08, 3);
            CheckTransformFromGeo("EPSG:29100", -53.0, 5.0, 5110899.06, 1.055297181E7, 4000);
            CheckTransformFromGeo("EPSG:3031", -57.65625, -79.21875, -992481.633786, 628482.06328, 0.1);
            CheckTransformFromGeo("EPSG:3035", 11.0, 53.0, 4388138.6, 3321736.46, 0.1);
            CheckTransformFromGeo("EPSG:3153", -127.0, 52.11, 931625.91, 789252.65, 0.1);
            CheckTransformFromGeo("EPSG:32612", -113.109375, 60.28125, 383357.429537, 6684599.06392, 0.1);
            CheckTransformFromGeo("EPSG:32615", -93.0, 42.0, 500000.0, 4649776.22482, 0.1);
            CheckTransformFromGeo("EPSG:3411", -32.0, 48.0, 1070076.44, -4635010.27, 2);
            CheckTransformFromGeo("EPSG:3573", 9.84375, 61.875, 2923052.02009, 1054885.46559, 0.1);
        }

        [Test, Ignore("Unsupported parameter exception thrown")]
        public void xtestNotImplemented()
        {
            // gamma not implemented
            CheckTransformFromGeo("EPSG:2057", -53.0, 5.0, -1.160832226E7, 1.828261223E7, 0.1);
            // somerc not implemented
            CheckTransformFromGeo("EPSG:21781", 8.23, 46.82, 660389.52, 185731.63, 0.1);
            // PM not supported
            CheckTransformFromGeo("EPSG:27563", 3.005, 43.89, 653704.865208, 176887.660037, 0.1);
        }

        [Test]
        public void testLargeDiscrepancy()
        {
            // Proj4J matches PROJ.4
            CheckTransformFromGeo("EPSG:2736", 34.0, -21.0, 603933.4, 7677505.64, 200);

            // Proj4J matches PROJ.4
            CheckTransformFromGeo("EPSG:26916", -86.6056, 34.579, 5110899.06, 10552971.81, 7000000);

            // Proj4J matches PROJ.4
            CheckTransformFromGeo("EPSG:21781", 8.23, 46.82, 660389.52, 185731.63, 200);

            CheckTransformFromGeo("EPSG:27700", -2.89, 55.4, 343733.14, 612144.53, 100);
            CheckTransformFromGeo("EPSG:27492", -7.84, 39.58, 25260.493584, -9579.245052, 100);
            CheckTransformFromGeo("EPSG:28992", 5.29, 52.11, 148312.15, 457804.79, 100);
            CheckTransformFromGeo("EPSG:31285", 13.33333333333, 47.5, 450055.7, 5262356.33, 100);
            CheckTransformFromGeo("EPSG:31466", 6.685, 51.425, 2547685.01212, 5699155.7345, 200);
        }

        [Test, Ignore("Unknown CRS")]
        public void xtestUnknownCRS()
        {
            CheckTransformFromGeo("EPSG:102026", 40.0, 40.0, 3000242.4, 5092492.64, 0.1);
            CheckTransformFromGeo("EPSG:42304", -99.84375, 48.515625, -358185.267976, -40271.099023, 0.1);
            CheckTransformFromGeo("EPSG:54003", 11.0, 53.0, 1223145.57, 6491218.13, 0.1);
            CheckTransformFromGeo("EPSG:54008", 11.0, 53.0, 738509.49, 5874620.38, 0.1);
            CheckTransformFromGeo("EPSG:54009", -119.0, 34.0, -1.061760279013849E7, 4108337.84708608, 0.1);
            CheckTransformFromGeo("EPSG:54029", 11.0, 53.0, 1094702.5, 6496789.74, 0.1);
            CheckTransformFromGeo("EPSG:54032", -127.0, 52.11, -4024426.19, 6432026.98, 0.1);
            //CheckTransformFromGeo("google", -76.640625, 49.921875, -8531595.34908, 6432756.94421, 0.1);
        }
    }
}