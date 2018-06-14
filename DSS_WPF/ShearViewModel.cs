using System;

namespace Dss
{
	public class ShearViewModel
	{
		public float SigmaNForShearStrainPercentage(float shearStrainPercentage)
		{
			float error = float.MaxValue;
			int i = 0;
			int bestIndex = 0;
			while (i < GetDataPoints().Length)
			{
				float currentValue = GetDataPoints()[i].HorizontalStrain;
				if (Math.Abs(currentValue - shearStrainPercentage) < error)
				{
					error = shearStrainPercentage - currentValue;
					//Debug.WriteLine("error is " + error);
					bestIndex = i;
				}
				i++;
			};

			return GetDataPoints()[bestIndex].NormalStress;
		}

		public float TauForShearStrainPercentage(float shearStrainPercentage)
		{
			float error = float.MaxValue;
			int i = 0;
			int bestIndex = 0;
			while (i < GetDataPoints().Length)
			{
				float currentValue = GetDataPoints()[i].HorizontalStrain - (float)GenericTestInformation.CorrectieWaardeB - ((float)GenericTestInformation.CorrectieWaardeA * shearStrainPercentage);
				if (Math.Abs(currentValue - shearStrainPercentage) < error)
				{
					error = shearStrainPercentage - currentValue;
					bestIndex = i;
				}
				i++;
			};

			return GetDataPoints()[bestIndex].HorizontalStress;
		}
		private DataPoint[] _dataPoints;
		public DataPoint[] GetDataPoints()
		{
			return _dataPoints;
		}

		public GenericTestInformation GenericTestInformation { get; }

		private readonly SpecificTestInformation[] specificTestInformation;

		public SpecificTestInformation[] GetSpecificTestInformation()
		{
			return specificTestInformation;
		}

		public ShearViewModel(DataPoint[] dataPoints, GenericTestInformation testInformation)
		{
			this._dataPoints = dataPoints;
			this.GenericTestInformation = testInformation;
		}

		public ShearViewModel(DataPoint[] dataPoints, GenericTestInformation testInformation, SpecificTestInformation[] specificTestInformation)
		{
			this._dataPoints = dataPoints;
			this.GenericTestInformation = testInformation;
			this.specificTestInformation = specificTestInformation;
		}


	}
}
