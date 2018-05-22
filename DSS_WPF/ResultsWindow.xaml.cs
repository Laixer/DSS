using System;
using System.Windows;
using System.Globalization;
using FileHelpers;
using LiveCharts;
using System.Windows.Controls;

namespace DSS_WPF
{
	/// <summary>
	/// Interaction logic for ResultsWindow.xaml
	/// </summary>
	public partial class ResultsWindow : Window
	{
		SpecificTestInformation[] SpecificTestInformation;
		GenericTestInformation GenericTestInformation;


		public ResultsWindow(String[] fileNames, GenericTestInformation testInformation, SpecificTestInformation[] specificTestInformation)
		{
			InitializeComponent();

			var engine = new FileHelperEngine<DataPoint>();
			var numberOfFiles = fileNames.Length;

			GenericTestInformation = testInformation;
			SpecificTestInformation = specificTestInformation;

			DataPoint[][] resultArrays = new DataPoint[numberOfFiles][];
			ShearViewModel[] models = new ShearViewModel[numberOfFiles];
			for (int i = 0; i < fileNames.Length; i++)
			{
				resultArrays[i] = engine.ReadFile(fileNames[i]);
				models[i] = new ShearViewModel(resultArrays[i], GenericTestInformation, SpecificTestInformation);
			}

			GenericTestInformation = testInformation;
			SpecificTestInformation = specificTestInformation;
			
			for (int i = 0; i < numberOfFiles; i++)
			{

				ResultScrollViewer resultScrollViewer = new ResultScrollViewer();
				resultScrollViewer.ShearDataGrid.Model = models[i];
				resultScrollViewer.GeneralDataGrid.Model = models[i];
				TabItem tabItem = new TabItem
				{
					Header = "Resultaten " + (i + 1)
				};

				SeriesCollectionConfiguration configuration1 = new SeriesCollectionConfiguration
				{
					Types = new SeriesCollectionType[] { SeriesCollectionType.ShearStrainHorizontalStress },
					DataPoints = resultArrays[i],
					HasLogarithmicX = false,
					HasLogarithmicY = false
				};

				SeriesCollectionConfiguration configuration2 = new SeriesCollectionConfiguration
				{
					Types = new SeriesCollectionType[] { SeriesCollectionType.NormalStressShearStress },
					DataPoints = resultArrays[i],
					HasLogarithmicX = false,
					HasLogarithmicY = false
				};

				SeriesCollectionConfiguration configuration3 = new SeriesCollectionConfiguration
				{
					Types = new SeriesCollectionType[] { SeriesCollectionType.TimeAxialStrain },
					DataPoints = resultArrays[i],
					HasLogarithmicX = true,
					HasLogarithmicY = false
				};

				SeriesCollectionConfiguration configuration4 = new SeriesCollectionConfiguration
				{
					Types = new SeriesCollectionType[] { SeriesCollectionType.ShearStrainNormalStress, SeriesCollectionType.ShearStrainPorePressure },
					DataPoints = resultArrays[i],
					HasLogarithmicX = false,
					HasLogarithmicY = false
				};

				SeriesCollectionConfiguration configuration5 = new SeriesCollectionConfiguration
				{
					Types = new SeriesCollectionType[] { SeriesCollectionType.HorizontalStrainSecantGModulus },
					DataPoints = resultArrays[i],
					HasLogarithmicX = true,
					HasLogarithmicY = true
				};

				resultScrollViewer.SeriesCollection1 = SeriesCollectionManager.SeriesCollectionForConfiguration(configuration1);
				resultScrollViewer.SeriesCollection2 = SeriesCollectionManager.SeriesCollectionForConfiguration(configuration2);
				resultScrollViewer.SeriesCollection3 = SeriesCollectionManager.SeriesCollectionForConfiguration(configuration3);
				resultScrollViewer.SeriesCollection4 = SeriesCollectionManager.SeriesCollectionForConfiguration(configuration4);
				resultScrollViewer.SeriesCollection5 = SeriesCollectionManager.SeriesCollectionForConfiguration(configuration5);

				tabItem.Content = resultScrollViewer;

				TabControl.Items.Add(tabItem);
			}

			Formatter = value => Math.Pow(10, value).ToString("N", CultureInfo.CreateSpecificCulture("nl"));
			Base = 10;

			DataContext = this;
		}
		
		public Func<double, string> Formatter { get; set; }
		public double Base { get; set; }
	}
}
