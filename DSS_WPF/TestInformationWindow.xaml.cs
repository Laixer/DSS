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
		String[] FileNames;
		SpecificTestInformationComponent[] InformationComponents = new SpecificTestInformationComponent[3];


		public TestInformationWindow(String[] FileNames)
		{
			this.FileNames = FileNames;
			InitializeComponent();
			
			for (int i = 0; i < FileNames.Length; i++)
			{
				SpecificTestInformationComponent comp = new SpecificTestInformationComponent
				{
					Name = "SpecificInformationComponent" + (i + 1)
				};
				comp.ProefstukTextBlock.Text = "Proefstuk " + (i + 1);

				InformationComponents[i] = comp;
				InformationStackPanel.Children.Add(comp);

			}
		}

		private void Continue_Button_Click(object sender, RoutedEventArgs e)
		{
			GenericTestInformation genericTestInformation = GenericInformationComponent.GetInformation();
			if (genericTestInformation == null)
			{
				return;
			}
			SpecificTestInformation[] specific = new SpecificTestInformation[3];
			int i = 0;
			foreach (SpecificTestInformationComponent component in InformationComponents)
			{
				if (component == null)
				{
					continue;
				}
				SpecificTestInformation info = component.GetInformation();
				if (info == null)
				{
					return;
				} else
				{
					specific[i] = info;
					i++;
				}
			}
			

			ResultsWindow wind = new ResultsWindow(FileNames[0], genericTestInformation, specific);
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

			Random random = new Random();
			foreach (SpecificTestInformationComponent comp in InformationComponents)
			{
				if (comp == null)
				{
					continue;
				}
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
