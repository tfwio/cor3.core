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
using System.Diagnostics;
using System.Drawing.Utilities.SuperOld;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.Drawing
{

	/// <summary>
	/// A general Point class using a Double as it's numeric data type.
	/// </summary>
	public class DoublePoint
	{
		static public DoublePoint Empty { get { return new DoublePoint(0D,0D,UnitType.Pixel); } }
		static public DoublePoint One { get { return new DoublePoint(1D,1D,UnitType.Pixel); } }

		DblUnit _X = "0px", _Y = "0px";
		[XmlAttribute] public DblUnit X { get { return _X; } set { _X = value; } }
		[XmlAttribute] public DblUnit Y { get { return _Y; } set { _Y = value; } }
	
		#region Properties
		[XmlIgnore] public DblUnit Bigger { get { return (X >= Y)? X: Y; } }
		[XmlIgnore] public bool IsLand { get { return X >= Y; } }
		/// <summary>zerod?</summary>
		[XmlIgnore] public DblUnit Slope { get  { return Math.Sqrt(Math.Pow(X,2)+Math.Pow(Y,2)); }  }
		#endregion
		#region Static Methods
		static public DoublePoint FlattenPoint(DoublePoint _pin, bool roundUp)
		{
			DoublePoint newP = _pin.Clone();
			if (newP.X==newP.Y) return newP;
			if (_pin.X > _pin.Y) { if (roundUp) newP.Y = newP.X; else newP.X = newP.Y; }
			else { if (!roundUp) newP.Y = newP.X; else newP.X = newP.Y; }
			return newP;
		}
		static public DoublePoint FlattenPoint(DoublePoint _pin) { return FlattenPoint(_pin,false); }
		/// <summary>same as FlattenPoint overload without boolean</summary>
		static public DoublePoint FlattenDown(DoublePoint _pin) { return FlattenPoint(_pin); }
		static public DoublePoint FlattenUp(DoublePoint _pin) { return FlattenPoint(_pin,true); }
		#endregion
		#region Helpers ?Obsolete??
		//public Unit Slope() { return Hypotenuse; }
		//public Unit Sine { get { return Y/Hypotenuse; } }
		//public Unit Cosine { get { return X/Hypotenuse; } }
		//public Unit Tangent { get { return Y/X; } }
		//public Unit SlopeRatio(XPoint cmp) { return Slope()/cmp.Slope); }
		/// <summary>Returns a new flattened point</summary>
		public DoublePoint Flat(bool roundUp) { return FlattenPoint(this,roundUp); }
		/// <summary>Flattens the calling point</summary>
		public void Flatten(bool roundUp) { DoublePoint f = Flat(roundUp); this.X = f.X; this.Y = f.Y; f = null; }
		/// <summary>use Flat or flatten calls.</summary>
		public DoublePoint ScaleTo(DoublePoint point)
		{
			if (point.X==X && point.Y==Y) throw new InvalidOperationException("you mucker");
			System.Windows.Forms.MessageBox.Show( string.Format("X: {1},Y: {0}",Y/point.Y,X/point.X) );
			if (X > point.Y)
			{
//				Global.cstat(ConsoleColor.Red,"X is BIGGER");
				Debug.Print("X is BIGGER");
			}
			else {
				Debug.Print("X is SMALLER");
//				Global.cstat(ConsoleColor.Red,"X is SMALLER");
			}
			return this;
		}
		
		void Print(ConsoleColor red, string string1)
		{
			throw new NotImplementedException();
		}
		public DoublePoint GetRation(DoublePoint dst)
		{
			return dst/this;
		}
		public DoublePoint GetScaledRation(DoublePoint dst)
		{
			return this*(dst/this);
		}
		public DblUnit Dimension() { return X*Y; }
		#endregion
		#region Help
		public DoublePoint Translate(DoublePoint offset, DoublePoint zoom)
		{
			return (this+offset)*zoom;
		}
		public DoublePoint Translate(DblUnit offset, DblUnit zoom)
		{
			return (this+new DoublePoint(offset*zoom));
		}
		#endregion
		#region Maths
		public bool IsXG(DoublePoint P) { return X>P.X; }
		public bool IsYG(DoublePoint P) { return Y>P.Y; }
		public bool IsXL(DoublePoint P) { return X<P.X; }
		public bool IsYL(DoublePoint P) { return Y<P.Y; }
	
		public bool IsLEq(DoublePoint p) { return (X<=p.X) && (Y<=p.Y); }
		public bool IsGEq(DoublePoint p) { return (X>=p.X) && (Y>=p.Y); }
		
		public bool IsXGEq(DoublePoint P) { return IsXG(P)&IsXG(P); }
		public bool IsYGEq(DoublePoint P) { return IsYG(P)&IsYG(P); }
		public bool IsXLEq(DoublePoint P) { return IsXG(P)&IsXG(P); }
		public bool IsYLEq(DoublePoint P) { return IsYG(P)&IsYG(P); }
		public bool IsXEq(DoublePoint P) { return X==P.X; }
		public bool IsYEq(DoublePoint P) { return Y==P.Y; }
	
		public DoublePoint Multiply(params DoublePoint[] P) {
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new DoublePoint(X,Y)*P[0];
			DoublePoint NewPoint = new DoublePoint(X,Y)*P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint *= P[i];
			}
			return NewPoint;
		}
		public DoublePoint Multiply(params float[] P) {
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new DoublePoint(X,Y)*P[0];
			DoublePoint NewPoint = new DoublePoint(X,Y)*P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint *= P[i];
			}
			return NewPoint;
		}
		public DoublePoint Divide(params DoublePoint[] P)
		{
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new DoublePoint(X,Y)/P[0];
			DoublePoint NewPoint = new DoublePoint(X,Y)/P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint /= P[i];
			}
			return NewPoint;
		}
		public DoublePoint MulX(params DoublePoint[] P)
		{
			DoublePoint PBase = Clone();
			foreach (DoublePoint RefPoint in P) PBase.X *= RefPoint.X;
			return PBase;
		}
		public DoublePoint MulY(params DoublePoint[] P)
		{
			DoublePoint PBase = Clone();
			foreach (DoublePoint RefPoint in P) PBase.Y *= RefPoint.Y;
			return PBase;
		}
		public DoublePoint DivX(params DoublePoint[] P)
		{
			DoublePoint PBase = Clone();
			foreach (DoublePoint RefPoint in P) PBase.X /= RefPoint.X;
			return PBase;
		}
		public DoublePoint DivY(params DoublePoint[] P)
		{
			DoublePoint PBase = Clone();
			foreach (DoublePoint RefPoint in P) PBase.Y /= RefPoint.Y;
			return PBase;
		}
		public DoublePoint AddX(params DoublePoint[] P)
		{
			DoublePoint PBase = Clone();
			foreach (DoublePoint RefPoint in P) PBase.X += RefPoint.X;
			return PBase;
		}
		public DoublePoint AddY(params DoublePoint[] P)
		{
			DoublePoint PBase = Clone();
			foreach (DoublePoint RefPoint in P) PBase.Y += RefPoint.Y;
			return PBase;
		}
		public DoublePoint AddY(params int[] P)
		{
			DoublePoint PBase = Clone();
			foreach (int RefPoint in P) PBase.Y += RefPoint;
			return PBase;
		}
		public DoublePoint NegX(params DoublePoint[] P)
		{
			DoublePoint PBase = Clone();
			foreach (DoublePoint RefPoint in P) PBase.X -= RefPoint.X;
			return PBase;
		}
		public DoublePoint NegX(params int[] P)
		{
			DoublePoint PBase = Clone();
			foreach (int Ref in P) PBase.X -= Ref;
			return PBase;
		}
		public DoublePoint NegY(params DoublePoint[] P)
		{
			DoublePoint PBase = Clone();
			foreach (DoublePoint RefPoint in P) PBase.Y -= RefPoint.Y;
			return PBase;
		}
		public DoublePoint NegY(params int[] P)
		{
			DoublePoint PBase = Clone();
			foreach (int Ref in P) PBase.Y -= Ref;
			return PBase;
		}
		#endregion
		#region Static Methods
		static public DoublePoint Average(params DoublePoint[] xp)
		{
			DoublePoint p = new DoublePoint(0);
			foreach (DoublePoint pt in xp) p += pt;
			return p/xp.Length;
		}
		static public DoublePoint GetClientSize(Control ctl) { return ctl.ClientSize; }
		static public DoublePoint GetPaddingTopLeft(Padding pad) { return new DoublePoint(pad.Left,pad.Top); }
		static public DoublePoint GetPaddingOffset(Padding pad) { return new DoublePoint(pad.Left+pad.Right,pad.Top+pad.Bottom); }
		// =======================================================
		static public DoublePoint Angus(float offset, float ration, float phase) { return new DoublePoint(-Math.Sin(cvtf(ration,offset+phase)),Math.Cos(cvtf(ration,offset+phase))); }
		static public DoublePoint Angus(float offset, float ration) { return Angus(offset,ration,0.0f); }
		static float cvtf(float S, float I){ return (float)((Math.PI*2)*(I/S)); }
		// =======================================================
		/// ? AutoScale ? multiplies agains largest point in ?dest / source?
		static public DoublePoint Fit(DoublePoint dest, DoublePoint source)
		{
			return Fit(dest,source,scaleFlags.autoScale);
		}
		/// ? AutoScale ? Multiplies against largest source size: ( ( source.X | source.Y ) * ( dest / source.X | source.Y ) )<br/>?
		/// ScaleWidth ( dest * source.X )
		static public DoublePoint Fit(DoublePoint dest, DoublePoint source, scaleFlags sf)
		{
			DoublePoint HX = dest/source;
			if (sf== scaleFlags.autoScale) return (HX.Y > HX.X) ? source*HX.X : source * HX.Y;
			else return (sf== scaleFlags.sWidth) ? source*HX.X : source*HX.Y;
		}
		
		#endregion
		#region Operators
		static public DoublePoint operator +(DoublePoint a, DoublePoint b){ return new DoublePoint(a.X+b.X,a.Y+b.Y); }
		static public DoublePoint operator +(DoublePoint a, Point b){ return new DoublePoint(a.X+b.X,a.Y+b.Y); }
		static public DoublePoint operator +(DoublePoint a, int b){ return new DoublePoint(a.X+b,a.Y+b); }
		static public DoublePoint operator +(DoublePoint a, float b){ return new DoublePoint(a.X+b,a.Y+b); }
		static public DoublePoint operator +(DoublePoint a, DblUnit b){ return new DoublePoint(a.X+b,a.Y+b); }
		static public DoublePoint operator -(DoublePoint a){ return new DoublePoint(-a.X,-a.Y); }
		static public DoublePoint operator -(DoublePoint a, DoublePoint b){ return new DoublePoint(a.X-b.X,a.Y-b.Y); }
		static public DoublePoint operator -(DoublePoint a, Point b){ return new DoublePoint(a.X-b.X,a.Y-b.Y); }
		static public DoublePoint operator -(DoublePoint a, int b){ return new DoublePoint(a.X-b,a.Y-b); }
		static public DoublePoint operator -(DoublePoint a, float b){ return new DoublePoint(a.X-b,a.Y-b); }
		static public DoublePoint operator -(DoublePoint a, DblUnit b){ return new DoublePoint(a.X-b,a.Y-b); }
		static public DoublePoint operator /(DoublePoint a, DoublePoint b){ return new DoublePoint(a.X/b.X,a.Y/b.Y); }
		static public DoublePoint operator /(DoublePoint a, Point b){ return new DoublePoint(a.X/b.X,a.Y/b.Y); }
		static public DoublePoint operator /(DoublePoint a, int b){ return new DoublePoint(a.X/b,a.Y/b); }
		static public DoublePoint operator /(DoublePoint a, float b){ return new DoublePoint(a.X/b,a.Y/b); }
		static public DoublePoint operator /(DoublePoint a, DblUnit b){ return new DoublePoint(a.X/b,a.Y/b); }
		static public DoublePoint operator *(DoublePoint a, DoublePoint b){ return new DoublePoint(a.X*b.X,a.Y*b.Y); }
		static public DoublePoint operator *(DoublePoint a, Point b){ return new DoublePoint(a.X*b.X,a.Y*b.Y); }
		static public DoublePoint operator *(DoublePoint a, int b){ return new DoublePoint(a.X*b,a.Y*b); }
		static public DoublePoint operator *(DoublePoint a, float b){ return new DoublePoint(a.X*b,a.Y*b); }
		static public DoublePoint operator *(DoublePoint a, DblUnit b){ return new DoublePoint(a.X*(float)b,a.Y*(float)b); }
		static public DoublePoint operator %(DoublePoint a, DoublePoint b){ return new DoublePoint(a.X%b.X,a.Y%b.Y); }
		static public DoublePoint operator %(DoublePoint a, Point b){ return new DoublePoint(a.X%b.X,a.Y%b.Y); }
		static public DoublePoint operator %(DoublePoint a, int b){ return new DoublePoint(a.X % b,a.Y % b); }
		static public DoublePoint operator %(DoublePoint a, float b){ return new DoublePoint(a.X % b,a.Y % b); }
		static public DoublePoint operator %(DoublePoint a, DblUnit b){ return new DoublePoint(a.X % b,a.Y % b); }
		static public DoublePoint operator ++(DoublePoint a){ return new DoublePoint(a.X++,a.Y++); }
		static public DoublePoint operator --(DoublePoint a){ return new DoublePoint(a.X--,a.Y--); }
		static public bool operator >(DoublePoint a,DoublePoint b){ return ((a.X>b.X) & (a.Y>b.Y)); }
		static public bool operator <(DoublePoint a,DoublePoint b){ return ((a.X<b.X) & (a.Y<b.Y)); }
		#endregion
		#region Operators Implicit
		static public implicit operator Point(DoublePoint a){ return new Point((int)a.X,(int)a.Y); }
		static public implicit operator PointF(DoublePoint a){ return new PointF((float)a.X,(float)a.Y); }
		static public implicit operator Size(DoublePoint a){ return new Size((int)a.X,(int)a.Y); }
		static public implicit operator SizeF(DoublePoint a){ return new SizeF((float)a.X,(float)a.Y); }
		static public implicit operator DoublePoint(Size s){ return new DoublePoint(s); }
		static public implicit operator DoublePoint(SizeF s){ return new DoublePoint(s); }
		static public implicit operator DoublePoint(Point s){ return new DoublePoint(s); }
		static public implicit operator DoublePoint(PointF s){ return new DoublePoint(s); }
		#endregion
		
		public DoublePoint(){ }
		public DoublePoint(double x, double y){ _X = x; _Y = y; }
		public DoublePoint(DblUnit x, DblUnit y, UnitType type){ _X = new DblUnit(x,type); _Y = new DblUnit(y,type); }
		public DoublePoint(int value) : this(value,value) {  }
		public DoublePoint(long value) : this(value,value) {  }
		public DoublePoint(float value) : this((DblUnit)value,(DblUnit)value) {  }
		public DoublePoint(DblUnit value) : this(value,value) {  }
		public DoublePoint(FloatPoint value) : this(value.X,value.Y) {  }
		public DoublePoint(Point P){ _X = P.X; _Y = P.Y; }
		public DoublePoint(PointF P){ _X = P.X; _Y = P.Y; }
		public DoublePoint(Size P){ _X = P.Width; _Y = P.Height; }
		public DoublePoint(SizeF P){ _X = P.Width; _Y = P.Height; }
	
		#region Object
		public DoublePoint Clone(){ return new DoublePoint(X,Y); }
		public void CopyPoint(DoublePoint inPoint) { X=inPoint.X; Y=inPoint.Y; }
		public override string ToString() { return String.Format("XPoint:X:{0},Y:{1}",X,Y); }
		#endregion
		
	}
}
