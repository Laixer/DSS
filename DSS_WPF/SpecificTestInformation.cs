using System;
using System.ComponentModel;

namespace Dss
{
	public class SpecificTestInformation
	{
		private String boring;
		private int monster;
		private int bus;
		private double diepteMaaiveld;
		private double monsterDiepteMaaiveld;
		private double monsterDiepteNAP;
		private int monsterKlasse;
		private String datumProef;
		private double initieelVolumegewicht;
		private double droogVolumegewicht;
		private double watergehalteVoor;
		private double watergehalteNa;

		public string Boring { get => boring; set => boring = value; }
		public int Monster { get => monster; set => monster = value; }
		public int Bus { get => bus; set => bus = value; }
		public double DiepteMaaiveld { get => diepteMaaiveld; set => diepteMaaiveld = value; }
		public double MonsterDiepteMaaiveld { get => monsterDiepteMaaiveld; set => monsterDiepteMaaiveld = value; }
		public double MonsterDiepteNap { get => monsterDiepteNAP; set => monsterDiepteNAP = value; }
		public int MonsterKlasse { get => monsterKlasse; set => monsterKlasse = value; }
		public string DatumProef { get => datumProef; set => datumProef = value; }
		public double InitieelVolumegewicht { get => initieelVolumegewicht; set => initieelVolumegewicht = value; }
		public double DroogVolumegewicht { get => droogVolumegewicht; set => droogVolumegewicht = value; }
		public double WatergehalteVoor { get => watergehalteVoor; set => watergehalteVoor = value; }
		public double WatergehalteNa { get => watergehalteNa; set => watergehalteNa = value; }

		override public String ToString()
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
