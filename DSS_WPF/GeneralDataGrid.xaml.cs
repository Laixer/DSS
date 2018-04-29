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
		}

		private void UpdateItemsSources()
		{
			UpdateEigenschappenMonsterGrid();
			UpdateConsolidatieGrid();
			UpdateAfschuifGrid();
			UpdateNaBeproevingGrid();

			UpdatePersonnelGrid();
			UpdateBoringGrid();
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
			float deltaH = 0;
			float normal_stress = 0;
			for (int i = 0; i < model.dataPoints.Length; i++)
			{
				if (model.dataPoints[i].stage_number == 2)
				{
					duration = (int)Math.Round((float)model.dataPoints[i - 1].time_since_start_stage / 3600);
					normal_stress = (int)Math.Round((float)model.dataPoints[i].normal_stress);
				}
				if (model.dataPoints[i].axial_displacement > deltaH)
				{
					deltaH = model.dataPoints[i].axial_displacement;
				}
			}

			List<GeneralDataEntry> items = new List<GeneralDataEntry>();
			items.Add(new GeneralDataEntry("Δh:", deltaH.ToString(), "mm"));
			items.Add(new GeneralDataEntry("h na consolidatie:", (generic.InitieleHoogte - deltaH).ToString(), "mm"));
			items.Add(new GeneralDataEntry("Normal stress:", Utilities.RoundTo(normal_stress, 1).ToString(), "kPa"));
			items.Add(new GeneralDataEntry("Duur:", duration.ToString(), "uur"));
			ConsolidatieGrid.ItemsSource = items;
		}

		private void UpdateAfschuifGrid()
		{
			SpecificTestInformation specific = model.SpecificTestInformation[0];
			GenericTestInformation generic = model.GenericTestInformation;

			float max_horizontal_strain = 0;
			float max_stage_time = 0;
			float max_horizontal_stress = 0;
			int max_tau_index = 0;

			for (int i = 0; i < model.dataPoints.Length; i++)
			{				
				if (model.dataPoints[i].horizontal_strain > max_horizontal_strain)
				{
					max_horizontal_strain = model.dataPoints[i].horizontal_strain;
				}
				if (model.dataPoints[i].stage_number == 2 && model.dataPoints[i].time_since_start_stage > max_stage_time)
				{
					max_stage_time = model.dataPoints[i].time_since_start_stage;
				}
				if (model.dataPoints[i].horizontal_stress > max_horizontal_stress)
				{
					max_horizontal_stress = model.dataPoints[i].horizontal_stress;
					max_tau_index = i;
				}
			}

			float afschuifsnelheid = max_horizontal_strain / max_stage_time * 3600;
			float max_tau = model.dataPoints[max_tau_index].horizontal_strain;
			float max_shear = calculateMaxShear();
			float best_error = float.MaxValue;
			float target = max_shear / 2;
			float horizontal_strain_at_target = 0;
			for (int i = 0; i < model.dataPoints.Length; i++)
			{
				float current_error = Math.Abs(target - model.dataPoints[i].horizontal_stress);
				if (current_error < best_error)
				{
					best_error = current_error;
					horizontal_strain_at_target = model.dataPoints[i].horizontal_strain;
				}
			}
			float g50 = max_shear / 2 / horizontal_strain_at_target / 10;

			List<GeneralDataEntry> items = new List<GeneralDataEntry>();
			items.Add(new GeneralDataEntry("Afschuifsnelheid:", Utilities.RoundTo(afschuifsnelheid, 1).ToString(), "%/h"));
			items.Add(new GeneralDataEntry("Max. shear stress:", Utilities.RoundTo(max_horizontal_stress, 1).ToString(), "kPa"));
			items.Add(new GeneralDataEntry("Shear strain bij max.", Utilities.RoundTo(max_tau, 1).ToString(), "%"));
			items.Add(new GeneralDataEntry("G" + '\u2085' + '\u2080' + ":", Utilities.RoundTo(g50, 3).ToString(), "Mpa"));
			AfschuifGrid.ItemsSource = items;
		}

		private float calculateMaxShear()
		{
			float max_horizontal_stress = 0;
			float horizontal_strain = 0;
			for (int i = 0; i < model.dataPoints.Length; i++)
			{
				if (model.dataPoints[i].horizontal_stress > max_horizontal_stress)
				{
					max_horizontal_stress = model.dataPoints[i].horizontal_stress;
					horizontal_strain = model.dataPoints[i].horizontal_strain;
				}
			}

			double a = model.GenericTestInformation.CorrectieWaardeA;
			double b = model.GenericTestInformation.CorrectieWaardeB;
			double max_shear = max_horizontal_stress - b - a * horizontal_strain;

			return (float)max_shear;
		}

		private void UpdateNaBeproevingGrid()
		{
			List<GeneralDataEntry> items = new List<GeneralDataEntry>();
			items.Add(new GeneralDataEntry("Watergehalte W:", model.SpecificTestInformation[0].WatergehalteNa.ToString(), "%"));
			NaBeproevingGrid.ItemsSource = items;
		}

		private void UpdatePersonnelGrid()
		{
			GenericTestInformation generic = model.GenericTestInformation;

			List<GeneralDataEntry> items = new List<GeneralDataEntry>();
			items.Add(new GeneralDataEntry("Laborant:", generic.Laborant, ""));
			items.Add(new GeneralDataEntry("Adviseur:", generic.Adviseur, ""));
			items.Add(new GeneralDataEntry("Teamleider:", generic.Teamleider, ""));
			PersonnelGrid.ItemsSource = items;
		}

		private void UpdateBoringGrid()
		{
			GenericTestInformation generic = model.GenericTestInformation;
			SpecificTestInformation specific = model.SpecificTestInformation[0];

			List<GeneralDataEntry> items = new List<GeneralDataEntry>();
			items.Add(new GeneralDataEntry("Boring:", specific.Boring, ""));
			items.Add(new GeneralDataEntry("Monsterdiepte:", "MV " + specific.MonsterDiepteMaaiveld + " m", ""));
			items.Add(new GeneralDataEntry("Grondsoort:", generic.GrondSoort, ""));
			items.Add(new GeneralDataEntry("Monsterklasse:", specific.MonsterKlasse.ToString(), ""));
			items.Add(new GeneralDataEntry("Datum proef:", specific.DatumProef, ""));
			PersonnelGrid.ItemsSource = items;
		}
	}
}
