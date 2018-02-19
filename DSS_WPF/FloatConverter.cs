using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using FileHelpers;

namespace DSS_WPF
{
	class FloatConverter: ConverterBase
	{
		public override object StringToField(string from)
		{
			Debug.WriteLine("parsing " + from);
			return float.Parse(from.Trim('"'));
		}
	}
}
