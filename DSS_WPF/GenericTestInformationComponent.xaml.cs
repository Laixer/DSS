using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Dss
{
	/// <summary>
	/// Interaction logic for GenericTestInformationComponent.xaml. This component
	/// allows the user to enter the generic information about a test
	/// (the information that is equal for all tests in a set).
	/// </summary>
	public partial class GenericTestInformationComponent : UserControl
	{
		public GenericTestInformationComponent()
		{
			InitializeComponent();
		}


		// This method uses such a large number of try/catch blocks
		// because it's the only way to be able to retrieve
		// which input field was not able to be converted to a double.
		// We want to know this because we can then show a specific error
		// stating which field has invalid input.

		/// <summary>
		/// Parses the information the user entered, verifies that it's valid
		/// (e.g. check that when decimal values are expected, the entered string
		/// can be converted to a decimal value).
		/// </summary>
		/// <returns></returns>
		/// 
		//  Because this method converts the text the user has entered, which can fail,
		//	this method shouldn't be a method (see https://docs.microsoft.com/en-us/visualstudio/code-quality/ca1024-use-properties-where-appropriate)
		[SuppressMessage("Microsoft.Design", "CA1024:ChangeToProperty")] // because this method does conversion of the
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
