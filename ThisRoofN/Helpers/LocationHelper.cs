using System;

namespace ThisRoofN.Helpers
{
	public class LocationHelper
	{
		/// <summary>Converts Kilometer to latitude degrees</summary>
		/// <param name="kms">kilometer value</param>
		/// <returns>Converted Latitude</returns>
		public static double KilometersToLatitudeDegrees(double kms)
		{
			double earthRadius = 6371.0; // in kms
			double radiansToDegrees = 180.0 / Math.PI;
			return (kms / earthRadius) * radiansToDegrees;
		}

		/// <summary>
		/// Converts Kilometers to longitudinal degrees at a specified latitude
		/// </summary>
		/// <param name="kms">The KMS.</param>
		/// <param name="latitude">At latitude.</param>
		/// <returns>Converted longitude</returns>
		public static double KilometersToLongitudeDegrees(double kms, double latitude)
		{
			double earthRadius = 6371.0; // in kms
			double degreesToRadians = Math.PI / 180.0;
			double radiansToDegrees = 180.0 / Math.PI;

			// derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos(latitude * degreesToRadians);
			return (kms / radiusAtLatitude) * radiansToDegrees;
		}
	}
}

