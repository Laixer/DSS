using System;

namespace Dss
{
	/// <summary>
	/// Model which can calculate the sigma n and tau n values
	/// for a certain shear strain percentage.
	/// </summary>
	public class ShearViewModel
	{
		/// <summary>
		/// Gets the sigma n value for a certain shear strain percentage
		/// </summary>
		/// <param name="shearStrainPercentage">the percentage of shear strain for which to look up sigma n</param>
		/// <returns>the calculated sigma n value</returns>
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

		/// <summary>
		/// Gets the tau value for a certain shear strain percentage
		/// </summary>
		/// <param name="shearStrainPercentage">the percentage of shear strain for which to look up sigma n</param>
		/// <returns>the calculated tau value</returns>
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
		private readonly DataPoint[] _dataPoints;
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
