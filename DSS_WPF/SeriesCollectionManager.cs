using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace DSS_WPF
{
	public enum SeriesCollectionType { ShearStrainHorizontalStress, NormalStressShearStress, HorizontalStrainSecantGModulus, ShearStrainPorePressureNormalStress, TimeAxialStrain }


	static class SeriesCollectionManager
	{
		static public SeriesCollection SeriesCollectionForType(SeriesCollectionType type, DataPoint[] result)
		{
			SeriesCollection Collection;

			ChartValues<ObservablePoint> Points = new ChartValues<ObservablePoint>();
			ObservablePoint[] PointsToAdd = new ObservablePoint[result.Length];

			Func<DataPoint, ObservablePoint> PointGenerationFunc;

			switch (type)
			{
				case SeriesCollectionType.ShearStrainHorizontalStress:
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.horizontal_strain,
						Y = dataPoint.horizontal_stress
					});
					break;
				case SeriesCollectionType.NormalStressShearStress:
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.normal_stress,
						Y = dataPoint.horizontal_stress
					});
					break;
				default:
					throw new System.ArgumentException("Unsupported type");
			}

			for (int i = 0; i < result.Length; i++)
			{
				PointsToAdd[i] = PointGenerationFunc(result[i]);
			}

			Points.AddRange(PointsToAdd);

			Collection = new SeriesCollection
			{
				new LineSeries
				{
					Values = Points,
					StrokeThickness = 1,
					PointGeometrySize = 1
				},
			};

			return Collection;
		}
	}
}
