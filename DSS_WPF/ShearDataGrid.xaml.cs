using System.Windows.Controls;

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
				updateItemsSource();
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

		private void updateItemsSource()
		{
			float[] shearStrainValues = new float[] { 1.0f, 2.0f, 5.0f, 10.0f, 15.0f, 20.0f, 30.0f, 40.0f, 50.0f, 60.0f };
			DataPoint[] itemsSource = new DataPoint[10];
			float maxTau = 0;
			int maxTauIndex = -1;
			
			for (int i = 0; i < itemsSource.Length; i++)
			{
				DataPoint point = new DataPoint();
				point.horizontal_strain = shearStrainValues[i];
				point.normal_stress = model.SigmaNForShearStrainPercentage(shearStrainValues[i]);
				float tau = model.TauForShearStrainPercentage(shearStrainValues[i]);
				point.horizontal_stress = tau;
				if (tau >= maxTau)
				{
					maxTau = tau;
					maxTauIndex = i;
				}
				itemsSource[i] = point;
			}

			ShearStrainDataGrid.ItemsSource = itemsSource;

			MaxShearStrainDataGrid.ItemsSource = new DataPoint[] { itemsSource[maxTauIndex] };


		}
	}
}
