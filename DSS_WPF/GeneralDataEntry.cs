using System;

namespace Dss
{
	/// <summary>
	/// Simple (view) model class that encapsulates a table row data model
	/// with a property, a value for that property, and a unit for it.
	/// Example: a property "initial height" with value "50" and unit "cm".
	/// </summary>
	class GeneralDataEntry
	{
		// These ca1811 suppressions are here because somehow the compiler doesn't see
		// that we actually do use these fields, but only in xaml files.

		/// <summary>
		/// The property for of the entry, for example "initial height".
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public String Property { get; set; }
		/// <summary>
		/// The value for the entry, for example "50". These are strings because they'll be shown in UI.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public String Value { get; set; }
		/// <summary>
		/// The unit for the entry, for example "cm" (centimeters).
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public String Unit { get; set; }

		/// <param name="property">The value for the <see cref="Property"/> field</param>
		/// <param name="value">The value for the <see cref="Value"/> field</param>
		/// <param name="unit">The value for the <see cref="Unit"/> field</param>
		public GeneralDataEntry(string property, string value, string unit)
		{
			Property = property;
			Value = value;
			Unit = unit;
		}
	}
}
