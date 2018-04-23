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

		SpecificTestInformation[] SpecificTestInformation;
		GenericTestInformation GenericTestInformation;


		public ResultsWindow(String fileName, GenericTestInformation testInformation, SpecificTestInformation[] specificTestInformation)
		{
			InitializeComponent();

			var engine = new FileHelperEngine<DataPoint>();

			// To Read Use:
			//Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
			var result = engine.ReadFile(fileName);

			GenericTestInformation = testInformation;
			SpecificTestInformation = specificTestInformation;
			ShearViewModel model = new ShearViewModel(result, GenericTestInformation, SpecificTestInformation);
			ShearDataGrid.model = model;
			GeneralDataGrid.model = model;

			//stopwatch.Stop();
			//Console.WriteLine("reading and parsing csv took " + stopwatch.ElapsedMilliseconds + " milliseconds");
			SeriesCollectionConfiguration configuration1 = new SeriesCollectionConfiguration
			{
				Types = new SeriesCollectionType[] { SeriesCollectionType.ShearStrainHorizontalStress },
				DataPoints = result,
				HasLogarithmicX = false,
				HasLogarithmicY = false
			};

			SeriesCollectionConfiguration configuration2 = new SeriesCollectionConfiguration
			{
				Types = new SeriesCollectionType[] { SeriesCollectionType.NormalStressShearStress },
				DataPoints = result,
				HasLogarithmicX = false,
				HasLogarithmicY = false
			};

			SeriesCollectionConfiguration configuration3 = new SeriesCollectionConfiguration
			{
				Types = new SeriesCollectionType[] { SeriesCollectionType.TimeAxialStrain },
				DataPoints = result,
				HasLogarithmicX = true,
				HasLogarithmicY = false
			};

			SeriesCollectionConfiguration configuration4 = new SeriesCollectionConfiguration
			{
				Types = new SeriesCollectionType[] { SeriesCollectionType.ShearStrainNormalStress, SeriesCollectionType.ShearStrainPorePressure },
				DataPoints = result,
				HasLogarithmicX = false,
				HasLogarithmicY = false
			};

			SeriesCollectionConfiguration configuration5 = new SeriesCollectionConfiguration
			{
				Types = new SeriesCollectionType[] { SeriesCollectionType.HorizontalStrainSecantGModulus },
				DataPoints = result,
				HasLogarithmicX = true,
				HasLogarithmicY = true
			};

			SeriesCollection1 = SeriesCollectionManager.SeriesCollectionForConfiguration(configuration1);
			SeriesCollection2 = SeriesCollectionManager.SeriesCollectionForConfiguration(configuration2);
			SeriesCollection3 = SeriesCollectionManager.SeriesCollectionForConfiguration(configuration3);
			SeriesCollection4 = SeriesCollectionManager.SeriesCollectionForConfiguration(configuration4);
			SeriesCollection5 = SeriesCollectionManager.SeriesCollectionForConfiguration(configuration5);


			Formatter = value => Math.Pow(10, value).ToString("N", CultureInfo.CreateSpecificCulture("nl"));
			Base = 10;

			DataContext = this;
		}

		public SeriesCollection SeriesCollection1 { get; set; }
		public SeriesCollection SeriesCollection2 { get; set; }
		public SeriesCollection SeriesCollection3 { get; set; }
		public SeriesCollection SeriesCollection4 { get; set; }
		public SeriesCollection SeriesCollection5 { get; set; }
		public Func<double, string> Formatter { get; set; }
		public double Base { get; set; }
	}
}
