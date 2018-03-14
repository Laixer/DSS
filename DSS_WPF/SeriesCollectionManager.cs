using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Configurations;
using System.Diagnostics;
using System.Windows.Media;

namespace DSS_WPF
{
	public enum SeriesCollectionType { ShearStrainHorizontalStress, NormalStressShearStress, HorizontalStrainSecantGModulus, ShearStrainPorePressure, ShearStrainNormalStress, TimeAxialStrain }


	static class SeriesCollectionManager
	{
		static public SeriesCollection SeriesCollectionForType(SeriesCollectionType type, DataPoint[] result, Boolean logarithmic)
		{
			return SeriesCollectionForTypes(new SeriesCollectionType[] { type }, result, logarithmic);
		}

		static public SeriesCollection SeriesCollectionForTypes(SeriesCollectionType[] types, DataPoint[] result, Boolean logarithmic)
		{
			LineSeries[] Series = new LineSeries[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				Series[i] = LineSeriesForType(types[i], result);
			}

			SeriesCollection Collection;
			if (!logarithmic)
			{
				Collection = new SeriesCollection();
			}
			else
			{
				var mapper = Mappers.Xy<ObservablePoint>()
					.X(point => Math.Log(point.X + .0001, 10)) // a 10 base log scale in the X axis
					.Y(point => point.Y);

				Collection = new SeriesCollection(mapper);
			}

			Collection.AddRange(Series);

			return Collection;
		}

		static public LineSeries LineSeriesForType(SeriesCollectionType type, DataPoint[] result)
		{
			ChartValues<ObservablePoint> Points = new ChartValues<ObservablePoint>();
			int Length;
			int Stage1Length = 0;
			int StartOfStage2Index = 0;
			for (int i = 0; i < result.Length; i++)
			{
				if (result[i].stage_number == 2)
				{
					Stage1Length = i - 1;
					StartOfStage2Index = i;
					break;
				}
			}

			if (Stage1Length == 0 || StartOfStage2Index == 0)
			{
				throw new Exception("There must be two stages in the input file");
			}

			Func<DataPoint, ObservablePoint> PointGenerationFunc;

			switch (type)
			{
				case SeriesCollectionType.ShearStrainHorizontalStress:
					Length = result.Length;
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.horizontal_strain,
						Y = dataPoint.horizontal_stress
					});
					break;
				case SeriesCollectionType.NormalStressShearStress:
					Length = result.Length - StartOfStage2Index;
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.normal_stress,
						Y = dataPoint.horizontal_stress
					});
					break;
				case SeriesCollectionType.TimeAxialStrain:
					Length = Stage1Length;
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.time_since_start_test,
						Y = dataPoint.axial_strain
					});
					break;
				case SeriesCollectionType.ShearStrainNormalStress:
					Length = result.Length;
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.horizontal_strain,
						Y = dataPoint.normal_stress
					});
					break;
				case SeriesCollectionType.ShearStrainPorePressure:
					Length = result.Length;
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.horizontal_strain,
						Y = 100.0 - dataPoint.normal_stress
					});
					break;

				default:
					throw new System.ArgumentException("Unsupported type");
			}

			ObservablePoint[] PointsToAdd = new ObservablePoint[Length];

			switch (type)
			{
				case SeriesCollectionType.ShearStrainHorizontalStress:
					for (int i = 0; i < result.Length; i++)
					{
						PointsToAdd[i] = PointGenerationFunc(result[i]);
					}
					break;
				case SeriesCollectionType.NormalStressShearStress:
					for (int i = StartOfStage2Index; i < result.Length; i++)
					{
						PointsToAdd[i - StartOfStage2Index] = PointGenerationFunc(result[i]);
					}
					break;
				case SeriesCollectionType.TimeAxialStrain:
					for (int i = 0; i < Stage1Length; i++)
					{
						PointsToAdd[i] = PointGenerationFunc(result[i]);
					}
					break;
				case SeriesCollectionType.ShearStrainNormalStress:
					for (int i = 0; i < result.Length; i++)
					{
						PointsToAdd[i] = PointGenerationFunc(result[i]);
					}
					break;
				case SeriesCollectionType.ShearStrainPorePressure:
					for (int i = 0; i < result.Length; i++)
					{
						PointsToAdd[i] = PointGenerationFunc(result[i]);
					}
					break;

				default:
					throw new Exception("incomplete switch");

			}

			Points.AddRange(PointsToAdd);

			return new LineSeries
			{
				Values = Points,
				StrokeThickness = 1,
				PointGeometrySize = 1,
				Fill = Brushes.Transparent
			};
		}
	}

	

}
