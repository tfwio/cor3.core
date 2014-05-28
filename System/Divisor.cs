/* oOo * 11/19/2007 : 8:00 AM */
using System;
using System.Xml.Serialization;
namespace System
{
	// TODO: delete or re-appropriate if there are no dependencies.
	public class Divisor
	{
		[XmlAttribute("n")]
		public double Numerator { get;set; }
		
		[XmlAttribute("d")]
		public double Denominator { get;set; }
		
		[XmlIgnore] public double Value { get { return Numerator / Denominator; } }
		
		public double Multiply(double value)
		{
			return Value * value;
		}
		public double DivideBy(double value)
		{
			return Value / value;
		}
		public double DivideFrom(double value)
		{
			return value / Value;
		}
		
		public Divisor(double n, double d)
		{
			Numerator    = n;
			Denominator  = d;
		}
		/// <summary>
		/// 1 / d = ap.Value
		/// </summary>
		/// <param name="d"></param>
		public Divisor(double d)
		{
			Numerator    = 1;
			Denominator  = d;
		}
		/// <summary>
		/// nothing is set
		/// </summary>
		public Divisor()
		{
		}
	}
}
