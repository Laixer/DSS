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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using FileHelpers;

namespace DSS_WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			var engine = new FileHelperEngine<DataPoint>();

			// To Read Use:
			Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
			var result = engine.ReadFile("\\\\Mac\\Home\\Desktop\\proef_1.csv");
			stopwatch.Stop();
			Console.WriteLine("took " + stopwatch.ElapsedMilliseconds + " milliseconds");
			
			// result is now an array of Customer
			int i = 0;
			foreach (var dataPoint in result)
			{
				//Debug.WriteLine("dataPoint " + i);
				i++;
			}
		}
	}
}
