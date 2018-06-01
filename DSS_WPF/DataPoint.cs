using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;



namespace DSS_WPF
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
		public int stage_number { get; set; }


		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		/// <summary>
		/// The time in seconds that has elapsed since the start of the test.
		/// </summary>
		public int time_since_start_test { get; set; }


		/// <summary>
		/// The time in seconds that has elapsed since the start of the test stage.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int time_since_start_stage { get; set; }


		/// <summary>
		/// The axial displacement in millimeters.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float axial_displacement { get; set; }


		/// <summary>
		/// The axial load in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float axial_load { get; set; }


		/// <summary>
		/// The horizontal displacement in millimeters.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_displacement { get; set; }


		/// <summary>
		/// The horizontal load in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_load { get; set; }


		/// <summary>
		/// The pore water pressure in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_water_pressure { get; set; }


		/// <summary>
		/// The back pressure in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int back_pressure { get; set; }


		/// <summary>
		/// The back volume, in cubic millimeters (mm^3).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int back_volume { get; set; }


		/// <summary>
		/// Undefined transducer 1.
		/// (I don't know what this means)
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int undefined_transducer_1 { get; set; }


		/// <summary>
		/// Undefined transducer 2.
		/// (I don't know what this means)
		/// </summary>
		[FieldQuoted] [FieldConverter(ConverterKind.Int32)]
		public int undefined_transducer_2 { get; set; }


		/// <summary>
		/// Ring shear torque, in newton meter (Nm).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_torque { get; set; }


		/// <summary>
		/// Ring shear angle, in degrees.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_angle { get; set; }


		/// <summary>
		/// Lower chamber pressure, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int lower_chamber_pressure { get; set; }


		/// <summary>
		/// Lower chamber volume, in cubic millimeter (mm^3).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int lower_chamber_volume { get; set;} // in mm^3


		/// <summary>
		/// Second axial displacement measurement, in millimeter (mm).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float axial_displacement_2 { get; set;}


		/// <summary>
		/// Second horizontal displacement measurement, in millimeter (mm).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_displacement_2 { get; set; }


		/// <summary>
		/// Ring shear load, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_load_1 { get; set; }


		/// <summary>
		/// Second ring shear load measurement, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_load_2 { get; set; }


		/// <summary>
		/// Second axial load measurement, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int axial_load_2 { get; set; } // in kN


		/// <summary>
		/// Second horizontal load measurement, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int horizontal_load_2 { get; set; }


		/// <summary>
		/// Third horizontal load measurement, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int horizontal_load_3 { get; set; }


		/// <summary>
		/// The axial stroke, in millimeter (mm).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int axial_stroke { get; set; } // in mm


		/// <summary>
		/// The horizontal stroke, in millimeter (mm).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int horizontal_stroke { get; set; }


		/// <summary>
		/// The pore air pressure, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_air_pressure { get; set; }


		/// <summary>
		/// The second pore air pressure measurement, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_air_pressure_2 { get; set; } // in kPa


		/// <summary>
		/// The atmospheric pressure, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int atmospheric_pressure { get; set; } // in kPa


		/// <summary>
		/// The back-to-air differential, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int back_to_air_differential { get; set; }


		/// <summary>
		/// The cell pressure, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int cell_pressure { get; set; } // in kPa


		/// <summary>
		/// The cell volume, in cubic millimeters (mm^3).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int cell_volume { get; set; }


		/// <summary>
		/// The pore air volume, in cubic millimeters (mm^3).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_air_volume { get; set; } // in mm^3


		/// <summary>
		/// The axial strain as a percentage (0-100).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float axial_strain { get; set; }


		/// <summary>
		/// The normal stress, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float normal_stress { get; set; }


		/// <summary>
		/// The horizontal strain, in kilopascal (kPa).
		/// equal to horizontal ring displacement for ring shear machine.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_strain { get; set; }


		/// <summary>
		/// The horizontal stress, in kilopascal (kPa).
		/// equal to shear stress for ring shear machine.
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_stress { get; set; }


		/// <summary>
		/// The horizontal effective stress, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_eff_stress { get; set; }


		/// <summary>
		/// The effective area, in squared millimeters (mm^2).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float effective_area { get; set; } // in mm^2


		/// <summary>
		/// The normal effective stress, in kilopascal (kPa).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float normal_effective_stress { get; set; }


		/// <summary>
		/// The average ring shear load, in kilonewton (kN).
		/// </summary>
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int average_ring_shear_load { get; set; }
	}
}
