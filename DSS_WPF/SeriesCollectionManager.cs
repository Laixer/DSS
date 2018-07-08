using System;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Geared;
using LiveCharts.Configurations;
using System.Windows.Media;
using System.Diagnostics;

namespace Dss
{
	/// <summary>
	/// An enum of all the currently supported SeriesCollection types, in the naming format [property on x-axis][property on y-axis]
	/// </summary>
	public enum SeriesCollectionType { ShearStrainHorizontalStress, NormalStressShearStress, HorizontalStrainSecantGModulus, ShearStrainPorePressure, ShearStrainNormalStress, TimeAxialStrain }

	/// <summary>
	/// This class is responsible for doing the actual calculation of the values for the points in the graphs.
	/// </summary>
	static class SeriesCollectionManager
	{
		/// <summary>
		/// Returns a SeriesCollection set up with the values of a certain configuration.
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		static public SeriesCollection SeriesCollectionForConfiguration(SeriesCollectionConfiguration configuration)
		{

			GLineSeries[] Series = new GLineSeries[configuration.GetTypes().Length];
			for (int i = 0; i < configuration.GetTypes().Length; i++)
			{
				Series[i] = LineSeriesForType(configuration.GetTypes()[i], configuration.GetDataPoints(), configuration);
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

		/// <summary>
		/// Generates a LineSeries given a type of series and an array of data points
		/// </summary>
		/// <param name="type">The type of SeriesCollection to get a LineSeries for</param>
		/// <param name="result">The data points to use for building the LineSeries</param>
		/// <returns>A LineSeries which can be added to a SeriesCollection</returns>
		static public GLineSeries LineSeriesForType(SeriesCollectionType type, DataPoint[] result, SeriesCollectionConfiguration configuration)
		{
			var Values = ValuesForType(type, result, configuration);

			return new GLineSeries
			{
				Values = Values,
				StrokeThickness = 1,
				PointGeometry = DefaultGeometries.None,
				Fill = Brushes.Transparent
			};
		}

		/// <summary>
		/// Generates the ObservablePoint objects which can be added to a LineSeries
		/// </summary>
		/// <param name="type">The type of SeriesCollection to calculate the points for</param>
		/// <param name="result">The data points to use for calculating the points</param>
		/// <param name="func">The function which turns a DataPoint into an ObservablePoint</param>
		/// <param name="length">The number of points to generate</param>
		/// <param name="stage1Length">The length of the first stage of the test</param>
		/// <param name="startOfStage2Index">The first index of the second stage of the test</param>
		/// <returns>An array of the ObservablePoints that were calculated from the parameters</returns>
		static private ObservablePoint[] GeneratePoints(SeriesCollectionType type, DataPoint[] result, Func<DataPoint, ObservablePoint> func, int stage1Length, int startOfStage2Index)
		{
			int Length = GetLengthForType(type, result, stage1Length, startOfStage2Index);
			ObservablePoint[] PointsToAdd = new ObservablePoint[Length];
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

		static public GearedValues<ObservablePoint> ValuesForType(SeriesCollectionType type, DataPoint[] result, SeriesCollectionConfiguration configuration)
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

			Func<DataPoint, ObservablePoint> PointGenerationFunc = GetPointGenerationFunc(type, result, configuration);
			ObservablePoint[] PointsToAdd = GeneratePoints(type, result, PointGenerationFunc, Stage1Length, StartOfStage2Index);


			GearedValues<ObservablePoint> Points = new GearedValues<ObservablePoint>();
			
				Points.AddRange(PointsToAdd);
				if (type == SeriesCollectionType.NormalStressShearStress)
				{
					Points.Quality = Quality.Low;
				}
				else
				{
					Points.Quality = Quality.Highest;
				}
				return Points;
		}

		/// <summary>
		/// Gets the function that turns a DataPoint into an ObservablePoint
		/// </summary>
		/// <param name="type">The type of SeriesCollection to get transformation functions for</param>
		/// <param name="result">The array of data points, used for calculating graphs of horizontal strain against g modulus</param>
		/// <returns>The function that transforms a DataPoint into an ObservablePoint given the parameters</returns>
		public static Func<DataPoint, ObservablePoint> GetPointGenerationFunc(SeriesCollectionType type, DataPoint[] result, SeriesCollectionConfiguration configuration)
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
						Y = -dataPoint.AxialStrain
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
					foreach (DataPoint point in result)
					{
						if (point.StageNumber == 2)
						{
							StrainStartShear = point.HorizontalStrain - configuration.GenericTestInformation.CorrectieWaardeB;
							StressStartShear = point.HorizontalStress;
							Found = true;
							break;
						}
					}

					if (!Found)
					{
						throw new ArgumentException("result doesn't contain data points in two stages");
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

		/// <summary>
		/// Gets the number of points in a SeriesCollection taken into account the given parameters.
		/// </summary>
		/// <param name="type">The type of SeriesCollection</param>
		/// <param name="result">The array of DataPoints for the SeriesCollection</param>
		/// <param name="Stage1Length">The number of data points in the first stage </param>
		/// <param name="StartOfStage2Index">The index of the first data point belonging to the second stage</param>
		/// <returns></returns>
		private static int GetLengthForType(SeriesCollectionType type, DataPoint[] result, int Stage1Length, int StartOfStage2Index)
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
