using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS_WPF
{
	public class SeriesCollectionConfiguration
	{
		public SeriesCollectionType[] Types;
		public SpecificTestInformation TestInformation;
		public DataPoint[] DataPoints;
		public Boolean HasLogarithmicX;
		public Boolean HasLogarithmicY;
	}
}
