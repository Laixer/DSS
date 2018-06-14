using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dss
{
	public class GenericTestInformation
	{
		private String project;
		private String projectnummer;
		private String laborant;
		private String adviseur;
		private String teamleider;

		private double initieleHoogte;
		private double diameter;
		private String grondSoort;
		private String soortMonster;
		private String correctie;

		private double correctieWaardeA;
		private double correctieWaardeB;

		public string Project { get => project; set => project = value; }
		public string Projectnummer { get => projectnummer; set => projectnummer = value; }
		public string Laborant { get => laborant; set => laborant = value; }
		public string Adviseur { get => adviseur; set => adviseur = value; }
		public string Teamleider { get => teamleider; set => teamleider = value; }
		public double InitieleHoogte { get => initieleHoogte; set => initieleHoogte = value; }
		public double Diameter { get => diameter; set => diameter = value; }
		public string GrondSoort { get => grondSoort; set => grondSoort = value; }
		public string SoortMonster { get => soortMonster; set => soortMonster = value; }
		public string Correctie { get => correctie; set => correctie = value; }
		public double CorrectieWaardeA { get => correctieWaardeA; set => correctieWaardeA = value; }
		public double CorrectieWaardeB { get => correctieWaardeB; set => correctieWaardeB = value; }
	}
}
