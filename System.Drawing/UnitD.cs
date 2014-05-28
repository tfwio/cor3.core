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
using System.Text.RegularExpressions;

namespace System.Cor3.Drawing
{
	/// <summary>
	/// Isn't this the same as UnitD?
	/// </summary>
	public class UnitD
	{

		static public bool Round = false;
		static public int RoundPercision = 3;
		static public bool UseInchQuotation = false;
		
		#region basic definitions
		static decimal __px = 92;
		#endregion

		#region
		
		public UnitType NativeCoordinateSpace {
			get { return nativeCoordinateSpace; }
		} UnitType nativeCoordinateSpace;
		
		[DefaultValue(UnitType.Millimeter)]
		public UnitType CoordinateSpace { get { return coordinateSpace; } set { coordinateSpace = value; } }
		UnitType coordinateSpace;
		
//		[System.ComponentModel.DefaultValue(UnitType.Pixel)]
		static UnitType outputUnit = UnitType.Pixel;
		static public UnitType OutputUnit { get { return outputUnit; } set { outputUnit = value; } }
		#endregion

		#region Regex Type Inspector
		//		static System.Text.RegularExpressions.Regex unitParser = new Regex(
		//			@"([0-9.]+)\s*([PX|PT|PICA|IN|CM|MM|FT])",
		//			RegexOptions.CultureInvariant |
		//			RegexOptions.IgnoreCase
		//		);
		//		static System.Text.RegularExpressions.Match Match(string input)
		//		{
		//			return unitParser.Match(input);
		//		}
		static UnitType ConvertType(string type)
		{
			string t = type.ToLower();
//			Global.statB(t);
			switch (t)
			{
					case "px": return UnitType.Pixel;
					case "pt": return UnitType.Point;
					case "pc": return UnitType.Pica;
				case @"""":
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
					case UnitType.Inch: return UseInchQuotation ? "\"": "in";
					case UnitType.Centimeter: return "cm";
					case UnitType.Millimeter: return "mm";
					default: return UnitType.Invalid.ToString();
			}
		}
		#endregion
		
		decimal nativeInput;
		decimal nativeValue;
		public decimal NativeValue { get { return nativeValue; } internal set { nativeValue = value; } }
		
		public decimal GetValue(UnitType type)
		{
			return Round ? Math.Round(NativeValue / Multiply(type), RoundPercision) : NativeValue / Multiply(type) ;
		}

		decimal Multiplier { get { return Multiply(coordinateSpace); } }
		decimal Multiply(UnitType type)
		{
			switch (type)
			{
					case UnitType.Millimeter: return 1;
					case UnitType.Pixel: return 25.4m / __px;
					case UnitType.Point: return 25.4m/72;
					case UnitType.Pica: return 25.4m/6m;
					case UnitType.Inch: return 25.4m;
					case UnitType.Centimeter: return 10m;
					default: return decimal.Zero;
			}
		}
		
		public decimal SetValue(string value)
		{
			Regex r = new Regex(@"(?<unit>([0-9.]+))\s*(?<type>(px|pt|pc|in|cm|mm|""))",RegexOptions.CultureInvariant|RegexOptions.IgnoreCase);
			try {
				MatchCollection m = r.Matches(value);
				return SetValue(
					decimal.Parse(m[0].Groups["unit"].Value),
					ConvertType(m[0].Groups["type"].Value)
				);
				m = null;
			} catch (Exception)
			{
				return SetValue(decimal.Parse(value,Globalization.NumberStyles.Any),OutputUnit);
			}
			r = null;
			return decimal.Parse(value);
		}
		public decimal SetValue(decimal value, UnitType type)
		{
			nativeInput = value;
			coordinateSpace = type;
			nativeValue = value * Multiply(type);
			return nativeInput;
		}
		public void SetValue(decimal value, UnitType type, UnitType outUnit)
		{
//			Global.statB("{0} ? {1} ? {2}",value,type,outUnit);
			OutputUnit = outUnit;
			SetValue(value,type);
		}
		
		public decimal Pixel { get { return nativeValue / Multiply(UnitType.Pixel); } set { SetValue(value,UnitType.Pixel); } }
		public decimal Point { get { return nativeValue / Multiply(UnitType.Point); } set { SetValue(value,UnitType.Point); } }
		public decimal Pica { get { return nativeValue / Multiply(UnitType.Pica); } set { SetValue(value,UnitType.Pica); } }
		public decimal Inch { get { return nativeValue / Multiply(UnitType.Inch); } set { SetValue(value,UnitType.Inch); } }
		public decimal Cm { get { return nativeValue / Multiply(UnitType.Centimeter); } set { SetValue(value,UnitType.Centimeter); } }
		public decimal Mm { get { return nativeValue; } set { SetValue(value,UnitType.Millimeter); } }

		public UnitD(string value)
		{
			SetValue(value);
		}
		public UnitD(decimal value, UnitType type)
		{
			SetValue(value,type);
		}
		public UnitD(decimal value, UnitType type, UnitType outUnit)
		{
			SetValue(value,type,outUnit);
		}
		
		static public UnitD operator +(UnitD a, UnitD b){ return new UnitD(a.NativeValue+b.NativeValue,a.CoordinateSpace,OutputUnit); }
		static public UnitD operator -(UnitD a, UnitD b){ return new UnitD(a.NativeValue-b.NativeValue,a.CoordinateSpace,OutputUnit); }
		static public UnitD operator *(UnitD a, UnitD b){ return new UnitD(a.NativeValue*b.NativeValue,a.CoordinateSpace,OutputUnit); }
		static public UnitD operator /(UnitD a, UnitD b){ return new UnitD(a.NativeValue/b.NativeValue,a.CoordinateSpace,OutputUnit); }
		static public UnitD operator %(UnitD a, UnitD b){ return new UnitD(a.NativeValue % b.NativeValue,a.CoordinateSpace,OutputUnit); }
		static public bool operator >(UnitD a, UnitD b){ return a.NativeValue > b.NativeValue; }
		static public bool operator <(UnitD a, UnitD b){ return a.NativeValue < b.NativeValue; }
		static public UnitD operator ++(UnitD a){ return new UnitD(a.NativeValue++,a.CoordinateSpace,OutputUnit); }
		static public UnitD operator --(UnitD a){ return new UnitD(a.NativeValue--,a.CoordinateSpace,OutputUnit); }
		
		static public implicit operator decimal(UnitD a){ return a.GetValue(OutputUnit); }
		static public implicit operator UnitD(decimal a){ return new UnitD(a,UnitType.Pixel); }
		static public implicit operator UnitD(double a){ return new UnitD((decimal)a,UnitType.Pixel); }
		static public implicit operator UnitD(float a){ return new UnitD((decimal)a,UnitType.Pixel); }
		static public implicit operator UnitD(int a){ return new UnitD((decimal)a,UnitType.Pixel); }
		static public implicit operator float(UnitD a){ return (float)(a.GetValue(OutputUnit)); }
		static public implicit operator int(UnitD a){ return (int)Math.Round(a.GetValue(OutputUnit),0); }
		static public implicit operator string(UnitD a){ return a.ToString(); }
		static public implicit operator UnitD(string str) { return new UnitD(str); }
		public override string ToString()
		{
			return string.Format("{0}{1}",GetValue(OutputUnit),ConvertType(OutputUnit));
		}
	}
}
