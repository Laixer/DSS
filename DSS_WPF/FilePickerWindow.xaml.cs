using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;


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
			OpenFilePicker();
		}

		private void OpenFilePicker()
		{
			var picker = new OpenFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv"
			};
			picker.Multiselect = true;

			if (picker.ShowDialog() == true)
			{
				TestInformationWindow window = new TestInformationWindow(picker.FileNames);
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
