using System;
using FileHelpers;
using System.Diagnostics;

public class IntConverter: ConverterBase
{
	public override object StringToField(string from)
	{
		Debug.WriteLine("parsing " + from);
		return Int32.Parse(from.Trim('"'));
	}
}
