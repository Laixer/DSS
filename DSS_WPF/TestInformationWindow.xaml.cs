using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;

namespace Dss
{
	/// <summary>
	/// Interaction logic for TestInformationWindow.xaml
	/// </summary>
	public partial class TestInformationWindow : Window
	{
		String[] FileNames;
		SpecificTestInformationComponent[] InformationComponents = new SpecificTestInformationComponent[3];


		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Documents.Run.set_Text(System.String)", Justification = "This program has not been localized at this time")]
		public TestInformationWindow(String[] fileNames)
		{
			if (fileNames == null)
			{
				return;
			}
			this.FileNames = fileNames;
			InitializeComponent();
			
			for (int i = 0; i < fileNames.Length; i++)
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
			

			ResultsWindow wind = new ResultsWindow(FileNames, genericTestInformation, specific);
			wind.Show();
		}

		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "sond")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Zandbergen")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:RetrieveLiteralsFromResourceTable")]
		[SuppressMessage("Microsoft.Naming", "CA2204:SpellingErrors")]
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
				comp.MonsterField.Text = random.Next(1, 5).ToString(CultureInfo.CurrentCulture);
				comp.BusField.Text = random.Next(1, 5).ToString(CultureInfo.CurrentCulture);
				comp.DiepteMaaiveldField.Text = random.NextDouble().ToString(CultureInfo.CurrentCulture);
				comp.MonsterdiepteMaaiveldField.Text = random.NextDouble().ToString(CultureInfo.CurrentCulture);
				comp.MonsterDiepteNAPField.Text = random.NextDouble().ToString(CultureInfo.CurrentCulture);
				comp.MonsterklasseField.Text = random.Next(1, 5).ToString(CultureInfo.CurrentCulture);
				comp.DatumProefField.Text = random.Next(1, 28) + "/" + random.Next(1, 12) + "/" + "18";

				comp.InitieelVolumeGewichtField.Text = random.Next(11, 20).ToString(CultureInfo.CurrentCulture);
				comp.DroogVolumeGewichtField.Text = random.Next(1, 10).ToString(CultureInfo.CurrentCulture);
				comp.WatergehalteVoorField.Text = random.Next(200, 600).ToString(CultureInfo.CurrentCulture);
				comp.WatergehalteNaField.Text = random.Next(100, 200).ToString(CultureInfo.CurrentCulture);

			}
		}
	}
}
