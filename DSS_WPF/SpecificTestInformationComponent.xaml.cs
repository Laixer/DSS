using System;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Dss
{
	public partial class SpecificTestInformationComponent: Grid
	{
		public SpecificTestInformationComponent()
		{
			InitializeComponent();
		}

		[SuppressMessage("Microsoft.Design", "CA1024:ChangeToProperty")]
		public SpecificTestInformation GetInformation()
		{
			SpecificTestInformation information = new SpecificTestInformation
			{
				Boring = BoringField.Text
			};
			try
			{
				information.Monster = Convert.ToInt32(MonsterField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde Monster (" + MonsterField.Text + ") is geen geheel getal");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde Monster (" + MonsterField.Text + ") is geen geheel getal");
				return null;
			}

			try
			{
				information.Bus = Convert.ToInt32(BusField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde Bus (" + BusField.Text + ") is geen geheel getal");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde Bus (" + BusField.Text + ") is geen geheel getal");
				return null;
			}

			try
			{
				information.DiepteMaaiveld = Convert.ToDouble(DiepteMaaiveldField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde Diepte Maaiveld (" + DiepteMaaiveldField.Text + ") is geen geheel getal");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde Diepte Maaiveld (" + DiepteMaaiveldField.Text + ") is geen geheel getal");
				return null;
			}
			

			try
			{
				information.MonsterDiepteMaaiveld = Convert.ToDouble(MonsterdiepteMaaiveldField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde Monsterdiepte (t.o.v maaiveld) (" + MonsterdiepteMaaiveldField.Text + ") is geen decimaal getal");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde Monsterdiepte (t.o.v maaiveld) (" + MonsterdiepteMaaiveldField.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.MonsterDiepteNap = Convert.ToDouble(MonsterDiepteNAPField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde Monsterdiepte (t.o.v maaiveld) (" + MonsterdiepteMaaiveldField.Text + ") is geen decimaal getal");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde Monsterdiepte (t.o.v maaiveld) (" + MonsterdiepteMaaiveldField.Text + ") is geen decimaal getal");
				return null;
			}


			try
			{
				information.MonsterKlasse = Convert.ToInt32(MonsterklasseField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde Monsterklasse (" + MonsterklasseField.Text + ") is geen geheel getal");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde Monsterklasse (" + MonsterklasseField.Text + ") is geen geheel getal");
				return null;
			}

			information.DatumProef = DatumProefField.Text;

			try
			{
				information.InitieelVolumegewicht = Convert.ToDouble(InitieelVolumeGewichtField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde Initieel volumegewicht (" + InitieelVolumeGewichtField.Text + ") is geen decimaal getal");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde Initieel volumegewicht (" + InitieelVolumeGewichtField.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.DroogVolumegewicht = Convert.ToDouble(DroogVolumeGewichtField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde Droog volumegewicht (" + DroogVolumeGewichtField.Text + ") is geen decimaal getal");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde Droog volumegewicht (" + DroogVolumeGewichtField.Text + ") is geen decimaal getal");
				return null;
			}

			try
			{
				information.WatergehalteVoor = Convert.ToInt32(WatergehalteVoorField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde Watergehalte (voor) (" + WatergehalteVoorField.Text + ") is geen geheel getal");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde Watergehalte (voor) (" + WatergehalteVoorField.Text + ") is geen geheel getal");
				return null;
			}

			try
			{
				information.WatergehalteNa = Convert.ToInt32(WatergehalteNaField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde Watergehalte (na) (" + WatergehalteNaField.Text + ") is geen geheel getal");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde Watergehalte (na) (" + WatergehalteNaField.Text + ") is geen geheel getal");
				return null;
			}

			return information;
		}
	}
}
