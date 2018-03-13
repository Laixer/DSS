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
using System.Globalization;
using FileHelpers;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace DSS_WPF
{
	/// <summary>
	/// Interaction logic for ResultsWindow.xaml
	/// </summary>
	public partial class ResultsWindow : Window
	{

		Stopwatch stopwatch;

		public ResultsWindow(string fileName)
		{
			InitializeComponent();

			var engine = new FileHelperEngine<DataPoint>();

			// To Read Use:
			//Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
			var result = engine.ReadFile(fileName);
			//stopwatch.Stop();
			//Console.WriteLine("reading and parsing csv took " + stopwatch.ElapsedMilliseconds + " milliseconds");

			SeriesCollection1 = SeriesCollectionManager.SeriesCollectionForType(SeriesCollectionType.ShearStrainHorizontalStress, result, false);
			SeriesCollection2 = SeriesCollectionManager.SeriesCollectionForType(SeriesCollectionType.NormalStressShearStress, result, false);
			SeriesCollection3 = SeriesCollectionManager.SeriesCollectionForType(SeriesCollectionType.TimeAxialStrain, result, true);
			Formatter = value => Math.Pow(10, value).ToString("N", CultureInfo.CreateSpecificCulture("nl"));
			Base = 10;


			DataContext = this;
		}

		public SeriesCollection SeriesCollection1 { get; set; }
		public SeriesCollection SeriesCollection2 { get; set; }
		public SeriesCollection SeriesCollection3 { get; set; }
		public Func<double, string> Formatter { get; set; }
		public double Base { get; set; }

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			//Debug.WriteLine("finished");
			//stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
		}

		private void CartesianChart_UpdaterTick(object sender)
		{
			//Debug.WriteLine("finished");
			//stopwatch.Stop();
			//Console.WriteLine("rendering chart took " + stopwatch.ElapsedMilliseconds + " milliseconds");
		}
	}
}
