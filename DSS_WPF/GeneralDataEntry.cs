using System;

namespace DSS_WPF
{
	class GeneralDataEntry
	{
		public String Property { get; set; }
		public String Value { get; set; }
		public String Unit { get; set; }

		public GeneralDataEntry(string property, string value, string unit)
		{
			Property = property;
			Value = value;
			Unit = unit;
		}
	}
}
