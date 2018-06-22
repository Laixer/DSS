using System;

namespace Dss
{
	/// <summary>
	/// Object storing the model data for a SeriesCollection,
	/// so we can easily pass these configurations around
	/// instead of having to pass a bunch of variables.
	/// </summary>
	public class SeriesCollectionConfiguration
	{
		private SeriesCollectionType[] types;
		private DataPoint[] _dataPoints;

		/// <summary>
		/// The types of SeriesCollections for this configuration.
		/// This is an array because there can be multiple
		/// SeriesCollections in a single CartesianChart.
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// true if this configuration is for a SeriesCollection
		/// which should be displayed using a logarithmic x axis.
		/// This is used by the ResultScrollViewer to adjust the axes of the charts.
		/// </summary>
		public bool HasLogarithmicX { get; set; }
		/// <summary>
		/// true if this configuration is for a SeriesCollection
		/// which should be displayed using a logarithmic x axis.
		/// This is used by the ResultScrollViewer to adjust the axes of the charts.
		/// </summary>
		public bool HasLogarithmicY { get; set; }
	}
}
