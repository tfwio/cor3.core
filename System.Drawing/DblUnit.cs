#region User/License
// Copyright (c) 2005-2013 tfwroble
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace System.Cor3.Drawing
{
	/// <summary>
	/// Isn't this the same as UnitD?
	/// </summary>
	public class DblUnit
	{
		#region basic definitions
		static double __px = 92;
		#endregion
		#region
		[DefaultValue(UnitType.Pixel)]
		public UnitType CoordinateSpace {
			get { return coordinateSpace; } set { coordinateSpace = value; }
		} UnitType coordinateSpace = UnitType.Pixel;
		[DefaultValue(UnitType.Pixel)]
		public UnitType OutputUnit {
			get { return outputUnit; } set { outputUnit = value; }
		} UnitType outputUnit = UnitType.Pixel;
		#endregion

		#region REGEX
		static readonly Regex RegexParser = new Regex(
			@"(?<unit>([0-9.]+))\s*(?<type>(px|pt|pc|in|cm|mm))",
			RegexOptions.CultureInvariant|
			RegexOptions.IgnoreCase);
		
		public void SetValue(string value)
		{
			Regex r = RegexParser;
			MatchCollection m = r.Matches(value);
			SetValue(
				double.Parse(m[0].Groups["unit"].Value),
				ConvertType(m[0].Groups["type"].Value),
				UnitType.Pixel
			);
			r = null;
			m = null;
		}

		#endregion
		#region REGEX TYPE
		static UnitType ConvertType(string type)
		{
			string t = type.ToLower();
//			Global.statB(t);
			Debug.Print("{0}",t);
			switch (t)
			{
					case "px": return UnitType.Pixel;
					case "pt": return UnitType.Point;
					case "pc": return UnitType.Pica;
					case "in": return UnitType.Inch;
					case "cm": return UnitType.Centimeter;
					case "mm": return UnitType.Millimeter;
					default: return UnitType.Invalid;
			}
		}
		static string ConvertType(UnitType type)
		{
			switch (type)
			{
					case UnitType.Pixel: return "px";
					case UnitType.Point: return "pt";
					case UnitType.Pica: return "pc";
					case UnitType.Inch: return "in";
					case UnitType.Centimeter: return "cm";
					case UnitType.Millimeter: return "mm";
					default: return UnitType.Invalid.ToString();
			}
		}
		#endregion

		double nativeInput, nativeValue;
		public double NativeValue { get { return nativeValue; } internal set { nativeValue = value; } }

		public double GetValue(UnitType type)
		{
			return NativeValue / Multiply(type);
		}
		
		double Multiplier { get { return Multiply(coordinateSpace); } }
		double Multiply(UnitType type)
		{
			switch (type)
			{
					case UnitType.Millimeter: return 1;
					case UnitType.Pixel: return 25.4/__px;
					case UnitType.Point: return 25.4/72;
					case UnitType.Pica: return 25.4/6;
					case UnitType.Inch: return 25.4;
					case UnitType.Centimeter: return 10;
					default: return double.NaN;
			}
		}
		
		public void SetValue(double value, UnitType type)
		{
			nativeInput = value;
			coordinateSpace = type;
			nativeValue = value * Multiply(type);
		}
		public void SetValue(double value, UnitType type, UnitType outUnit)
		{
//			Global.statB("{0} — {1} — {2}",value,type,outUnit);
			Debug.Print("{0} — {1} — {2}",value,type,outUnit);
			OutputUnit = outUnit;
			SetValue(value,type);
		}

		public double Pixel { get { return nativeValue / Multiply(UnitType.Pixel); } set { SetValue(value,UnitType.Pixel); } }
		public double Point { get { return nativeValue / Multiply(UnitType.Point); } set { SetValue(value,UnitType.Point); } }
		public double Pica { get { return nativeValue / Multiply(UnitType.Pica); } set { SetValue(value,UnitType.Pica); } }
		public double Inch { get { return nativeValue / Multiply(UnitType.Inch); } set { SetValue(value,UnitType.Inch); } }
		public double Cm { get { return nativeValue / Multiply(UnitType.Centimeter); } set { SetValue(value,UnitType.Centimeter); } }
		public double Mm { get { return nativeValue; } set { SetValue(value,UnitType.Millimeter); } }
		
		public DblUnit(string value)
		{
			SetValue(value);
		}
		public DblUnit(double value, UnitType type) : this(value,type,UnitType.Pixel)
		{
		}
		public DblUnit(double value, UnitType type, UnitType outUnit)
		{
			SetValue(value,type,outUnit);
		}
		
		static public DblUnit operator +(DblUnit a, DblUnit b){ return new DblUnit(a.NativeValue+b.NativeValue,a.CoordinateSpace,a.OutputUnit); }
		static public DblUnit operator -(DblUnit a, DblUnit b){ return new DblUnit(a.NativeValue-b.NativeValue,a.CoordinateSpace,a.OutputUnit); }
		static public DblUnit operator *(DblUnit a, DblUnit b){ return new DblUnit(a.NativeValue*b.NativeValue,a.CoordinateSpace,a.OutputUnit); }
		static public DblUnit operator /(DblUnit a, DblUnit b){ return new DblUnit(a.NativeValue/b.NativeValue,a.CoordinateSpace,a.OutputUnit); }
		static public DblUnit operator %(DblUnit a, DblUnit b){ return new DblUnit(a.NativeValue % b.NativeValue,a.CoordinateSpace,a.OutputUnit); }
		static public bool operator >(DblUnit a, DblUnit b){ return a.NativeValue > b.NativeValue; }
		static public bool operator <(DblUnit a, DblUnit b){ return a.NativeValue < b.NativeValue; }
		static public DblUnit operator ++(DblUnit a){ return new DblUnit(a.NativeValue++,a.CoordinateSpace,a.OutputUnit); }
		static public DblUnit operator --(DblUnit a){ return new DblUnit(a.NativeValue--,a.CoordinateSpace,a.OutputUnit); }
		
		static public implicit operator double(DblUnit a){ return a.GetValue(a.OutputUnit); }
		static public implicit operator DblUnit(double a){ return a = new DblUnit(a,UnitType.Pixel); }
		static public implicit operator float(DblUnit a){ return (float)(a.GetValue(a.OutputUnit)); }
		static public implicit operator int(DblUnit a){ return (int)Math.Round(a.GetValue(a.OutputUnit),0); }
		static public implicit operator string(DblUnit a){ return a.ToString(); }
		static public implicit operator DblUnit(string str) { return new DblUnit(str); }
		public override string ToString()
		{
			return string.Format("{0}{1}",GetValue(OutputUnit),ConvertType(OutputUnit));
		}
	}
}
