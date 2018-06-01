using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS_WPF
{
    class Utilities
    {
		public static float RoundTo(float number, int decimals)
		{
			if (System.Math.Abs(decimals) > 28)
				throw new System.ArgumentOutOfRangeException("roundTo", "Maximum rounding allowed before or after the decimal is 28 places.");

			if (decimals >= 0)
				return (float)Math.Round(number, decimals);

			float roundFactor = (float)System.Math.Pow(10, -decimals);
			return (float)Math.Round(number / roundFactor, 0) * roundFactor;
		}
	}
}
