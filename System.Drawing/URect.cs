/* oOo * 11/28/2007 : 5:29 PM */
/* THIS CALSS HAS NOT YET FULLY BEEN TESTED */
using System;
using System.Cor3.Drawing;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.Drawing
{
	#region “ URect ”
	public class URect {
		static public URect Zero { get { return new URect(0,0,0,0); } }
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
		static public URect operator +(URect a, URect b) { return new URect(a.X+b.X,a.Y+b.Y,a.Width+b.Width,a.Height+b.Height); }
		static public URect operator -(URect a, URect b){ return new URect(a.X-b.X,a.Y-b.Y,a.Width-b.Width,a.Height-b.Height); }
		static public URect operator /(URect a, URect b){ return new URect(a.X/b.X,a.Y/b.Y,a.Width/b.Width,a.Height/b.Height); }
		static public URect operator *(URect a, URect b){ return new URect(a.X*b.X,a.Y*b.Y,a.Width*b.Width,a.Height*b.Height); }
		static public URect operator %(URect a, URect b){ return new URect(a.X%b.X,a.Y%b.Y,a.Width%b.Width,a.Height%b.Height); }
		static public URect operator ++(URect a) { return new URect(a.X++,a.Y++,a.Width++,a.Height++); }
		static public URect operator --(URect a) { return new URect(a.X--,a.Y--,a.Width--,a.Height--); }
		#endregion
		#region implicit operator Point,PointF
		static public implicit operator Rectangle(URect a){ return new Rectangle((int)a.X,(int)a.Y,(int)a.Width,(int)a.Height); }
		static public implicit operator RectangleF(URect a){ return new RectangleF(a.X,a.Y,a.Width,a.Height); }
		static public implicit operator Padding(URect a){ return new Padding((int)a.X,(int)a.Y,(int)a.Width,(int)a.Height); }
		static public implicit operator URect(Rectangle a){ return new URect(a.X,a.Y,a.Right,a.Bottom); }
		static public implicit operator URect(RectangleF a){ return new URect(a.X,a.Y,a.Right,a.Bottom); }
		#endregion
		public URect Clone(){ return new URect(); }
		///	static FromControl Methods (relative to the control)
		static public URect FromClientInfo(UDblPoint ClientSize, Padding pad){ return new URect(UDblPoint.GetPaddingTopLeft(pad),ClientSize-UDblPoint.GetPaddingOffset(pad)); }
		///	static FromControl Methods (relative to the control)
		static public URect FromControl(Control ctl, bool usepadding){ return FromControl(ctl,(usepadding)?ctl.Padding:Padding.Empty); }
		///	static FromControl Methods (relative to the control)
		static public URect FromControl(Control ctl, Padding pad){ return new URect(UDblPoint.GetPaddingTopLeft(pad),UDblPoint.GetClientSize(ctl)-UDblPoint.GetPaddingOffset(pad)); }
		/// <para>? p.Top,p.Right,p.Bottom,p.Left</para>
		static public URect FromPadding(Padding p) { return new URect(p.Left,p.Top,p.Right,p.Bottom); }
		//
		public URect() {}
		public URect(UnitD x, UnitD y, UnitD width, UnitD height) { Location = new UPointD(x,y); Size = new UPointD(width,height); }
		public URect(int x, int y, int width, int height) : this((UnitD)x,(UnitD)y,(UnitD)width,(UnitD)height) {}
		public URect(UDblPoint L, UDblPoint S) : this(L.X,L.Y,S.X,S.Y) {}
		public URect(Rectangle R) : this(R.X,R.Y,R.Width,R.Height) { }
		public URect(UnitD num) : this(num,num,num,num) { }
		public URect(PointF Loc, SizeF Siz) : this(Loc.X,Loc.Y,Siz.Width,Siz.Height) {}
		
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
			return string.Format("FRect: X:{0},Y:{1},Width:{2},Height:{3}", X,Y,Width,Height);
		}
	}
	#endregion
}
