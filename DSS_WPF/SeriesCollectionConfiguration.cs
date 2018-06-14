using System;

namespace Dss
{
	public class SeriesCollectionConfiguration
	{
		private SeriesCollectionType[] types;
		private DataPoint[] _dataPoints;

		public SeriesCollectionType[] GetTypes()
		{
			return types;
		}

		public void SetTypes(SeriesCollectionType[] value)
		{
			types = value;
		}

		public SpecificTestInformation TestInformation { get; set; }

		public DataPoint[] GetDataPoints()
		{
			return _dataPoints;
		}

		public void SetDataPoints(DataPoint[] value)
		{
			_dataPoints = value;
		}

		public bool HasLogarithmicX { get; set; }
		public bool HasLogarithmicY { get; set; }
	}
}
