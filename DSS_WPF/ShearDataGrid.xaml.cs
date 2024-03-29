﻿using System.Windows.Controls;
using System.Collections.Generic;

namespace Dss
{
	/// <summary>
	/// Interaction logic for ShearDataGrid.xaml. This is the table in the excel sheet
	/// which shows the sigma and tau values for a bunch of shear strain values. 
	/// </summary>
	public partial class ShearDataGrid : UserControl
	{
		private ShearViewModel _model;
		public ShearViewModel Model
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
		

		public ShearDataGrid()
		{
			InitializeComponent();

		}

		/// <summary>
		/// Updates the item source of the shear strain data grid with the calculated values of sigma and tau
		/// for the predefined shear strain values.
		/// </summary>
		private void UpdateItemsSource()
		{
			float[] shearStrainValues = new float[] { 1.0f, 2.0f, 5.0f, 10.0f, 15.0f, 20.0f, 30.0f, 40.0f, 50.0f, 60.0f, 70.0f, 80.0f, 90.0f };
			List<DataPoint> itemsSource = new List<DataPoint>();
			

			float maxTau = 0;
			int maxTauIndex = -1;
			float maxStrain = 0;
			for (int i = 0; i < Model.GetDataPoints().Length; i++)
			{
				float tau = Model.GetDataPoints()[i].HorizontalStress;
				if (tau > maxTau)
				{
					maxTau = tau;
					maxTauIndex = i;
				}

				float strain = Model.GetDataPoints()[i].HorizontalStrain;
				if (strain > maxStrain)
				{
					maxStrain = strain;
				}
			}

			for (int i = 0; i < shearStrainValues.Length; i++)
			{
				DataPoint point = new DataPoint
				{
					HorizontalStrain = shearStrainValues[i],
					NormalStress = Model.SigmaNForShearStrainPercentage(shearStrainValues[i]),
					HorizontalStress = Model.TauForShearStrainPercentage(shearStrainValues[i])
				};
				if (i == 0 || (i > 0 && itemsSource[i-1].HorizontalStrain + 10 <= Utilities.RoundTo(maxStrain, -1)))
				{
					itemsSource.Add(point);
				} else
				{
					break;
				}
			}

			ShearStrainDataGrid.ItemsSource = itemsSource;

			MaxShearStrainDataGrid.ItemsSource = new DataPoint[] { Model.GetDataPoints()[maxTauIndex] };
		}
	}

	
}
