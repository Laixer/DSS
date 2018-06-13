using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;



namespace Dss
{
	// The csv files that this program is made for
	// are delimeted by commas.
	[DelimitedRecord(",")]

	// The first 23 rows of the csv files are ignored since they
	// dont contain data points
	[IgnoreFirst(23)]
	/// <summary>
	/// This class is basically a simple model class storing the properties
	/// that are read from the csv files the user selects. Using FileHelpers
	/// instances of this class automatically get created: it usually isn't necessary
	/// to create instances yourself.
	/// </summary>

	public class DataPoint
	{
		/// <summary>
		/// The stage number, usually 1 or 2.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int StageNumber { get; set; }


		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		/// <summary>
		/// The time in seconds that has elapsed since the start of the test.
		/// </summary>
		public int TimeSinceStartTest { get; set; }


		/// <summary>
		/// The time in seconds that has elapsed since the start of the test stage.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int TimeSinceStartStage { get; set; }


		/// <summary>
		/// The axial displacement in millimeters.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float AxialDisplacement { get; set; }


		/// <summary>
		/// The axial load in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float AxialLoad { get; set; }


		/// <summary>
		/// The horizontal displacement in millimeters.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float HorizontalDisplacement { get; set; }


		/// <summary>
		/// The horizontal load in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float HorizontalLoad { get; set; }


		/// <summary>
		/// The pore water pressure in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int PoreWaterPressure { get; set; }


		/// <summary>
		/// The back pressure in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int Backpressure { get; set; }


		/// <summary>
		/// The back volume, in cubic millimeters (mm^3).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int BackVolume { get; set; }


		/// <summary>
		/// Undefined transducer 1.
		/// (I don't know what this means)
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int UndefinedTransducer1 { get; set; }


		/// <summary>
		/// Undefined transducer 2.
		/// (I don't know what this means)
		/// </summary>
		[FieldQuoted] [FieldConverter(ConverterKind.Int32)]
		public int UndefinedTransducer2 { get; set; }


		/// <summary>
		/// Ring shear torque, in newton meter (Nm).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int RingShearTorque { get; set; }


		/// <summary>
		/// Ring shear angle, in degrees.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int RingShearAngle { get; set; }


		/// <summary>
		/// Lower chamber pressure, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int LowerChamberPressure { get; set; }


		/// <summary>
		/// Lower chamber volume, in cubic millimeter (mm^3).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int LowerChamberVolume { get; set;} // in mm^3


		/// <summary>
		/// Second axial displacement measurement, in millimeter (mm).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float AxialDisplacement2 { get; set;}


		/// <summary>
		/// Second horizontal displacement measurement, in millimeter (mm).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float HorizontalDisplacement2 { get; set; }


		/// <summary>
		/// Ring shear load, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int RingShearLoad1 { get; set; }


		/// <summary>
		/// Second ring shear load measurement, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int RingShearLoad2 { get; set; }


		/// <summary>
		/// Second axial load measurement, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int AxialLoad2 { get; set; } // in kN


		/// <summary>
		/// Second horizontal load measurement, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int HorizontalLoad2 { get; set; }


		/// <summary>
		/// Third horizontal load measurement, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int HorizontalLoad3 { get; set; }


		/// <summary>
		/// The axial stroke, in millimeter (mm).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int AxialStroke { get; set; } // in mm


		/// <summary>
		/// The horizontal stroke, in millimeter (mm).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int HorizontalStroke { get; set; }


		/// <summary>
		/// The pore air pressure, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int PoreAirPressure { get; set; }


		/// <summary>
		/// The second pore air pressure measurement, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int PoreAirPressure2 { get; set; } // in kPa


		/// <summary>
		/// The atmospheric pressure, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int AtmosphericPressure { get; set; } // in kPa


		/// <summary>
		/// The back-to-air differential, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int BackToAirDifferential { get; set; }


		/// <summary>
		/// The cell pressure, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int CellPressure { get; set; } // in kPa


		/// <summary>
		/// The cell volume, in cubic millimeters (mm^3).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int CellVolume { get; set; }


		/// <summary>
		/// The pore air volume, in cubic millimeters (mm^3).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int PoreAirVolume { get; set; } // in mm^3


		/// <summary>
		/// The axial strain as a percentage (0-100).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float AxialStrain { get; set; }


		/// <summary>
		/// The normal stress, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float NormalStress { get; set; }


		/// <summary>
		/// The horizontal strain, in kilopascal (kPa).
		/// equal to horizontal ring displacement for ring shear machine.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float HorizontalStrain { get; set; }


		/// <summary>
		/// The horizontal stress, in kilopascal (kPa).
		/// equal to shear stress for ring shear machine.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float HorizontalStress { get; set; }


		/// <summary>
		/// The horizontal effective stress, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float HorizontalEffectiveStress { get; set; }


		/// <summary>
		/// The effective area, in squared millimeters (mm^2).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float EffectiveArea { get; set; } // in mm^2


		/// <summary>
		/// The normal effective stress, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float NormalEffectiveStress { get; set; }


		/// <summary>
		/// The average ring shear load, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int AverageRingShearLoad { get; set; }
	}
}
