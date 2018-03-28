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
		String FileName;

		public TestInformationWindow(String FileName)
		{
			this.FileName = FileName;
			InitializeComponent();
		}

		private void Continue_Button_Click(object sender, RoutedEventArgs e)
		{
			GenericTestInformation genericTestInformation = GenericInformationComponent.GetInformation();
			SpecificTestInformation specificTestInformation1 = SpecificInformationComponent1.GetInformation();
			SpecificTestInformation specificTestInformation2 = SpecificInformationComponent2.GetInformation();
			SpecificTestInformation specificTestInformation3 = SpecificInformationComponent3.GetInformation();
			Debug.Write(specificTestInformation1);

			ResultsWindow wind = new ResultsWindow(FileName);
			wind.Show();
		}

		private void Example_Data_Click(object sender, RoutedEventArgs e)
		{
			GenericTestInformationComponent component = GenericInformationComponent;
			component.ProjectField.Text = "DELFT 2018-007\n" + "Mvj17247 sond ji604";
			component.ProjectnummerField.Text = "2018-1";
			component.LaborantField.Text = "Bob van Amsterdam";
			component.AdviseurField.Text = "Bob van Amsterdam";
			component.TeamleiderField.Text = "Don Zandbergen";

			component.InitieleHoogteField.Text = "30";
			component.DiameterField.Text = "50";
			component.GrondsoortField.Text = "Klei";
			component.SoortMonsterField.Text = "Ongereoerd";
			component.CorrectieField.Text = "Ringen, membraan";
			component.CorrectieAField.Text = "0,037";
			component.CorrectieBField.Text = "1,28";

			SpecificTestInformationComponent[] informationComponents = new SpecificTestInformationComponent[] { SpecificInformationComponent1, SpecificInformationComponent2, SpecificInformationComponent3 };
			Random random = new Random();
			foreach (SpecificTestInformationComponent comp in informationComponents)
			{
				comp.BoringField.Text = "B0" + random.Next(1, 9);
				comp.MonsterField.Text = random.Next(1, 5).ToString();
				comp.BusField.Text = random.Next(1, 5).ToString();
				comp.DiepteMaaiveldField.Text = random.NextDouble().ToString();
				comp.MonsterdiepteMaaiveldField.Text = random.NextDouble().ToString();
				comp.MonsterDiepteNAPField.Text = random.NextDouble().ToString();
				comp.MonsterklasseField.Text = random.Next(1, 5).ToString();
				comp.DatumProefField.Text = random.Next(1, 28) + "/" + random.Next(1, 12) + "/" + "18";

				comp.InitieelVolumeGewichtField.Text = "dit is geen getal";
				comp.DroogVolumeGewichtField.Text = random.Next(1, 15).ToString();
				comp.WatergehalteVoorField.Text = random.Next(200, 600).ToString();
				comp.WatergehalteNaField.Text = random.Next(100, 200).ToString();

			}
		}
	}
}
