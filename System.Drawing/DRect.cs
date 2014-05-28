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
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.Drawing
{
	#region “ DRect ”
	public class DRect {
		static public DRect Zero { get { return new DRect(0,0,0,0); } }
		////////////////////////////////////////////////////////////////////////
		//
		[XmlIgnore] public DPoint Location = DPoint.Empty;
		[XmlIgnore] public DPoint Size = DPoint.Empty;
		////////////////////////////////////////////////////////////////////////
		//
		[XmlIgnore] public DPoint Center { get { return Size*0.5f; } }
		[XmlIgnore] public DPoint BottomRight { get { return Location+Size; } }
		////////////////////////////////////////////////////////////////////////
		//
		[XmlAttribute] public double X { get { return Location.X; } set { Location.X=value; } }
		[XmlAttribute] public double Y { get { return Location.Y; } set { Location.Y=value; } }
		/// <summary> (read only) </summary>
		[XmlIgnore] public double Top { get { return Location.Y; } /*set { Location.Y = value; }*/ }
		/// <summary> (read only) </summary>
		[XmlIgnore] public double Bottom { get { return Location.Y + Size.Y; } /*set { Size.Y = value-Location.Y; }*/ }
		[XmlIgnore] public double Left { get { return Location.X; } set { Location.X = value; } }
		/// <summary> (read only) </summary>
		[XmlIgnore] public double Right { get { return (Location.AddX(Size)).X; } /*set { Size.X = value-Location.X; }*/ }
		////////////////////////////////////////////////////////////////////////
		/// <summary> (read only) </summary>
		[XmlAttribute] public double Width { get {return Size.X; } set { Size.X = value; } }
		[XmlAttribute] public double Height { get { return Size.Y; } set { Size.Y = value; } }
		////////////////////////////////////////////////////////////////////////
		//	operator
		#region Standard +,-,*,/,++,--
		static public DRect operator +(DRect a, DRect b) { return new DRect(a.X+b.X,a.Y+b.Y,a.Width+b.Width,a.Height+b.Height); }
		static public DRect operator -(DRect a, DRect b){ return new DRect(a.X-b.X,a.Y-b.Y,a.Width-b.Width,a.Height-b.Height); }
		static public DRect operator /(DRect a, DRect b){ return new DRect(a.X/b.X,a.Y/b.Y,a.Width/b.Width,a.Height/b.Height); }
		static public DRect operator *(DRect a, DRect b){ return new DRect(a.X*b.X,a.Y*b.Y,a.Width*b.Width,a.Height*b.Height); }
		static public DRect operator %(DRect a, DRect b){ return new DRect(a.X%b.X,a.Y%b.Y,a.Width%b.Width,a.Height%b.Height); }
		static public DRect operator ++(DRect a) { return new DRect(a.X++,a.Y++,a.Width++,a.Height++); }
		static public DRect operator --(DRect a) { return new DRect(a.X--,a.Y--,a.Width--,a.Height--); }
		#endregion
		#region implicit operator Point,PointF
		static public implicit operator Rectangle(DRect a){ return new Rectangle((int)a.X,(int)a.Y,(int)a.Width,(int)a.Height); }
		static public implicit operator RectangleF(DRect a){ return new RectangleF((float)a.X,(float)a.Y,(float)a.Width,(float)a.Height); }
		static public implicit operator Padding(DRect a){ return new Padding((int)a.X,(int)a.Y,(int)a.Width,(int)a.Height); }
		static public implicit operator DRect(Rectangle a){ return new DRect(a.X,a.Y,a.Right,a.Bottom); }
		static public implicit operator DRect(RectangleF a){ return new DRect(a.X,a.Y,a.Right,a.Bottom); }
		#endregion
		public DRect Clone(){ return new DRect(); }
		///	static FromControl Methods (relative to the control)
		static public DRect FromClientInfo(DPoint ClientSize, Padding pad){ return new DRect(DPoint.GetPaddingTopLeft(pad),ClientSize-DPoint.GetPaddingOffset(pad)); }
		///	static FromControl Methods (relative to the control)
		static public DRect FromControl(Control ctl, bool usepadding){ return FromControl(ctl,(usepadding)?ctl.Padding:Padding.Empty); }
		///	static FromControl Methods (relative to the control)
		static public DRect FromControl(Control ctl, Padding pad){ return new DRect(DPoint.GetPaddingTopLeft(pad),DPoint.GetClientSize(ctl)-DPoint.GetPaddingOffset(pad)); }
		/// <para>• p.Top,p.Right,p.Bottom,p.Left</para>
		static public DRect FromPadding(Padding p) { return new DRect(p.Left,p.Top,p.Right,p.Bottom); }
		//
		public DRect() {}
		public DRect(double x, double y, double width, double height) { Location = new DPoint(x,y); Size = new DPoint(width,height); }
		public DRect(int x, int y, int width, int height) : this((double)x,(double)y,(double)width,(double)height) {}
		public DRect(DPoint L, DPoint S) : this(L.X,L.Y,S.X,S.Y) {}
		public DRect(Rectangle R) : this(R.X,R.Y,R.Width,R.Height) { }
		public DRect(PointF Loc, SizeF Siz) : this(Loc.X,Loc.Y,Siz.Width,Siz.Height) {}
		
		public override bool Equals(object obj) { return obj.ToString()==ToString(); }
		public override int GetHashCode() { return base.GetHashCode(); }
		public override string ToString() { return string.Format("DRect: X:{0},Y:{1},Width:{2},Height:{3}", X,Y,Width,Height); }
	}
	#endregion
}
