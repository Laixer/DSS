using System.Windows.Controls;
using System.Collections.Generic;

namespace DSS_WPF
{
	/// <summary>
	/// Interaction logic for ShearDataGrid.xaml
	/// </summary>
	public partial class ShearDataGrid : UserControl
	{
		private ShearViewModel _model;
		public ShearViewModel model
		{
			set
			{
				_model = value;
				UpdateItemsSource();
			}

			get
			{
				return _model;
			}
		}
		

		public ShearDataGrid()
		{
			InitializeComponent();

		}

		private void UpdateItemsSource()
		{
			float[] shearStrainValues = new float[] { 1.0f, 2.0f, 5.0f, 10.0f, 15.0f, 20.0f, 30.0f, 40.0f, 50.0f, 60.0f, 70.0f, 80.0f, 90.0f };
			List<DataPoint> itemsSource = new List<DataPoint>();
			

			float maxTau = 0;
			int maxTauIndex = -1;
			float maxStrain = 0;
			for (int i = 0; i < model.dataPoints.Length; i++)
			{
				float tau = model.dataPoints[i].horizontal_stress;
				if (tau > maxTau)
				{
					maxTau = tau;
					maxTauIndex = i;
				}

				float strain = model.dataPoints[i].horizontal_strain;
				if (strain > maxStrain)
				{
					maxStrain = strain;
				}
			}

			for (int i = 0; i < shearStrainValues.Length; i++)
			{
				DataPoint point = new DataPoint();
				point.horizontal_strain = shearStrainValues[i];
				point.normal_stress = model.SigmaNForShearStrainPercentage(shearStrainValues[i]);
				point.horizontal_stress = model.TauForShearStrainPercentage(shearStrainValues[i]);
				if (i == 0 || (i > 0 && itemsSource[i-1].horizontal_strain + 10 <= Utilities.RoundTo(maxStrain, -1)))
				{
					itemsSource.Add(point);
				} else
				{
					break;
				}
			}

			ShearStrainDataGrid.ItemsSource = itemsSource;

			MaxShearStrainDataGrid.ItemsSource = new DataPoint[] { model.dataPoints[maxTauIndex] };


		}
	}

	
}
