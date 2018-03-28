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
using System.Windows.Shapes;
using System.Diagnostics;

namespace DSS_WPF
{
	/// <summary>
	/// Interaction logic for TestInformationWindow.xaml
	/// </summary>
	public partial class TestInformationWindow : Window
	{
		public TestInformationWindow()
		{
			InitializeComponent();
		}

		private void Continue_Button_Click(object sender, RoutedEventArgs e)
		{
			GenericTestInformation genericTestInformation = GenerateGenericTestInformation();
			SpecificTestInformation specificTestInformation1 = GenerateSpecificTestInformation1();
			Debug.Write(specificTestInformation1);
		}

		private GenericTestInformation GenerateGenericTestInformation()
		{
			GenericTestInformation information = new GenericTestInformation();
			
			information.Project = GenericInformationComponent.ProjectField.Text;
			information.Projectnummer = GenericInformationComponent.ProjectnummerField.Text;
			information.Laborant = GenericInformationComponent.LaborantField.Text;
			information.Adviseur = GenericInformationComponent.AdviseurField.Text;
			information.Teamleider = GenericInformationComponent.TeamleiderField.Text;
			try
			{
				information.InitieleHoogte = Convert.ToDouble(GenericInformationComponent.InitieleHoogteField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Initiele hoogte (" + GenericInformationComponent.InitieleHoogteField.Text + ") is geen decimale waarde");
				return null;
			}

			try
			{
				information.Diameter = Convert.ToDouble(GenericInformationComponent.DiameterField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde diameter (" + GenericInformationComponent.DiameterField.Text + ") is geen decimale waarde");
				return null;
			}

			information.GrondSoort = GenericInformationComponent.GrondsoortField.Text;
			information.SoortMonster = GenericInformationComponent.SoortMonsterField.Text;
			information.Correctie = GenericInformationComponent.CorrectieField.Text;

			try
			{
				information.CorrectieWaardeA = Convert.ToDouble(GenericInformationComponent.CorrectieAField.Text);
			}
			catch
			{
				MessageBox.Show("Correctiewaarde a (" + GenericInformationComponent.CorrectieAField.Text + ") is geen decimale waarde");
				return null;
			}

			try
			{
				information.CorrectieWaardeB = Convert.ToDouble(GenericInformationComponent.CorrectieBField.Text);
			}
			catch
			{
				MessageBox.Show("Correctiewaarde b (" + GenericInformationComponent.CorrectieBField.Text + ") is geen decimale waarde");
				return null;
			}

			return information;
		}

		private SpecificTestInformation GenerateSpecificTestInformation1()
		{
			SpecificTestInformation information = new SpecificTestInformation();
			/*
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
			}*/

			return information;
		}
	}
}
