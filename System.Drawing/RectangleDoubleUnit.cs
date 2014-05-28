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
/* oOo * 11/28/2007 : 5:29 PM */
/* THIS CALSS HAS NOT YET FULLY BEEN TESTED */
using System;
using System.Cor3.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.Drawing
{
	#region “ RectangleDoubleUnit ”
	public class RectangleDoubleUnit {
		static public RectangleDoubleUnit Zero { get { return new RectangleDoubleUnit(0,0,0,0); } }
		////////////////////////////////////////////////////////////////////////
		//
		[XmlIgnore] public UPointD Location = UPointD.Empty;
		[XmlIgnore] public UPointD Size = UPointD.Empty;
		////////////////////////////////////////////////////////////////////////
		//
		[XmlIgnore] public UPointD Center { get { return Size*0.5f; } }
		[XmlIgnore] public UPointD BottomRight { get { return Location+Size; } }
		////////////////////////////////////////////////////////////////////////
		//
		[XmlAttribute] public UnitD X { get { return Location.X; } set { Location.X=value; } }
		[XmlAttribute] public UnitD Y { get { return Location.Y; } set { Location.Y=value; } }
		/// <summary> (read only) </summary>
		[XmlIgnore] public UnitD Top { get { return Location.Y; } /*set { Location.Y = value; }*/ }
		/// <summary> (read only) </summary>
		[XmlIgnore] public UnitD Bottom { get { return Location.Y + Size.Y; } /*set { Size.Y = value-Location.Y; }*/ }
		[XmlIgnore] public UnitD Left { get { return Location.X; } set { Location.X = value; } }
		/// <summary> (read only) </summary>
		[XmlIgnore] public UnitD Right { get { return (Location.AddX(Size)).X; } /*set { Size.X = value-Location.X; }*/ }
		////////////////////////////////////////////////////////////////////////
		/// <summary> (read only) </summary>
		[XmlAttribute] public UnitD Width { get {return Size.X; } set { Size.X = value; } }
		[XmlAttribute] public UnitD Height { get { return Size.Y; } set { Size.Y = value; } }
		////////////////////////////////////////////////////////////////////////
		//	operator
		#region Standard +,-,*,/,++,--
		static public RectangleDoubleUnit operator +(RectangleDoubleUnit a, RectangleDoubleUnit b) { return new RectangleDoubleUnit(a.X+b.X,a.Y+b.Y,a.Width+b.Width,a.Height+b.Height); }
		static public RectangleDoubleUnit operator -(RectangleDoubleUnit a, RectangleDoubleUnit b){ return new RectangleDoubleUnit(a.X-b.X,a.Y-b.Y,a.Width-b.Width,a.Height-b.Height); }
		static public RectangleDoubleUnit operator /(RectangleDoubleUnit a, RectangleDoubleUnit b){ return new RectangleDoubleUnit(a.X/b.X,a.Y/b.Y,a.Width/b.Width,a.Height/b.Height); }
		static public RectangleDoubleUnit operator *(RectangleDoubleUnit a, RectangleDoubleUnit b){ return new RectangleDoubleUnit(a.X*b.X,a.Y*b.Y,a.Width*b.Width,a.Height*b.Height); }
		static public RectangleDoubleUnit operator %(RectangleDoubleUnit a, RectangleDoubleUnit b){ return new RectangleDoubleUnit(a.X%b.X,a.Y%b.Y,a.Width%b.Width,a.Height%b.Height); }
		static public RectangleDoubleUnit operator ++(RectangleDoubleUnit a) { return new RectangleDoubleUnit(a.X++,a.Y++,a.Width++,a.Height++); }
		static public RectangleDoubleUnit operator --(RectangleDoubleUnit a) { return new RectangleDoubleUnit(a.X--,a.Y--,a.Width--,a.Height--); }
		#endregion
		#region implicit operator Point,PointF
		static public implicit operator Rectangle(RectangleDoubleUnit a){ return new Rectangle((int)a.X,(int)a.Y,(int)a.Width,(int)a.Height); }
		static public implicit operator RectangleF(RectangleDoubleUnit a){ return new RectangleF(a.X,a.Y,a.Width,a.Height); }
		static public implicit operator Padding(RectangleDoubleUnit a){ return new Padding((int)a.X,(int)a.Y,(int)a.Width,(int)a.Height); }
		static public implicit operator RectangleDoubleUnit(Rectangle a){ return new RectangleDoubleUnit(a.X,a.Y,a.Right,a.Bottom); }
		static public implicit operator RectangleDoubleUnit(RectangleF a){ return new RectangleDoubleUnit(a.X,a.Y,a.Right,a.Bottom); }
		#endregion
		public RectangleDoubleUnit Clone(){ return new RectangleDoubleUnit(); }
		///	static FromControl Methods (relative to the control)
		static public RectangleDoubleUnit FromClientInfo(DoublePoint ClientSize, Padding pad){ return new RectangleDoubleUnit(DoublePoint.GetPaddingTopLeft(pad),ClientSize-DoublePoint.GetPaddingOffset(pad)); }
		///	static FromControl Methods (relative to the control)
		static public RectangleDoubleUnit FromControl(Control ctl, bool usepadding){ return FromControl(ctl,(usepadding)?ctl.Padding:Padding.Empty); }
		///	static FromControl Methods (relative to the control)
		static public RectangleDoubleUnit FromControl(Control ctl, Padding pad){ return new RectangleDoubleUnit(DoublePoint.GetPaddingTopLeft(pad),DoublePoint.GetClientSize(ctl)-DoublePoint.GetPaddingOffset(pad)); }
		/// <para>? p.Top,p.Right,p.Bottom,p.Left</para>
		static public RectangleDoubleUnit FromPadding(Padding p) { return new RectangleDoubleUnit(p.Left,p.Top,p.Right,p.Bottom); }
		//
		public RectangleDoubleUnit() {}
		public RectangleDoubleUnit(UnitD x, UnitD y, UnitD width, UnitD height) { Location = new UPointD(x,y); Size = new UPointD(width,height); }
		public RectangleDoubleUnit(int x, int y, int width, int height) : this((UnitD)x,(UnitD)y,(UnitD)width,(UnitD)height) {}
		public RectangleDoubleUnit(DoublePoint L, DoublePoint S) : this(L.X,L.Y,S.X,S.Y) {}
		public RectangleDoubleUnit(Rectangle R) : this(R.X,R.Y,R.Width,R.Height) { }
		public RectangleDoubleUnit(UnitD num) : this(num,num,num,num) { }
		public RectangleDoubleUnit(PointF Loc, SizeF Siz) : this(Loc.X,Loc.Y,Siz.Width,Siz.Height) {}
		
		public override bool Equals(object obj)
		{
			return obj.ToString()==ToString();
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public override string ToString()
		{
			return string.Format("FloatRect: X:{0},Y:{1},Width:{2},Height:{3}", X,Y,Width,Height);
		}
	}
	#endregion
}
