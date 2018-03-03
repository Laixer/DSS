using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;


namespace DSS_WPF
{
	/// <summary>
	/// Interaction logic for FilePickerWindow.xaml
	/// </summary>
	public partial class FilePickerWindow : Window
	{
		public FilePickerWindow()
		{
			InitializeComponent();
		}

		private void Pick_File_Click(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("swag");
			OpenFilePicker();
		}

		private void OpenFilePicker()
		{
			var picker = new OpenFileDialog();
			picker.Filter = "CSV files (*.csv)|*.csv";

			if (picker.ShowDialog() == true)
			{
				TextBlock.Text = "Aan het lezen: " + picker.FileName;
				
				ResultsWindow window = new ResultsWindow();
				window.Show();
				this.Close();
			}
			else
			{
				Debug.Write("file is null");
			}
		}
	}
}
