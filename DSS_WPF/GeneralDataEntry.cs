using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS_WPF
{
	class GeneralDataEntry
	{
		String Property;
		String Value;
		String Unit;

		public GeneralDataEntry(string property, string value, string unit)
		{
			Property = property;
			Value = value;
			Unit = unit;
		}
	}
}
