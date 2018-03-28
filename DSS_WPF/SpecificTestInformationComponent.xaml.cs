using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

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
			
			information.Boring = BoringField1.Text;
			try
			{
				information.Monster = Convert.ToInt32(MonsterField1.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Monster (" + MonsterField1.Text + ") is geen geheel getal");
				return null;
			}

			try
			{
				information.Bus = Convert.ToInt32(BusField1.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Bus (" + BusField1.Text + ") is geen geheel getal");
				return null;
			}

			try
			{
				information.DiepteMaaiveld = Convert.ToDouble(DiepteMaaiveldField1.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Diepte maaiveld (" + DiepteMaaiveldField1.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.MonsterDiepteMaaiveld = Convert.ToDouble(MonsterdiepteMaaiveldField1.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Monsterdiepte (t.o.v maaiveld) (" + MonsterdiepteMaaiveldField1.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.MonsterDiepteNAP = Convert.ToDouble(MonsterDiepteNAPField1.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Monsterdiepte (t.o.v maaiveld) (" + MonsterDiepteNAPField1.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.MonsterKlasse = Convert.ToInt32(MonsterklasseField1.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Monsterklasse (" + MonsterklasseField1.Text + ") is geen geheel getal");
				return null;
			}

			information.DatumProef = DatumProefField1.Text;

			try
			{
				information.InitieelVolumegewicht = Convert.ToDouble(InitieelVolumeGewichtField1.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Initieel volumegewicht (" + InitieelVolumeGewichtField1.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.DroogVolumegewicht = Convert.ToDouble(DroogVolumeGewichtField1.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Droog volumegewicht (" + DroogVolumeGewichtField1.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.WatergehalteVoor = Convert.ToInt32(WatergehalteVoorField1.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Watergehalte (voor) (" + WatergehalteVoorField1.Text + ") is geen geheel getal");
				return null;
			}

			try
			{
				information.WatergehalteNa = Convert.ToInt32(WatergehalteNaField1.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Watergehalte (na) (" + WatergehalteNaField1.Text + ") is geen geheel getal");
				return null;
			}

			return information;
		}
	}
}
