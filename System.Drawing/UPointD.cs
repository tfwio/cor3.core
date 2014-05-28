/* oOo * 11/28/2007 : 5:29 PM */
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
using System.Cor3.Drawing;
using System.Drawing.Utilities.SuperOld;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.Drawing
{
	//
	public struct UPointD
	{
		static public UnitType GlobalUnits = UnitType.Pixel;
		static public UPointD Empty { get { return new UPointD(0m,0m,GlobalUnits); } }
		static public UPointD One { get { return new UPointD(1m,1m,GlobalUnits); } }
	
		UnitD _X, _Y;
		[XmlAttribute] public UnitD X { get { return _X; } set { _X = value; } }
		[XmlAttribute] public UnitD Y { get { return _Y; } set { _Y = value; } }
	
		#region Properties
		[XmlIgnore] public UnitD Bigger { get { return (X >= Y)? X: Y; } }
		[XmlIgnore] public bool IsLand { get { return X >= Y; } }
		/// <summary>zerod?</summary>
		[XmlIgnore] public UnitD Slope { get  { return (decimal)Math.Sqrt(Math.Pow((double)X,2)+Math.Pow((double)Y,2)); }  }
		#endregion
		#region Static Methods
		static public UPointD FlattenPoint(UPointD _pin, bool roundUp)
		{
			UPointD newP = _pin.Clone();
			if (newP.X==newP.Y) return newP;
			if (_pin.X > _pin.Y) { if (roundUp) newP.Y = newP.X; else newP.X = newP.Y; }
			else { if (!roundUp) newP.Y = newP.X; else newP.X = newP.Y; }
			return newP;
		}
		static public UPointD FlattenPoint(UPointD _pin) { return FlattenPoint(_pin,false); }
		/// <summary>same as FlattenPoint overload without boolean</summary>
		static public UPointD FlattenDown(UPointD _pin) { return FlattenPoint(_pin); }
		static public UPointD FlattenUp(UPointD _pin) { return FlattenPoint(_pin,true); }
		#endregion
		#region Helpers ?Obsolete??
		//public Unit Slope() { return Hypotenuse; }
		//public Unit Sine { get { return Y/Hypotenuse; } }
		//public Unit Cosine { get { return X/Hypotenuse; } }
		//public Unit Tangent { get { return Y/X; } }
		//public Unit SlopeRatio(XPoint cmp) { return Slope()/cmp.Slope); }
		/// <summary>Returns a new flattened point</summary>
		public UPointD Flat(bool roundUp) { return FlattenPoint(this,roundUp); }
		/// <summary>Flattens the calling point</summary>
		public void Flatten(bool roundUp) { UPointD? f = Flat(roundUp); this.X = f.Value.X; this.Y = f.Value.Y; f = null; }
		
		
		/// <summary>use Flat or flatten calls.</summary>
		public UPointD ScaleTo(UPointD point)
		{
			if (point.X==X && point.Y==Y) throw new InvalidOperationException("you mucker");
			System.Windows.Forms.MessageBox.Show( string.Format("X: {1},Y: {0}",Y/point.Y,X/point.X) );
			if (X > point.Y)
			{
//				Global.cstat(ConsoleColor.Red,"X is BIGGER");
			}
//			else Global.cstat(ConsoleColor.Red,"X is SMALLER");
			return this;
		}
		public UPointD GetRation(UPointD dst)
		{
			return dst/this;
		}
		public UPointD GetScaledRation(UPointD dst)
		{
			return this*(dst/this);
		}
		public UnitD Dimension() { return X*Y; }
		#endregion
		#region Help
		public UPointD Translate(UPointD offset, UPointD zoom)
		{
			return (this+offset)*zoom;
		}
		public UPointD Translate(UnitD offset, UnitD zoom)
		{
			return (this+new UPointD(offset*zoom));
		}
		#endregion
		#region Maths
		public bool IsXG(UPointD P) { return X>P.X; }
		public bool IsYG(UPointD P) { return Y>P.Y; }
		public bool IsXL(UPointD P) { return X<P.X; }
		public bool IsYL(UPointD P) { return Y<P.Y; }
	
		public bool IsLEq(UPointD p) { return (X<=p.X) && (Y<=p.Y); }
		public bool IsGEq(UPointD p) { return (X>=p.X) && (Y>=p.Y); }
		
		public bool IsXGEq(UPointD P) { return IsXG(P)&IsXG(P); }
		public bool IsYGEq(UPointD P) { return IsYG(P)&IsYG(P); }
		public bool IsXLEq(UPointD P) { return IsXG(P)&IsXG(P); }
		public bool IsYLEq(UPointD P) { return IsYG(P)&IsYG(P); }
		public bool IsXEq(UPointD P) { return X==P.X; }
		public bool IsYEq(UPointD P) { return Y==P.Y; }
	
		public UPointD Multiply(params UPointD[] P) {
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new UPointD(X,Y)*P[0];
			UPointD NewPoint = new UPointD(X,Y)*P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint *= P[i];
			}
			return NewPoint;
		}
		public UPointD Multiply(params float[] P) {
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new UPointD(X,Y)*P[0];
			UPointD NewPoint = new UPointD(X,Y)*P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint *= P[i];
			}
			return NewPoint;
		}
		public UPointD Divide(params UPointD[] P)
		{
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new UPointD(X,Y)/P[0];
			UPointD NewPoint = new UPointD(X,Y)/P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint /= P[i];
			}
			return NewPoint;
		}
		public UPointD MulX(params UPointD[] P)
		{
			UPointD PBase = Clone();
			foreach (UPointD RefPoint in P) PBase.X *= RefPoint.X;
			return PBase;
		}
		public UPointD MulY(params UPointD[] P)
		{
			UPointD PBase = Clone();
			foreach (UPointD RefPoint in P) PBase.Y *= RefPoint.Y;
			return PBase;
		}
		public UPointD DivX(params UPointD[] P)
		{
			UPointD PBase = Clone();
			foreach (UPointD RefPoint in P) PBase.X /= RefPoint.X;
			return PBase;
		}
		public UPointD DivY(params UPointD[] P)
		{
			UPointD PBase = Clone();
			foreach (UPointD RefPoint in P) PBase.Y /= RefPoint.Y;
			return PBase;
		}
		public UPointD AddX(params UPointD[] P)
		{
			UPointD PBase = Clone();
			foreach (UPointD RefPoint in P) PBase.X += RefPoint.X;
			return PBase;
		}
		public UPointD AddY(params UPointD[] P)
		{
			UPointD PBase = Clone();
			foreach (UPointD RefPoint in P) PBase.Y += RefPoint.Y;
			return PBase;
		}
		public UPointD AddY(params int[] P)
		{
			UPointD PBase = Clone();
			foreach (int RefPoint in P) PBase.Y += RefPoint;
			return PBase;
		}
		public UPointD NegX(params UPointD[] P)
		{
			UPointD PBase = Clone();
			foreach (UPointD RefPoint in P) PBase.X -= RefPoint.X;
			return PBase;
		}
		public UPointD NegX(params int[] P)
		{
			UPointD PBase = Clone();
			foreach (int Ref in P) PBase.X -= Ref;
			return PBase;
		}
		public UPointD NegY(params UPointD[] P)
		{
			UPointD PBase = Clone();
			foreach (UPointD RefPoint in P) PBase.Y -= RefPoint.Y;
			return PBase;
		}
		public UPointD NegY(params int[] P)
		{
			UPointD PBase = Clone();
			foreach (int Ref in P) PBase.Y -= Ref;
			return PBase;
		}
		#endregion
		#region Static Methods
		static public UPointD Average(params UPointD[] xp)
		{
			UPointD p = new UPointD(0);
			foreach (UPointD pt in xp) p += pt;
			return p/xp.Length;
		}
		static public UPointD GetClientSize(Control ctl) { return ctl.ClientSize; }
		static public UPointD GetPaddingTopLeft(Padding pad) { return new UPointD(pad.Left,pad.Top); }
		static public UPointD GetPaddingOffset(Padding pad) { return new UPointD(pad.Left+pad.Right,pad.Top+pad.Bottom); }
		// =======================================================
		static public UPointD Angus(float offset, float ration, float phase) { return new UPointD((decimal)-Math.Sin(cvtf(ration,offset+phase)),(decimal)Math.Cos(cvtf(ration,offset+phase))); }
		static public UPointD Angus(float offset, float ration) { return Angus(offset,ration,0.0f); }
		static float cvtf(float S, float I){ return (float)((Math.PI*2)*(I/S)); }
		// =======================================================
		/// ? AutoScale ? multiplies agains largest point in ?dest / source?
		static public UPointD Fit(UPointD dest, UPointD source)
		{
			return Fit(dest,source,scaleFlags.autoScale);
		}
		/// ? AutoScale ? Multiplies against largest source size: ( ( source.X | source.Y ) * ( dest / source.X | source.Y ) )<br/>?
		/// ScaleWidth ( dest * source.X )
		static public UPointD Fit(UPointD dest, UPointD source, scaleFlags sf)
		{
			UPointD HX = dest/source;
			if (sf== scaleFlags.autoScale) return (HX.Y > HX.X) ? source*HX.X : source * HX.Y;
			else return (sf== scaleFlags.sWidth) ? source*HX.X : source*HX.Y;
		}
		
		#endregion
		#region Operators
		static public UPointD operator +(UPointD a, UPointD b){ return new UPointD(a.X+b.X,a.Y+b.Y); }
		static public UPointD operator +(UPointD a, Point b){ return new UPointD(a.X+b.X,a.Y+b.Y); }
		static public UPointD operator +(UPointD a, int b){ return new UPointD(a.X+b,a.Y+b); }
		static public UPointD operator +(UPointD a, float b){ return new UPointD(a.X+b,a.Y+b); }
		static public UPointD operator +(UPointD a, UnitD b){ return new UPointD(a.X+b,a.Y+b); }
		static public UPointD operator -(UPointD a){ return new UPointD(-a.X,-a.Y); }
		static public UPointD operator -(UPointD a, UPointD b){ return new UPointD(a.X-b.X,a.Y-b.Y); }
		static public UPointD operator -(UPointD a, Point b){ return new UPointD(a.X-b.X,a.Y-b.Y); }
		static public UPointD operator -(UPointD a, int b){ return new UPointD(a.X-b,a.Y-b); }
		static public UPointD operator -(UPointD a, float b){ return new UPointD(a.X-b,a.Y-b); }
		static public UPointD operator -(UPointD a, UnitD b){ return new UPointD(a.X-b,a.Y-b); }
		static public UPointD operator /(UPointD a, UPointD b){ return new UPointD(a.X/b.X,a.Y/b.Y); }
		static public UPointD operator /(UPointD a, Point b){ return new UPointD(a.X/b.X,a.Y/b.Y); }
		static public UPointD operator /(UPointD a, int b){ return new UPointD(a.X/b,a.Y/b); }
		static public UPointD operator /(UPointD a, float b){ return new UPointD(a.X/b,a.Y/b); }
		static public UPointD operator /(UPointD a, UnitD b){ return new UPointD(a.X/b,a.Y/b); }
		static public UPointD operator *(UPointD a, UPointD b){ return new UPointD(a.X*b.X,a.Y*b.Y); }
		static public UPointD operator *(UPointD a, Point b){ return new UPointD(a.X*b.X,a.Y*b.Y); }
		static public UPointD operator *(UPointD a, int b){ return new UPointD(a.X*b,a.Y*b); }
		static public UPointD operator *(UPointD a, float b){ return new UPointD(a.X*b,a.Y*b); }
		static public UPointD operator *(UPointD a, UnitD b){ return new UPointD(a.X*(float)b,a.Y*(float)b); }
		static public UPointD operator %(UPointD a, UPointD b){ return new UPointD(a.X%b.X,a.Y%b.Y); }
		static public UPointD operator %(UPointD a, Point b){ return new UPointD(a.X%b.X,a.Y%b.Y); }
		static public UPointD operator %(UPointD a, int b){ return new UPointD(a.X % b,a.Y % b); }
		static public UPointD operator %(UPointD a, float b){ return new UPointD(a.X % b,a.Y % b); }
		static public UPointD operator %(UPointD a, UnitD b){ return new UPointD(a.X % b,a.Y % b); }
		static public UPointD operator ++(UPointD a){ return new UPointD(a.X++,a.Y++); }
		static public UPointD operator --(UPointD a){ return new UPointD(a.X--,a.Y--); }
		static public bool operator >(UPointD a,UPointD b){ return ((a.X>b.X) & (a.Y>b.Y)); }
		static public bool operator <(UPointD a,UPointD b){ return ((a.X<b.X) & (a.Y<b.Y)); }
		#endregion
		#region Operators Implicit
//		static public implicit operator Point(UPointD a){ return new Point((int)a.X.Pixel,(int)a.Y.Pixel); }
//		static public implicit operator PointF(UPointD a){ return new PointF((float)a.X.Pixel,(float)a.Y.Pixel); }
		static public implicit operator Size(UPointD a){ return new Size((int)a.X.Pixel,(int)a.Y.Pixel); }
		static public implicit operator SizeF(UPointD a){ return new SizeF((float)a.X.GetValue(UPointD.GlobalUnits),(float)a.Y.GetValue(UPointD.GlobalUnits)); }
		static public implicit operator UPointD(Size s){ return new UPointD(s); }
		static public implicit operator UPointD(SizeF s){ return new UPointD(s); }
		static public implicit operator DPoint(UPointD d)
		{
			return new DPoint(
				(double)d.X.GetValue(GlobalUnits),
				(double)d.Y.GetValue(GlobalUnits));
		}
		static public implicit operator UPointD(DPoint d)
		{
			return new UPointD(d.X,d.Y,UPointD.GlobalUnits);
		}
//		static public implicit operator UPointD(Point s){ return new UPointD(s); }
//		static public implicit operator UPointD(PointF s){ return new UPointD(s); }
		#endregion
		
		public UPointD(UnitD x, UnitD y){ _X = x; _Y = y; }
//		public UPointD(decimal x, decimal y){ _X = x; _Y = y; }
		public UPointD(UnitD x, UnitD y, UnitType type){ _X = new UnitD(x,type); _Y = new UnitD(y,type); }
//		public UPointD(int value) : this(value,value) {  }
//		public UPointD(long value) : this(value,value) {  }
//		public UPointD(float value) : this((UnitD)value,(UnitD)value) {  }
		public UPointD(UnitD value) : this(value,value) {  }
		public UPointD(FloatPoint value) : this((decimal)value.X,(decimal)value.Y) {  }
		public UPointD(Point P){ _X = P.X; _Y = P.Y; }
		public UPointD(PointF P){ _X = P.X; _Y = P.Y; }
		public UPointD(Size P){ _X = P.Width; _Y = P.Height; }
		public UPointD(SizeF P){ _X = P.Width; _Y = P.Height; }
		/// <summary>
		/// Using the value of GlobalUnits
		/// </summary>
		public FloatPoint FPoint { get { return new FloatPoint((float)X.GetValue(GlobalUnits),(float)Y.GetValue(GlobalUnits)); } }
		public DPoint DPoint { get { return new DPoint((double)X,(double)Y); } }
		static public PointF[] GetPoints(params UPointD[] upoints)
		{
			System.Collections.Generic.List<PointF> list = new System.Collections.Generic.List<PointF>();
			foreach (UPointD d in upoints)
			{
				list.Add(d.FPoint);
			}
			PointF[] array = list.ToArray();
			list.Clear();
			return array;
		}
		
		#region Object
		public UPointD Clone(){ return new UPointD(X,Y); }
		public void CopyPoint(UPointD inPoint) { X=inPoint.X; Y=inPoint.Y; }
		public override string ToString() { return String.Format("XPoint:X:{0},Y:{1}",X,Y); }
		#endregion
		
	}
}
