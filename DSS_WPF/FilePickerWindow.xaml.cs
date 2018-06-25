using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;

namespace Dss
{
	/// <summary>
	/// Interaction logic for FilePickerWindow.xaml. The FilePickerWindow
	/// is the first window shown to the user when running the program: 
	/// it simply shows a button to pick the csv files to read. It then passes the file names
	/// to the TestInformationWindow: no actual reading of files is done in FilePickerWindow.
	/// </summary>
	public partial class FilePickerWindow : Window
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public FilePickerWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Action handler for the 'pick files' button.
		/// </summary>
		/// <param name="sender">The sender of the event</param>
		/// <param name="e">Potential arguments that were passed</param>
		private void Pick_File_Click(object sender, RoutedEventArgs e)
		{
			OpenFilePicker();
		}

		/// <summary>
		/// Called by action handler, this method opens a open file dialog,
		/// filters for CSV files, and shows the dialog. Important note:
		/// multiple files can be picked.
		/// </summary>
		private void OpenFilePicker()
		{
			var picker = new OpenFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv"
			};
			picker.Multiselect = true;

			if (picker.ShowDialog().Value)
			{
				TestInformationWindow window = new TestInformationWindow(picker.FileNames);
				window.Show();
				Close();
			}
#if DEBUG
            else
			{
				Debug.Write("file is null");
			}
#endif
        }
	}
}
