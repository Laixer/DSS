using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;



namespace DSS_WPF
{
	[DelimitedRecord(",")]
	[IgnoreFirst(23)]
	public class DataPoint
	{
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int stage_number;
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int time_since_start_test; // in seconds
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int time_since_start_stage; // in seconds
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float axial_displacement; // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float axial_load; // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float horizontal_displacement; // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float horizontal_load; // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_water_pressure; // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int back_pressure; // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int back_volume; // in mm^3
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int undefined_transducer_1;
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int undefined_transducer_2;
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_torque; // in Nm
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_angle; // in degrees
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int lower_chamber_pressure; // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int lower_chamber_volume; // in mm^3
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float axial_displacement_2; // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float horizontal_displacement_2; // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_load_1; // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_load_2; // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int axial_load_2; // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int horizontal_load_2; // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int horizontal_load_3; // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int axial_stroke; // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int horizontal_stroke; // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_air_pressure; // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_air_pressure_2; // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int atmospheric_pressure; // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int back_to_air_differential; // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int cell_pressure; // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int cell_volume; // in mm^3
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_air_volume; // in mm^3
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int axial_strain; // as a percentage
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float normal_stress; // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float horizontal_strain; // or horizontal ring displacement for ring shear machine. In kPa.
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float horizontal_stress; // or shear stress for ring shear machine. In kPa.
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float horizontal_eff_stress; // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float effective_area; // in mm^2
		[FieldQuoted][FieldConverter(ConverterKind.Single)]
		public float normal_effective_stress; // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int average_ring_shear_load;
	}

}
