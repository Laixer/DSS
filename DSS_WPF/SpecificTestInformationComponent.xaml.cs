using System;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;

namespace DSS_WPF
{
	public partial class SpecificTestInformationComponent: Grid
	{
		public SpecificTestInformationComponent()
		{
			InitializeComponent();
		}

		public SpecificTestInformation GetInformation()
		{
			SpecificTestInformation information = new SpecificTestInformation();
			
			information.Boring = BoringField.Text;
			try
			{
				information.Monster = Convert.ToInt32(MonsterField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Monster (" + MonsterField.Text + ") is geen geheel getal");
				return null;
			}

			try
			{
				information.Bus = Convert.ToInt32(BusField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Bus (" + BusField.Text + ") is geen geheel getal");
				return null;
			}

			try
			{
				information.DiepteMaaiveld = Convert.ToDouble(DiepteMaaiveldField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Diepte maaiveld (" + DiepteMaaiveldField.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.MonsterDiepteMaaiveld = Convert.ToDouble(MonsterdiepteMaaiveldField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Monsterdiepte (t.o.v maaiveld) (" + MonsterdiepteMaaiveldField.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.MonsterDiepteNAP = Convert.ToDouble(MonsterDiepteNAPField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Monsterdiepte (t.o.v maaiveld) (" + MonsterDiepteNAPField.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.MonsterKlasse = Convert.ToInt32(MonsterklasseField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Monsterklasse (" + MonsterklasseField.Text + ") is geen geheel getal");
				return null;
			}

			information.DatumProef = DatumProefField.Text;

			try
			{
				information.InitieelVolumegewicht = Convert.ToDouble(InitieelVolumeGewichtField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Initieel volumegewicht (" + InitieelVolumeGewichtField.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.DroogVolumegewicht = Convert.ToDouble(DroogVolumeGewichtField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Droog volumegewicht (" + DroogVolumeGewichtField.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.WatergehalteVoor = Convert.ToInt32(WatergehalteVoorField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Watergehalte (voor) (" + WatergehalteVoorField.Text + ") is geen geheel getal");
				return null;
			}

			try
			{
				information.WatergehalteNa = Convert.ToInt32(WatergehalteNaField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Watergehalte (na) (" + WatergehalteNaField.Text + ") is geen geheel getal");
				return null;
			}

			return information;
		}
	}
}
