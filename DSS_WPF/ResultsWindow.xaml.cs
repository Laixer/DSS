using System;
using System.Windows;
using System.Globalization;
using FileHelpers;
using LiveCharts;
using System.Windows.Controls;

namespace Dss
{
	/// <summary>
	/// Interaction logic for ResultsWindow.xaml. This window shows a ResultScrollViewer
	/// in a tab control for every test that was picked by the user 
	/// in the file picker window.
	/// 
	/// </summary>
	public partial class ResultsWindow : Window
	{
		SpecificTestInformation[] SpecificTestInformation;
		GenericTestInformation GenericTestInformation;

		/// <summary>
		/// Creates a ResultWindow with a tab for every csv file the user selected
		/// in the file picker window.
		/// </summary>
		/// <param name="fileNames">An array of filenames, with a size between 1 and 3 inclusive. </param>
		/// <param name="testInformation">The generic test information object which is the same for every test</param>
		/// <param name="specificTestInformation">An array of specific test information objects, in the same order as the fileNames array</param>
		public ResultsWindow(String[] fileNames, GenericTestInformation testInformation, SpecificTestInformation[] specificTestInformation)
		{
			if (fileNames == null)
			{
				return;
			}

			InitializeComponent();

			var engine = new FileHelperEngine<DataPoint>();
			var numberOfFiles = fileNames.Length;

			GenericTestInformation = testInformation;
			SpecificTestInformation = specificTestInformation;

			DataPoint[][] resultArrays = new DataPoint[numberOfFiles][];
			ShearViewModel[] models = new ShearViewModel[numberOfFiles];
			// For every file, we build a result array, and a shear view model from that array.
			for (int i = 0; i < fileNames.Length; i++)
			{
				resultArrays[i] = engine.ReadFile(fileNames[i]);
				models[i] = new ShearViewModel(resultArrays[i], GenericTestInformation, SpecificTestInformation);
			}

			GenericTestInformation = testInformation;
			SpecificTestInformation = specificTestInformation;
			
			// For every file, we build a ScrollViewer to display its contents visually.
			// We add these ScrollViews to a TabControl so the user can easily switch
			// between the results of multiple tests.
			for (int i = 0; i < numberOfFiles; i++)
			{
				ResultScrollViewer resultScrollViewer = new ResultScrollViewer(i + 1);
				resultScrollViewer.ShearDataGrid.Model = models[i];
				resultScrollViewer.GeneralDataGrid.Model = models[i];
				TabItem tabItem = new TabItem
				{
					Header = "Resultaten " + (i + 1)
				};

				// Create SeriesCollectionConfiguration objects to pass the model objects 
				// to the SeriesCollection UI elements.

				SeriesCollectionConfiguration shearStrainHorizontalStress = new SeriesCollectionConfiguration
				{
					HasLogarithmicX = false,
					HasLogarithmicY = false
				};
				shearStrainHorizontalStress.SetTypes(new SeriesCollectionType[] { SeriesCollectionType.ShearStrainHorizontalStress });
				shearStrainHorizontalStress.SetDataPoints(resultArrays[i]);

				SeriesCollectionConfiguration normalStressShearStress = new SeriesCollectionConfiguration
				{
					HasLogarithmicX = false,
					HasLogarithmicY = false
				};
				normalStressShearStress.SetTypes(new SeriesCollectionType[] { SeriesCollectionType.NormalStressShearStress });
				normalStressShearStress.SetDataPoints(resultArrays[i]);

				SeriesCollectionConfiguration timeAxialStrain = new SeriesCollectionConfiguration
				{
					HasLogarithmicX = true,
					HasLogarithmicY = false
				};
				timeAxialStrain.SetTypes(new SeriesCollectionType[] { SeriesCollectionType.TimeAxialStrain });
				timeAxialStrain.SetDataPoints(resultArrays[i]);

				SeriesCollectionConfiguration shearStrainNormalStressAndShearStrainPorePressure = new SeriesCollectionConfiguration
				{
					HasLogarithmicX = false,
					HasLogarithmicY = false
				};
				shearStrainNormalStressAndShearStrainPorePressure.SetTypes(new SeriesCollectionType[] { SeriesCollectionType.ShearStrainNormalStress, SeriesCollectionType.ShearStrainPorePressure });
				shearStrainNormalStressAndShearStrainPorePressure.SetDataPoints(resultArrays[i]);

				SeriesCollectionConfiguration horizontalStrainSecantGModulus = new SeriesCollectionConfiguration
				{
					HasLogarithmicX = true,
					HasLogarithmicY = true
				};
				horizontalStrainSecantGModulus.SetTypes(new SeriesCollectionType[] { SeriesCollectionType.HorizontalStrainSecantGModulus });
				horizontalStrainSecantGModulus.SetDataPoints(resultArrays[i]);

				resultScrollViewer.ShearStrainHorizontalStress.AddRange(SeriesCollectionManager.SeriesCollectionForConfiguration(shearStrainHorizontalStress));
				resultScrollViewer.NormalStressShearStress.AddRange(SeriesCollectionManager.SeriesCollectionForConfiguration(normalStressShearStress));
				resultScrollViewer.TimeAxialStrain.AddRange(SeriesCollectionManager.SeriesCollectionForConfiguration(timeAxialStrain));
				resultScrollViewer.ShearStrainNormalStressAndShearStrainPorePressure.AddRange(SeriesCollectionManager.SeriesCollectionForConfiguration(shearStrainNormalStressAndShearStrainPorePressure));
				resultScrollViewer.HorizontalStrainSecantGModulus.AddRange(SeriesCollectionManager.SeriesCollectionForConfiguration(horizontalStrainSecantGModulus));

				tabItem.Content = resultScrollViewer;

				TabControl.Items.Add(tabItem);
			}

			// This formatter is used by the logarithmic graphs to create axes.
			Formatter = value => Math.Pow(10, value).ToString("N", CultureInfo.CreateSpecificCulture("nl"));
			Base = 10;

			DataContext = this;
		}
		
		/// <summary>
		/// Used by logarithmic graphs to create axes.
		/// </summary>
		public Func<double, string> Formatter { get; set; }
		/// <summary>
		/// The base for the logarithmic axes, currently always 10.
		/// </summary>
		public double Base { get; set; }
	}
}
