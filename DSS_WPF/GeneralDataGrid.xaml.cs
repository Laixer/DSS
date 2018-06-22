using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;

namespace Dss
{
	// I chose to use Dutch words in this file because it makes later maintenance of
	// this class easier since the translation between my code and
	// the Excel formulas is clearer. However, it makes for strange names sometimes.

	/// <summary>
	/// Interaction logic for GeneralDataGrid.xaml.
	/// This class represents a collection of DataGrid objects that shows general information
	/// about the test.
	/// </summary>
	public partial class GeneralDataGrid : UserControl
	{
		private ShearViewModel _model;
		/// <summary>
		/// The shear view model associated with the data grid.
		/// This model is the data source for the data grid.
		/// </summary>
		public ShearViewModel Model
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
			UpdateGemeenteGrid();
			UpdateProjectGrid();
		}

		private void UpdateEigenschappenMonsterGrid()
		{
			SpecificTestInformation specific = Model.GetSpecificTestInformation()[0];
			GenericTestInformation generic = Model.GenericTestInformation;

			List<GeneralDataEntry> items = new List<GeneralDataEntry>
			{
				new GeneralDataEntry("Initiële hoogte:", generic.InitieleHoogte.ToString(CultureInfo.CurrentCulture), "mm"),
				new GeneralDataEntry("Initiëel volumegewicht γ:", specific.InitieelVolumegewicht.ToString(CultureInfo.CurrentCulture), "kN/m3"),
				new GeneralDataEntry("Droog volumegewicht γ:", specific.DroogVolumegewicht.ToString(CultureInfo.CurrentCulture), "kN/m3"),
				new GeneralDataEntry("Watergehalte:", specific.WatergehalteVoor.ToString(CultureInfo.CurrentCulture), "%")
			};
			EigenschappenMonsterGrid.ItemsSource = items;
		}

		private void UpdateConsolidatieGrid()
		{
			GenericTestInformation generic = Model.GenericTestInformation;

			int duration = 0;
			float deltaH = 0;
			float normal_stress = 0;

			// This logic is a translation of the formula for this grid in the
			/// excel sheet.
			for (int i = 0; i < Model.GetDataPoints().Length; i++)
			{
				if (Model.GetDataPoints()[i].StageNumber == 2)
				{
					duration = (int)Math.Round((float)Model.GetDataPoints()[i - 1].TimeSinceStartStage / 3600);
					normal_stress = (int)Math.Round((float)Model.GetDataPoints()[i].NormalStress);
				}
				if (Model.GetDataPoints()[i].AxialDisplacement > deltaH)
				{
					deltaH = Model.GetDataPoints()[i].AxialDisplacement;
				}
			}

			List<GeneralDataEntry> items = new List<GeneralDataEntry>
			{
				new GeneralDataEntry("Δh:", deltaH.ToString(CultureInfo.CurrentCulture), "mm"),
				new GeneralDataEntry("h na consolidatie:", (generic.InitieleHoogte - deltaH).ToString(CultureInfo.CurrentCulture), "mm"),
				new GeneralDataEntry("Normal stress:", Utilities.RoundTo(normal_stress, 1).ToString(CultureInfo.CurrentCulture), "kPa"),
				new GeneralDataEntry("Duur:", duration.ToString(CultureInfo.CurrentCulture), "uur")
			};
			ConsolidatieGrid.ItemsSource = items;
		}

		private void UpdateAfschuifGrid()
		{
			float max_horizontal_strain = 0;
			float max_stage_time = 0;
			float max_horizontal_stress = 0;
			int max_tau_index = 0;

			// This logic is a translation of the formula for this grid in the
			/// excel sheet.
			for (int i = 0; i < Model.GetDataPoints().Length; i++)
			{				
				if (Model.GetDataPoints()[i].HorizontalStrain > max_horizontal_strain)
				{
					max_horizontal_strain = Model.GetDataPoints()[i].HorizontalStrain;
				}
				if (Model.GetDataPoints()[i].StageNumber == 2 && Model.GetDataPoints()[i].TimeSinceStartStage > max_stage_time)
				{
					max_stage_time = Model.GetDataPoints()[i].TimeSinceStartStage;
				}
				if (Model.GetDataPoints()[i].HorizontalStress > max_horizontal_stress)
				{
					max_horizontal_stress = Model.GetDataPoints()[i].HorizontalStress;
					max_tau_index = i;
				}
			}

			float afschuifsnelheid = max_horizontal_strain / max_stage_time * 3600;
			float max_tau = Model.GetDataPoints()[max_tau_index].HorizontalStrain;
			float max_shear = CalculateMaxShear();
			float best_error = float.MaxValue;
			float target = max_shear / 2;
			float horizontal_strain_at_target = 0;
			for (int i = 0; i < Model.GetDataPoints().Length; i++)
			{
				float current_error = Math.Abs(target - Model.GetDataPoints()[i].HorizontalStress);
				if (current_error < best_error)
				{
					best_error = current_error;
					horizontal_strain_at_target = Model.GetDataPoints()[i].HorizontalStrain;
				}
			}
			float g50 = max_shear / 2 / horizontal_strain_at_target / 10;

			List<GeneralDataEntry> items = new List<GeneralDataEntry>
			{
				new GeneralDataEntry("Afschuifsnelheid:", Utilities.RoundTo(afschuifsnelheid, 1).ToString(CultureInfo.CurrentCulture), "%/h"),
				new GeneralDataEntry("Max. shear stress:", Utilities.RoundTo(max_horizontal_stress, 1).ToString(CultureInfo.CurrentCulture), "kPa"),
				new GeneralDataEntry("Shear strain bij max.", Utilities.RoundTo(max_tau, 1).ToString(CultureInfo.CurrentCulture), "%"),
				// These unicode symbols are the superscript versions of 5 and 0 respectively
				new GeneralDataEntry("G" + '\u2085' + '\u2080' + ":", Utilities.RoundTo(g50, 3).ToString(CultureInfo.CurrentCulture), "Mpa")
			};
			AfschuifGrid.ItemsSource = items;
		}

		/// <summary>
		/// Helper method for calculating the maximal shear value
		/// for this model: its calculation is a translation
		/// of the formu.as in the Excel sheet.
		/// </summary>
		/// <returns></returns>
		private float CalculateMaxShear()
		{
			float max_horizontal_stress = 0;
			float horizontal_strain = 0;
			for (int i = 0; i < Model.GetDataPoints().Length; i++)
			{
				if (Model.GetDataPoints()[i].HorizontalStress > max_horizontal_stress)
				{
					max_horizontal_stress = Model.GetDataPoints()[i].HorizontalStress;
					horizontal_strain = Model.GetDataPoints()[i].HorizontalStrain;
				}
			}

			double a = Model.GenericTestInformation.CorrectieWaardeA;
			double b = Model.GenericTestInformation.CorrectieWaardeB;
			double max_shear = max_horizontal_stress - b - a * horizontal_strain;

			return (float)max_shear;
		}

		private void UpdateNaBeproevingGrid()
		{
			List<GeneralDataEntry> items = new List<GeneralDataEntry>
			{
				new GeneralDataEntry("Watergehalte W:", Model.GetSpecificTestInformation()[0].WatergehalteNa.ToString(CultureInfo.CurrentCulture), "%")
			};
			NaBeproevingGrid.ItemsSource = items;
		}

		private void UpdatePersonnelGrid()
		{
			GenericTestInformation generic = Model.GenericTestInformation;

			List<GeneralDataEntry> items = new List<GeneralDataEntry>
			{
				new GeneralDataEntry("Laborant:", generic.Laborant, ""),
				new GeneralDataEntry("Adviseur:", generic.Adviseur, ""),
				new GeneralDataEntry("Teamleider:", generic.Teamleider, "")
			};
			PersonnelGrid.ItemsSource = items;
		}

		private void UpdateBoringGrid()
		{
			GenericTestInformation generic = Model.GenericTestInformation;
			SpecificTestInformation specific = Model.GetSpecificTestInformation()[0];

			List<GeneralDataEntry> items = new List<GeneralDataEntry>
			{
				new GeneralDataEntry("Boring:", specific.Boring, ""),
				new GeneralDataEntry("Monsterdiepte:", "MV " + specific.MonsterDiepteMaaiveld + " m", ""),
				new GeneralDataEntry("", "MV NAP " + specific.MonsterDiepteNap + " m", ""),
				new GeneralDataEntry("", "NAP " + specific.DiepteMaaiveld + " m", ""),
				new GeneralDataEntry("Grondsoort:", generic.GrondSoort, ""),
				new GeneralDataEntry("Monsterklasse:", specific.MonsterKlasse.ToString(CultureInfo.CurrentCulture), ""),
				new GeneralDataEntry("Datum proef:", specific.DatumProef, "")
			};
			BoringGrid.ItemsSource = items;
		}

		private void UpdateGemeenteGrid()
		{
			List<GeneralDataEntry> items = new List<GeneralDataEntry>
			{
				new GeneralDataEntry("Heeeee", "Gemeente Rotterdam", "Swag"),
				new GeneralDataEntry("", "Ingenieursbureau", ""),
				new GeneralDataEntry("", "Veldmeetdienst, Laboratorium en Geomonitoring", "")
			};
			GemeenteGrid.ItemsSource = items;
		}

		private void UpdateProjectGrid()
		{
			GenericTestInformation generic = Model.GenericTestInformation;
			SpecificTestInformation specific = Model.GetSpecificTestInformation()[0];

			List<GeneralDataEntry> items = new List<GeneralDataEntry>
			{
				new GeneralDataEntry("Project:", generic.Project, ""),
				new GeneralDataEntry("", specific.Boring, ""),
				new GeneralDataEntry("", generic.Projectnummer, ""),
				new GeneralDataEntry("", "DIRECT SIMPLE SHEAR TEST", "")
			};

			ProjectGrid.ItemsSource = items;
		}
	}
}
