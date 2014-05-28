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
/* oOo * 12/14/2007 : 10:53 AM */
using System;
using System.ComponentModel;

namespace System.Drawing
{
	public class HGradient
	{
		public enum Alignment { Horizontal,Vertical }
		static Color def_color0 = Color.Black, def_color1 = Color.White;
		const float def_spread_distance = 12;
		const float def_spread_offset = 0; // probably not be used yet

		public Color Gra0, Gra1;
		public Alignment Align;
		public float Spread
		{
			get { return (Align==Alignment.Horizontal)?this.hpSpread.X:this.hpSpread.Y;}
			set { hpSpread=(Align==Alignment.Horizontal)?new FloatPoint(value,0):new FloatPoint(0,value); }
		}

		FloatPoint hpSpread;
		[TypeConverter(typeof(ExpandableObjectConverter))]
		/// <summary>
		/// note: the point is scaled down by a value of 'new Point(-1,-1)' so
		/// that drawing can happen within borders as this is designated for
		/// drawing backgrounds within the borders of a control.
		/// I expect to have a control using this to perhaps utilize a padding factor.
		/// </summary>
		public FloatPoint HSpread { get { return hpSpread; } set { hpSpread=value-1; } }
		/// <summary>Default alignment is Alignment.Horizontal</summary>
		public HGradient() : this(def_color0,def_color1,Alignment.Horizontal,def_spread_distance) { }
		public HGradient(Alignment align) : this(def_color0,def_color1,align,40f) { }
		public HGradient(Color g0, Color g1, Alignment align, FloatPoint spread) { Gra0=g0; Gra1=g1; Align=Alignment.Horizontal; hpSpread=spread; }
		public HGradient(Color g0, Color g1, Alignment align, float spread) { Gra0=g0; Gra1=g1; Align=Alignment.Horizontal; Spread=spread; }
		public Brush LinearGradientBrush { get { return Gradients.GetLinearGradient(FloatPoint.One,hpSpread,Gra0,Gra1);} }
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public Brush GradientBrush { get { return (Brush)Gradients.GetLinearGradient(FloatPoint.One,hpSpread,Gra0,Gra1);} }
	}
}
