using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DSS_WPF
{
	public class ShearViewModel
	{
		public float SigmaNForShearStrainPercentage(float shearStrainPercentage)
		{
			float error = float.MaxValue;
			int i = 0;
			int bestIndex = 0;
			while (i < dataPoints.Length) 
			{
				float currentValue = dataPoints[i].horizontal_strain;
				if (Math.Abs(currentValue - shearStrainPercentage) < error)
				{
					error = shearStrainPercentage - currentValue;
					//Debug.WriteLine("error is " + error);
					bestIndex = i;
				}
				i++;
			};

			return dataPoints[bestIndex].normal_stress;
		}
		
		public float TauForShearStrainPercentage(float shearStrainPercentage)
		{
			float error = float.MaxValue;
			int i = 0;
			int bestIndex = 0;
			while (i < dataPoints.Length)
			{
				float currentValue = dataPoints[i].horizontal_strain - (float)testInformation.CorrectieWaardeB - ((float)testInformation.CorrectieWaardeA * shearStrainPercentage);
				if (Math.Abs(currentValue - shearStrainPercentage) < error)
				{
					error = shearStrainPercentage - currentValue;
					bestIndex = i;
				}
				i++;
			};

			return dataPoints[bestIndex].horizontal_stress;
		}
		private DataPoint[] dataPoints;
		private GenericTestInformation testInformation;

		public ShearViewModel(DataPoint[] dataPoints, GenericTestInformation testInformation)
		{
			this.dataPoints = dataPoints;
			this.testInformation = testInformation;
		}

		
	}
}
