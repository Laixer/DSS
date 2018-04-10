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

namespace DSS_WPF
{
	/// <summary>
	/// Interaction logic for GeneralDataGrid.xaml
	/// </summary>
	public partial class GeneralDataGrid : UserControl
	{
		private ShearViewModel _model;
		public ShearViewModel model
		{
			set
			{
				_model = value;
				UpdateItemsSource();
			}

			get
			{
				return _model;
			}
		}

		public GeneralDataGrid()
		{
			InitializeComponent();
			UpdateItemsSource();
		}

		private void UpdateItemsSource()
		{
			List<GeneralDataEntry> items = new List<GeneralDataEntry>();
			items.Add(new GeneralDataEntry("Initiële hoogte:", /*model.TestInformation.InitieleHoogte*/(30.0).ToString(), "mm"));
			DataGrid.ItemsSource = items;
		}
	}
}
