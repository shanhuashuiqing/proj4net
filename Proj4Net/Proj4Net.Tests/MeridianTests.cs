using System;
using NUnit.Framework;

namespace Proj4Net.Tests
{
	public class MeridianTests
	{
		public MeridianTests ()
		{
		}
		
		[Test]
		public void TestNamedMeridians()
		{
			foreach (Datum.NamedMeridian nm in Enum.GetValues(typeof(Datum.NamedMeridian)))
			{
				if (nm == Datum.NamedMeridian.Unknown || nm == Datum.NamedMeridian.Undefined)
					continue;
				
				var m = Datum.Meridian.CreateByNamedMeridian(nm);
				Assert.AreEqual((int)nm, m.Code);
				Assert.AreEqual(nm, m.Name);
				Assert.AreEqual(string.Format(" +pm={0}", nm.ToString().ToLower()), m.Proj4Description);
				var c = new GeoAPI.Geometries.Coordinate(0, 0);
				m.InverseAdjust(c);
				Assert.AreEqual(m.Longitude, c.X, 1e-7);
				m.Adjust(c);
				Assert.AreEqual(0, c.X, 1e-7);
				
				var m2 = Datum.Meridian.CreateByName(nm.ToString().ToLower());
				Assert.AreEqual(m, m2);
				
				var m3 = Datum.Meridian.CreateByDegree(Utility.ProjectionMath.ToDegrees(m.Longitude));
				Assert.AreEqual(m, m3);
					
					
			}
		}
		
		[Test]
		public void TestCustomMeridian()
		{
			var degree = 5.7;
			var m = Datum.Meridian.CreateByDegree(degree);
			Assert.AreEqual(Datum.NamedMeridian.Unknown, m.Name);
			Assert.AreEqual(Utility.ProjectionMath.ToDegrees(m.Longitude), degree);
			Assert.AreEqual(string.Format(System.Globalization.NumberFormatInfo.InvariantInfo, " +pm={0}", degree), m.Proj4Description);
		}
	}
}

