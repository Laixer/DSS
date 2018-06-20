using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Dss
{
	/// <summary>
	/// Interaction logic for GenericTestInformationComponent.xaml
	/// </summary>
	public partial class GenericTestInformationComponent : UserControl
	{
		public GenericTestInformationComponent()
		{
			InitializeComponent();
		}

		[SuppressMessage("Microsoft.Design", "CA1024:ChangeToProperty")]

		public GenericTestInformation GetInformation()
		{
			GenericTestInformation information = new GenericTestInformation
			{
				Project = ProjectField.Text,
				Projectnummer = ProjectnummerField.Text,
				Laborant = LaborantField.Text,
				Adviseur = AdviseurField.Text,
				Teamleider = TeamleiderField.Text
			};
			try
			{
				information.InitieleHoogte = Convert.ToDouble(InitieleHoogteField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde Initiele hoogte (" + InitieleHoogteField.Text + ") is geen decimale waarde");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde Initiele hoogte (" + InitieleHoogteField.Text + ") is geen decimale waarde");
				return null;
			}

			try
			{
				information.Diameter = Convert.ToDouble(DiameterField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Waarde diameter (" + DiameterField.Text + ") is geen decimale waarde");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Waarde diameter (" + DiameterField.Text + ") is geen decimale waarde");
				return null;
			}

			information.GrondSoort = GrondsoortField.Text;
			information.SoortMonster = SoortMonsterField.Text;
			information.Correctie = CorrectieField.Text;

			try
			{
				information.CorrectieWaardeA = Convert.ToDouble(CorrectieAField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Correctiewaarde a (" + CorrectieAField.Text + ") is geen decimale waarde");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Correctiewaarde a (" + CorrectieAField.Text + ") is geen decimale waarde");
				return null;
			}

			try
			{
				information.CorrectieWaardeB = Convert.ToDouble(CorrectieBField.Text, CultureInfo.CurrentCulture);
			}
			catch (FormatException)
			{
				MessageBox.Show("Correctiewaarde b (" + CorrectieBField.Text + ") is geen decimale waarde");
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("Correctiewaarde b (" + CorrectieBField.Text + ") is geen decimale waarde");
				return null;
			}

			return information;
		}
	}
}
