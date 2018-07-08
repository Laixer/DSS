using System;

namespace Dss
{
    /// <summary>
    /// Common project utilities.
    /// </summary>
    internal sealed class Utilities
    {
        public static float RoundTo(float number, int decimals)
        {
            if (Math.Abs(decimals) > 28)
            {
                throw new ArgumentOutOfRangeException("decimals", "Maximum rounding allowed before or after the decimal is 28 places.");
            }

            if (decimals >= 0)
            {
                return (float)Math.Round(number, decimals);
            }

            float roundFactor = (float)Math.Pow(10, -decimals);
            return (float)Math.Round(number / roundFactor, 0) * roundFactor;
        }

		private Utilities() { }
    }
}
