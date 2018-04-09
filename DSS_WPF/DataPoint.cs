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
		[FieldQuoted] [FieldConverter(ConverterKind.Int32)]
		public int stage_number { get; set; }
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int time_since_start_test { get; set; } // in seconds
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int time_since_start_stage { get; set; } // in seconds
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float axial_displacement { get; set; } // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float axial_load { get; set; } // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_displacement { get; set; } // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_load { get; set; } // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_water_pressure { get; set; } // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int back_pressure { get; set; } // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int back_volume { get; set; } // in mm^3
		[FieldQuoted] [FieldConverter(ConverterKind.Int32)]
		public int undefined_transducer_1 { get; set; }
		[FieldQuoted] [FieldConverter(ConverterKind.Int32)]
		public int undefined_transducer_2 { get; set; }
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_torque { get; set; } // in Nm
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_angle { get; set; } // in degrees
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int lower_chamber_pressure { get; set; } // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int lower_chamber_volume { get; set;} // in mm^3
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float axial_displacement_2 { get; set;} // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_displacement_2 { get; set; } // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_load_1 { get; set; } // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int ring_shear_load_2 { get; set; } // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int axial_load_2 { get; set; } // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int horizontal_load_2 { get; set; } // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int horizontal_load_3 { get; set; } // in kN
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int axial_stroke { get; set; } // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int horizontal_stroke { get; set; } // in mm
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_air_pressure { get; set; } // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_air_pressure_2 { get; set; } // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int atmospheric_pressure { get; set; } // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int back_to_air_differential { get; set; } // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int cell_pressure { get; set; } // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int cell_volume { get; set; } // in mm^3
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int pore_air_volume { get; set; } // in mm^3
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float axial_strain { get; set; } // as a percentage
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float normal_stress { get; set; } // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_strain { get; set; } // or horizontal ring displacement for ring shear machine. In kPa.
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_stress { get; set; } // or shear stress for ring shear machine. In kPa.
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float horizontal_eff_stress { get; set; } // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float effective_area { get; set; } // in mm^2
		[FieldQuoted][FieldConverter(ConverterKind.Single, ",")]
		public float normal_effective_stress { get; set; } // in kPa
		[FieldQuoted][FieldConverter(ConverterKind.Int32)]
		public int average_ring_shear_load { get; set; }
	}

}
