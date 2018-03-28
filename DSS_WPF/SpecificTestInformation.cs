using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DSS_WPF
{
	public class SpecificTestInformation
	{
		public String Boring;
		public int Monster;
		public int Bus;
		public double DiepteMaaiveld;
		public double MonsterDiepteMaaiveld;
		public double MonsterDiepteNAP;
		public int MonsterKlasse;
		public String DatumProef;
		public double InitieelVolumegewicht;
		public double DroogVolumegewicht;
		public double WatergehalteVoor;
		public double WatergehalteNa;

		public String toString()
		{
			String description = "";
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this))
			{
				string name = descriptor.Name;
				object value = descriptor.GetValue(this);
				description += name + " = " + value + "\n";
			}
			return description;
		}
	}


}
