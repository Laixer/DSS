using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS_WPF
{
	public class ShearViewModel
	{
		public float SigmaNForShearStrainPercentage(float shearStrainPercentage)
		{
			float error = float.MaxValue;
			int i = 0;
			int bestIndex = 0;
			float currentValue = dataPoints[i].horizontal_strain;
			while (i < dataPoints.Length) 
			{
				if (Math.Abs(shearStrainPercentage - currentValue) < error)
				{
					error = shearStrainPercentage;
					bestIndex = i;
				}
			};

			return dataPoints[bestIndex].normal_stress;
		}
		
		/*public float TauN
		{
			get
			{

			}
		}*/
		private DataPoint[] dataPoints;
		private GenericTestInformation testInformation;

		public ShearViewModel(DataPoint[] dataPoints, GenericTestInformation testInformation)
		{
			this.dataPoints = dataPoints;
			this.testInformation = testInformation;
		}

		
	}
}
