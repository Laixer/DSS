using System;

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
