using UnityEngine;

namespace Core.Helper
{
	internal static class ExtensionMethods
	{
		/// <summary>
		/// Rounds Vector2.
		/// </summary>
		/// <param name="v2"></param>
		/// <param name="decimalPlaces"></param>
		/// <returns></returns>
		public static Vector2 Round(this Vector2 v2, int decimalPlaces = 2)
		{
			var multiplier = 1f;
			for (var i = 0; i < decimalPlaces; i++) multiplier *= 10f;
			return new Vector2(
				Mathf.Round(v2.x * multiplier) / multiplier,
				Mathf.Round(v2.y * multiplier) / multiplier
			);
		}
	}
}