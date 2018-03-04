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

			SeriesCollection = SeriesCollectionManager.SeriesCollectionForType(SeriesCollectionType.ShearStrainHorizontalStress, result);

			DataContext = this;
		}

		public SeriesCollection SeriesCollection { get; set; }

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			Debug.WriteLine("finished");
			stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
		}

		private void CartesianChart_UpdaterTick(object sender)
		{
			Debug.WriteLine("finished");
			stopwatch.Stop();
			Console.WriteLine("rendering chart took " + stopwatch.ElapsedMilliseconds + " milliseconds");
		}
	}
}
