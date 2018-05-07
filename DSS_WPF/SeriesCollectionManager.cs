using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Geared;
using LiveCharts.Configurations;
using System.Diagnostics;
using System.Windows.Media;

namespace DSS_WPF
{
	public enum SeriesCollectionType { ShearStrainHorizontalStress, NormalStressShearStress, HorizontalStrainSecantGModulus, ShearStrainPorePressure, ShearStrainNormalStress, TimeAxialStrain }


	static class SeriesCollectionManager
	{ 
		static public SeriesCollection SeriesCollectionForConfiguration(SeriesCollectionConfiguration configuration)
		{

			GLineSeries[] Series = new GLineSeries[configuration.Types.Length];
			for (int i = 0; i < configuration.Types.Length; i++)
			{
				Series[i] = LineSeriesForType(configuration.Types[i], configuration.DataPoints);
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

		static public GLineSeries LineSeriesForType(SeriesCollectionType type, DataPoint[] result)
		{
			GearedValues<ObservablePoint> Points = new GearedValues<ObservablePoint>();
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

				case SeriesCollectionType.HorizontalStrainSecantGModulus:
					Length = result.Length - StartOfStage2Index;
					double StrainStartShear = 0.0;
					double StressStartShear = 0.0;
					bool Found = false;
					//double Correction = 1.28;
					foreach (DataPoint point in result)
					{
						if (point.stage_number == 2)
						{
							StrainStartShear = point.horizontal_strain;
							StressStartShear = point.horizontal_stress;
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
						X = dataPoint.horizontal_strain,
						Y = ((dataPoint.horizontal_stress - StressStartShear) / (dataPoint.horizontal_strain - StrainStartShear)) / 10.0
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

				case SeriesCollectionType.HorizontalStrainSecantGModulus:
					for (int i = StartOfStage2Index; i < result.Length; i++)
					{
						PointsToAdd[i - StartOfStage2Index] = PointGenerationFunc(result[i]);
					}
					break;

				default:
					throw new Exception("incomplete switch");

			}

			Points.AddRange(PointsToAdd);
			//Points.Quality = Quality.Low;

			return new GLineSeries
			{
				Values = Points,
				StrokeThickness = 1,
				PointGeometrySize = 1,
				Fill = Brushes.Transparent
			};
		}
	}

	

}
