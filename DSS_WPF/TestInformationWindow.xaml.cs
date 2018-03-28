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
			GenericTestInformation genericTestInformation = GenericInformationComponent.GetInformation();
			SpecificTestInformation specificTestInformation1 = SpecificInformationComponent1.GetInformation();
			SpecificTestInformation specificTestInformation2 = SpecificInformationComponent2.GetInformation();
			SpecificTestInformation specificTestInformation3 = SpecificInformationComponent3.GetInformation();
			Debug.Write(specificTestInformation1);
		}
	}
}
