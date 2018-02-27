using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using FileHelpers;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace DSS_WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			var engine = new FileHelperEngine<DataPoint>();

			// To Read Use:
			Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
			var result = engine.ReadFile("\\\\Mac\\Home\\Desktop\\proef_1.csv");
			stopwatch.Stop();
			Console.WriteLine("reading and parsing csv took " + stopwatch.ElapsedMilliseconds + " milliseconds");

			int length = result.Length;

			ChartValues<ObservablePoint> points = new ChartValues<ObservablePoint>();

			ObservablePoint[] pointsToAdd = new ObservablePoint[length];
			for (int j = 0; j < length; j++)
			{
				pointsToAdd[j] = (new ObservablePoint
				{
					X = result[j].horizontal_strain,
					Y = result[j].horizontal_stress
				});
				Debug.WriteLine(j + " added");
			
			}
			points.AddRange(pointsToAdd);

			SeriesCollection = new SeriesCollection
			{
				new LineSeries
				{
					Values = points,
					StrokeThickness = 1,
					PointGeometrySize = 1
				},
			};

			

			DataContext = this;
		}

		public SeriesCollection SeriesCollection { get; set; }
	}
}
