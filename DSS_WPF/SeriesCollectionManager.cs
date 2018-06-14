using System;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
//using LiveCharts.Geared;
using LiveCharts.Configurations;
using System.Windows.Media;

namespace Dss
{
	public enum SeriesCollectionType { ShearStrainHorizontalStress, NormalStressShearStress, HorizontalStrainSecantGModulus, ShearStrainPorePressure, ShearStrainNormalStress, TimeAxialStrain }


	static class SeriesCollectionManager
	{ 
		static public SeriesCollection SeriesCollectionForConfiguration(SeriesCollectionConfiguration configuration)
		{

			LineSeries[] Series = new LineSeries[configuration.GetTypes().Length];
			for (int i = 0; i < configuration.GetTypes().Length; i++)
			{
				Series[i] = LineSeriesForType(configuration.GetTypes()[i], configuration.GetDataPoints());
			}

			SeriesCollection collection;
			CartesianMapper<ObservablePoint> Mapper = Mappers.Xy<ObservablePoint>();
			if (configuration.HasLogarithmicX)
			{
				Mapper.X(point => Math.Log(point.X + .000001, 10));
			} else
			{
				Mapper.X(point => point.X);
			}

			if (configuration.HasLogarithmicY)
			{
				Mapper.Y(point => Math.Log(point.Y + .000001, 10));
			}
			else
			{
				Mapper.Y(point => point.Y);
			}

			collection = new SeriesCollection(Mapper);
			collection.AddRange(Series);



			return collection;
		}

		static public LineSeries LineSeriesForType(SeriesCollectionType type, DataPoint[] result)
		{
			int Stage1Length = 0;
			int StartOfStage2Index = 0;
			for (int i = 0; i < result.Length; i++)
			{
				if (result[i].StageNumber == 2)
				{
					Stage1Length = i - 1;
					StartOfStage2Index = i;
					break;
				}
			}

			if (Stage1Length == 0 || StartOfStage2Index == 0)
			{
				throw new ArgumentException("There must be two stages in the input file");
				
			}

			Func<DataPoint, ObservablePoint> PointGenerationFunc = getPointGenerationFunc(type, result);
			int Length = GetLengthForType(type, result, Stage1Length, StartOfStage2Index);
			ObservablePoint[] PointsToAdd = generatePoints(type, result, PointGenerationFunc, Length, Stage1Length, StartOfStage2Index);

			ChartValues<ObservablePoint> Points = new ChartValues<ObservablePoint>();
			Points.AddRange(PointsToAdd);
			//Points.Quality = Quality.Low;

			return new LineSeries
			{
				Values = Points,
				StrokeThickness = 1,
				PointGeometrySize = 1,
				Fill = Brushes.Transparent
			};
		}

		static private ObservablePoint[] generatePoints(SeriesCollectionType type, DataPoint[] result, Func<DataPoint, ObservablePoint> func, int length, int stage1Length, int startOfStage2Index)
		{
			ObservablePoint[] PointsToAdd = new ObservablePoint[length];
			switch (type)
			{
				case SeriesCollectionType.ShearStrainHorizontalStress:
					for (int i = 0; i < result.Length; i++)
					{
						PointsToAdd[i] = func(result[i]);
					}
					break;
				case SeriesCollectionType.NormalStressShearStress:
					for (int i = startOfStage2Index; i < result.Length; i++)
					{
						PointsToAdd[i - startOfStage2Index] = func(result[i]);
					}
					break;
				case SeriesCollectionType.TimeAxialStrain:
					for (int i = 0; i < stage1Length; i++)
					{
						PointsToAdd[i] = func(result[i]);
					}
					break;
				case SeriesCollectionType.ShearStrainNormalStress:
					for (int i = 0; i < result.Length; i++)
					{
						PointsToAdd[i] = func(result[i]);
					}
					break;
				case SeriesCollectionType.ShearStrainPorePressure:
					for (int i = 0; i < result.Length; i++)
					{
						PointsToAdd[i] = func(result[i]);
					}
					break;

				case SeriesCollectionType.HorizontalStrainSecantGModulus:
					for (int i = startOfStage2Index; i < result.Length; i++)
					{
						PointsToAdd[i - startOfStage2Index] = func(result[i]);
					}
					break;

				default:
					throw new ArgumentException("incomplete switch");

			}

			return PointsToAdd;
		}

		public static Func<DataPoint, ObservablePoint> getPointGenerationFunc(SeriesCollectionType type, DataPoint[] result)
		{
			Func<DataPoint, ObservablePoint> PointGenerationFunc;

			switch (type)
			{
				case SeriesCollectionType.ShearStrainHorizontalStress:
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.HorizontalStrain,
						Y = dataPoint.HorizontalStress
					});
					break;
				case SeriesCollectionType.NormalStressShearStress:
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.NormalStress,
						Y = dataPoint.HorizontalStress
					});
					break;
				case SeriesCollectionType.TimeAxialStrain:
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.TimeSinceStartTest,
						Y = dataPoint.AxialStrain
					});
					break;
				case SeriesCollectionType.ShearStrainNormalStress:
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.HorizontalStrain,
						Y = dataPoint.NormalStress
					});
					break;
				case SeriesCollectionType.ShearStrainPorePressure:
					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.HorizontalStrain,
						Y = 100.0 - dataPoint.NormalStress
					});
					break;

				case SeriesCollectionType.HorizontalStrainSecantGModulus:
					double StrainStartShear = 0.0;
					double StressStartShear = 0.0;
					bool Found = false;
					//double Correction = 1.28;
					foreach (DataPoint point in result)
					{
						if (point.StageNumber == 2)
						{
							StrainStartShear = point.HorizontalStrain;
							StressStartShear = point.HorizontalStress;
							Found = true;
							break;
						}
					}

					if (!Found)
					{
						return null;
					}

					PointGenerationFunc = dataPoint => (new ObservablePoint
					{
						X = dataPoint.HorizontalStrain,
						Y = ((dataPoint.HorizontalStress - StressStartShear) / (dataPoint.HorizontalStrain - StrainStartShear)) / 10.0
					});

					break;

				default:
					throw new System.ArgumentException("Unsupported type");
			}

			return PointGenerationFunc;
		}

		static int GetLengthForType(SeriesCollectionType type, DataPoint[] result, int Stage1Length, int StartOfStage2Index)
		{
			int Length;
			switch (type)
			{
				case SeriesCollectionType.ShearStrainHorizontalStress:
					Length = result.Length;
					break;
				case SeriesCollectionType.NormalStressShearStress:
					Length = result.Length - StartOfStage2Index;
					break;
				case SeriesCollectionType.TimeAxialStrain:
					Length = Stage1Length;
					break;
				case SeriesCollectionType.ShearStrainNormalStress:
					Length = result.Length;
					break;
				case SeriesCollectionType.ShearStrainPorePressure:
					Length = result.Length;
					break;

				case SeriesCollectionType.HorizontalStrainSecantGModulus:
					Length = result.Length - StartOfStage2Index;
					break;

				default:
					throw new ArgumentException("Unsupported type");
			}

			return Length;
		}
	}
}
