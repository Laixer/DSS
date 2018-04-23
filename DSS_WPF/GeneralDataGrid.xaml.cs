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

namespace DSS_WPF
{
	/// <summary>
	/// Interaction logic for GeneralDataGrid.xaml
	/// </summary>
	public partial class GeneralDataGrid : UserControl
	{
		private ShearViewModel _model;
		public ShearViewModel model
		{
			set
			{
				_model = value;
				UpdateItemsSources();
			}

			get
			{
				return _model;
			}
		}

		public GeneralDataGrid()
		{
			InitializeComponent();
			//UpdateItemsSource();
		
		}

		private void UpdateItemsSources()
		{
			UpdateEigenschappenMonsterGrid();

		}

		private void UpdateEigenschappenMonsterGrid()
		{
			SpecificTestInformation specific = model.SpecificTestInformation[0];
			GenericTestInformation generic = model.GenericTestInformation;

			List<GeneralDataEntry> items = new List<GeneralDataEntry>();
			items.Add(new GeneralDataEntry("Initiële hoogte:", generic.InitieleHoogte.ToString(), "mm"));
			items.Add(new GeneralDataEntry("Initiëel volumegewicht γ:", specific.InitieelVolumegewicht.ToString(), "kN/m3"));
			items.Add(new GeneralDataEntry("Droog volumegewicht γ:", specific.DroogVolumegewicht.ToString(), "kN/m3"));
			items.Add(new GeneralDataEntry("Watergehalte:", specific.WatergehalteVoor.ToString(), "%"));
			EigenschappenMonsterGrid.ItemsSource = items;
		}

		private void UpdateConsolidatieGrid()
		{
			SpecificTestInformation specific = model.SpecificTestInformation[0];
			GenericTestInformation generic = model.GenericTestInformation;

			int duration = 0;
			for (int i = 0; i < model.dataPoints.Length; i++)
			{
				if (model.dataPoints[i].stage_number == 2)
				{
					duration = (int)Math.Round((float)model.dataPoints[i - 1].time_since_start_stage / 3600);
				}
			}

			List<GeneralDataEntry> items = new List<GeneralDataEntry>();
			items.Add(new GeneralDataEntry("Δh:", generic.InitieleHoogte.ToString(), "mm"));
			items.Add(new GeneralDataEntry("h na consolidatie:", specific.InitieelVolumegewicht.ToString(), "kN/m3"));
			items.Add(new GeneralDataEntry("Normal stress:", specific.DroogVolumegewicht.ToString(), "kN/m3"));
			items.Add(new GeneralDataEntry("Duur:", duration.ToString(), "uur"));
			EigenschappenMonsterGrid.ItemsSource = items;
		}
	}
}
