using System;
using System.Collections;

namespace ThisRoofN.Extensions
{
	public static class EnumerableExtension
	{
		public static int Count(this IEnumerable source)
		{
			int res = 0;

			foreach (var item in source)
				res++;

			return res;
		}
	}
}

