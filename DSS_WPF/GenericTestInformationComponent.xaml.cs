﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DSS_WPF
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

		public GenericTestInformation GetInformation()
		{
			GenericTestInformation information = new GenericTestInformation();

			information.Project = ProjectField.Text;
			information.Projectnummer = ProjectnummerField.Text;
			information.Laborant = LaborantField.Text;
			information.Adviseur = AdviseurField.Text;
			information.Teamleider = TeamleiderField.Text;
			try
			{
				information.InitieleHoogte = Convert.ToDouble(InitieleHoogteField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde Initiele hoogte (" + .nitieleHoogteField.Text + ") is geen decimale waarde");
				return null;
			}

			try
			{
				information.Diameter = Convert.ToDouble(DiameterField.Text);
			}
			catch
			{
				MessageBox.Show("Waarde diameter (" + DiameterField.Text + ") is geen decimale waarde");
				return null;
			}

			information.GrondSoort = GrondsoortField.Text;
			information.SoortMonster = SoortMonsterField.Text;
			information.Correctie = CorrectieField.Text;

			try
			{
				information.CorrectieWaardeA = Convert.ToDouble(CorrectieAField.Text);
			}
			catch
			{
				MessageBox.Show("Correctiewaarde a (" + CorrectieAField.Text + ") is geen decimale waarde");
				return null;
			}

			try
			{
				information.CorrectieWaardeB = Convert.ToDouble(CorrectieBField.Text);
			}
			catch
			{
				MessageBox.Show("Correctiewaarde b (" + CorrectieBField.Text + ") is geen decimale waarde");
				return null;
			}

			return information;
		}
	}

}
}
