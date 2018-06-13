using System;

namespace Dss
{
	class GeneralDataEntry
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public String Property { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public String Value { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public String Unit { get; set; }

		public GeneralDataEntry(string property, string value, string unit)
		{
			Property = property;
			Value = value;
			Unit = unit;
		}
	}
}
