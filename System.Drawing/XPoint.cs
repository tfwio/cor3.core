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
using System.Drawing.Utilities.SuperOld;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.Drawing
{
	public class XPoint
	{
		static public XPoint Empty { get { return new XPoint(0D); } }
		static public XPoint One { get { return new XPoint(1D); } }
		
		double _X=0f, _Y=0f;
		[XmlAttribute] public double X { get { return _X; } set { _X = value; } }
		[XmlAttribute] public double Y { get { return _Y; } set { _Y = value; } }
	
		#region Properties
		[XmlIgnore] public double Bigger { get { return (X >= Y)? X: Y; } }
		[XmlIgnore] public bool IsLand { get { return X >= Y; } }
		/// <summary>zerod’</summary>
		[XmlIgnore] public double Slope { get  { return Math.Sqrt(Math.Pow(X,2)+Math.Pow(Y,2)); }  }
		#endregion
		#region Static Methods
		static public XPoint FlattenPoint(XPoint _pin, bool roundUp)
		{
			XPoint newP = _pin.Clone();
			if (newP.X==newP.Y) return newP;
			if (_pin.X > _pin.Y) { if (roundUp) newP.Y = newP.X; else newP.X = newP.Y; }
			else { if (!roundUp) newP.Y = newP.X; else newP.X = newP.Y; }
			return newP;
		}
		static public XPoint FlattenPoint(XPoint _pin) { return FlattenPoint(_pin,false); }
		/// <summary>same as FlattenPoint overload without boolean</summary>
		static public XPoint FlattenDown(XPoint _pin) { return FlattenPoint(_pin); }
		static public XPoint FlattenUp(XPoint _pin) { return FlattenPoint(_pin,true); }
		#endregion
		#region Helpers “Obsolete?”
		//public double Slope() { return Hypotenuse; }
		//public double Sine { get { return Y/Hypotenuse; } }
		//public double Cosine { get { return X/Hypotenuse; } }
		//public double Tangent { get { return Y/X; } }
		//public double SlopeRatio(XPoint cmp) { return Slope()/cmp.Slope); }
		/// <summary>Returns a new flattened point</summary>
		public XPoint Flat(bool roundUp) { return FlattenPoint(this,roundUp); }
		/// <summary>Flattens the calling point</summary>
		public void Flatten(bool roundUp) { XPoint f = Flat(roundUp); this.X = f.X; this.Y = f.Y; f = null; }
		/// <summary>use Flat or flatten calls.</summary>
		public XPoint ScaleTo(XPoint point)
		{
			if (point.X==X && point.Y==Y) throw new InvalidOperationException("you mucker");
			System.Windows.Forms.MessageBox.Show( string.Format("X: {1},Y: {0}",Y/point.Y,X/point.X) );
			if (X > point.Y)
			{
				Global.cstat(ConsoleColor.Red,"X is BIGGER");
			}
			else Global.cstat(ConsoleColor.Red,"X is SMALLER");
			return this;
		}
		public XPoint GetRation(XPoint dst)
		{
			return dst/this;
		}
		public XPoint GetScaledRation(XPoint dst)
		{
			return this*(dst/this);
		}
		public double Dimension() { return X*Y; }
		#endregion
		#region Help
		public XPoint Translate(XPoint offset, XPoint zoom)
		{
			return (this+offset)*zoom;
		}
		public XPoint Translate(double offset, double zoom)
		{
			return (this+new XPoint(offset))*new FloatPoint(zoom);
		}
		#endregion
		#region Maths
		public bool IsXG(XPoint P) { return X>P.X; }
		public bool IsYG(XPoint P) { return Y>P.Y; }
		public bool IsXL(XPoint P) { return X<P.X; }
		public bool IsYL(XPoint P) { return Y<P.Y; }
	
		public bool IsLEq(XPoint p) { return (X<=p.X) && (Y<=p.Y); }
		public bool IsGEq(XPoint p) { return (X>=p.X) && (Y>=p.Y); }
		
		public bool IsXGEq(XPoint P) { return IsXG(P)&IsXG(P); }
		public bool IsYGEq(XPoint P) { return IsYG(P)&IsYG(P); }
		public bool IsXLEq(XPoint P) { return IsXG(P)&IsXG(P); }
		public bool IsYLEq(XPoint P) { return IsYG(P)&IsYG(P); }
		public bool IsXEq(XPoint P) { return X==P.X; }
		public bool IsYEq(XPoint P) { return Y==P.Y; }
	
		public XPoint Multiply(params XPoint[] P) {
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new XPoint(X,Y)*P[0];
			XPoint NewPoint = new XPoint(X,Y)*P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint *= P[i];
			}
			return NewPoint;
		}
		public XPoint Multiply(params float[] P) {
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new XPoint(X,Y)*P[0];
			XPoint NewPoint = new XPoint(X,Y)*P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint *= P[i];
			}
			return NewPoint;
		}
		public XPoint Divide(params XPoint[] P)
		{
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new XPoint(X,Y)/P[0];
			XPoint NewPoint = new XPoint(X,Y)/P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint /= P[i];
			}
			return NewPoint;
		}
		public XPoint MulX(params XPoint[] P)
		{
			XPoint PBase = Clone();
			foreach (XPoint RefPoint in P) PBase.X *= RefPoint.X;
			return PBase;
		}
		public XPoint MulY(params XPoint[] P)
		{
			XPoint PBase = Clone();
			foreach (XPoint RefPoint in P) PBase.Y *= RefPoint.Y;
			return PBase;
		}
		public XPoint DivX(params XPoint[] P)
		{
			XPoint PBase = Clone();
			foreach (XPoint RefPoint in P) PBase.X /= RefPoint.X;
			return PBase;
		}
		public XPoint DivY(params XPoint[] P)
		{
			XPoint PBase = Clone();
			foreach (XPoint RefPoint in P) PBase.Y /= RefPoint.Y;
			return PBase;
		}
		public XPoint AddX(params XPoint[] P)
		{
			XPoint PBase = Clone();
			foreach (XPoint RefPoint in P) PBase.X += RefPoint.X;
			return PBase;
		}
		public XPoint AddY(params XPoint[] P)
		{
			XPoint PBase = Clone();
			foreach (XPoint RefPoint in P) PBase.Y += RefPoint.Y;
			return PBase;
		}
		public XPoint AddY(params int[] P)
		{
			XPoint PBase = Clone();
			foreach (int RefPoint in P) PBase.Y += RefPoint;
			return PBase;
		}
		public XPoint NegX(params XPoint[] P)
		{
			XPoint PBase = Clone();
			foreach (XPoint RefPoint in P) PBase.X -= RefPoint.X;
			return PBase;
		}
		public XPoint NegX(params int[] P)
		{
			XPoint PBase = Clone();
			foreach (int Ref in P) PBase.X -= Ref;
			return PBase;
		}
		public XPoint NegY(params XPoint[] P)
		{
			XPoint PBase = Clone();
			foreach (XPoint RefPoint in P) PBase.Y -= RefPoint.Y;
			return PBase;
		}
		public XPoint NegY(params int[] P)
		{
			XPoint PBase = Clone();
			foreach (int Ref in P) PBase.Y -= Ref;
			return PBase;
		}
		#endregion
		#region Static Methods
		static public XPoint Average(params XPoint[] xp)
		{
			XPoint p = new XPoint(0);
			foreach (XPoint pt in xp) p += pt;
			return p/xp.Length;
		}
		static public XPoint GetClientSize(Control ctl) { return ctl.ClientSize; }
		static public XPoint GetPaddingTopLeft(Padding pad) { return new XPoint(pad.Left,pad.Top); }
		static public XPoint GetPaddingOffset(Padding pad) { return new XPoint(pad.Left+pad.Right,pad.Top+pad.Bottom); }
		// =======================================================
		static public XPoint Angus(float offset, float ration, float phase) { return new XPoint(-Math.Sin(cvtf(ration,offset+phase)),Math.Cos(cvtf(ration,offset+phase))); }
		static public XPoint Angus(float offset, float ration) { return Angus(offset,ration,0.0f); }
		static float cvtf(float S, float I){ return (float)((Math.PI*2)*(I/S)); }
		// =======================================================
		static public XPoint Fit(XPoint dest, XPoint source)
		{
			return Fit(dest,source,scaleFlags.autoScale);
		}
		static public XPoint Fit(XPoint dest, XPoint source, scaleFlags sf)
		{
			XPoint HX = dest/source;
			if (sf== scaleFlags.autoScale) return (HX.Y > HX.X) ? source*HX.X : source * HX.Y;
			else return (sf== scaleFlags.sWidth) ? source*HX.X : source*HX.Y;
		}
		
		#endregion
		#region Operators
		static public XPoint operator +(XPoint a, XPoint b){ return new XPoint(a.X+b.X,a.Y+b.Y); }
		static public XPoint operator +(XPoint a, Point b){ return new XPoint(a.X+b.X,a.Y+b.Y); }
		static public XPoint operator +(XPoint a, int b){ return new XPoint(a.X+b,a.Y+b); }
		static public XPoint operator +(XPoint a, float b){ return new XPoint(a.X+b,a.Y+b); }
		static public XPoint operator +(XPoint a, double b){ return new XPoint(a.X+b,a.Y+b); }
		static public XPoint operator -(XPoint a){ return new XPoint(-a.X,-a.Y); }
		static public XPoint operator -(XPoint a, XPoint b){ return new XPoint(a.X-b.X,a.Y-b.Y); }
		static public XPoint operator -(XPoint a, Point b){ return new XPoint(a.X-b.X,a.Y-b.Y); }
		static public XPoint operator -(XPoint a, int b){ return new XPoint(a.X-b,a.Y-b); }
		static public XPoint operator -(XPoint a, float b){ return new XPoint(a.X-b,a.Y-b); }
		static public XPoint operator -(XPoint a, double b){ return new XPoint(a.X-b,a.Y-b); }
		static public XPoint operator /(XPoint a, XPoint b){ return new XPoint(a.X/b.X,a.Y/b.Y); }
		static public XPoint operator /(XPoint a, Point b){ return new XPoint(a.X/b.X,a.Y/b.Y); }
		static public XPoint operator /(XPoint a, int b){ return new XPoint(a.X/b,a.Y/b); }
		static public XPoint operator /(XPoint a, float b){ return new XPoint(a.X/b,a.Y/b); }
		static public XPoint operator /(XPoint a, double b){ return new XPoint(a.X/b,a.Y/b); }
		static public XPoint operator *(XPoint a, XPoint b){ return new XPoint(a.X*b.X,a.Y*b.Y); }
		static public XPoint operator *(XPoint a, Point b){ return new XPoint(a.X*b.X,a.Y*b.Y); }
		static public XPoint operator *(XPoint a, int b){ return new XPoint(a.X*b,a.Y*b); }
		static public XPoint operator *(XPoint a, float b){ return new XPoint(a.X*b,a.Y*b); }
		static public XPoint operator *(XPoint a, double b){ return new XPoint(a.X*(float)b,a.Y*(float)b); }
		static public XPoint operator %(XPoint a, XPoint b){ return new XPoint(a.X%b.X,a.Y%b.Y); }
		static public XPoint operator %(XPoint a, Point b){ return new XPoint(a.X%b.X,a.Y%b.Y); }
		static public XPoint operator ++(XPoint a){ return new XPoint(a.X++,a.Y++); }
		static public XPoint operator --(XPoint a){ return new XPoint(a.X--,a.Y--); }
		static public bool operator >(XPoint a,XPoint b){ return ((a.X>b.X) & (a.Y>b.Y)); }
		static public bool operator <(XPoint a,XPoint b){ return ((a.X<b.X) & (a.Y<b.Y)); }
		#endregion
		#region Operators Implicit
		static public implicit operator Point(XPoint a){ return new Point((int)a.X,(int)a.Y); }
		static public implicit operator PointF(XPoint a){ return new PointF((float)a.X,(float)a.Y); }
		static public implicit operator Size(XPoint a){ return new Size((int)a.X,(int)a.Y); }
		static public implicit operator SizeF(XPoint a){ return new SizeF((float)a.X,(float)a.Y); }
		static public implicit operator XPoint(Size s){ return new XPoint(s); }
		static public implicit operator XPoint(SizeF s){ return new XPoint(s); }
		static public implicit operator XPoint(Point s){ return new XPoint(s); }
		static public implicit operator XPoint(PointF s){ return new XPoint(s); }
		#endregion
	
		public XPoint(){ }
		public XPoint(double x, double y){ _X = x; _Y = y; }
		public XPoint(int value) : this(value,value) {  }
		public XPoint(long value) : this(value,value) {  }
		public XPoint(float value) : this((double)value,(double)value) {  }
		public XPoint(double value) : this(value,value) {  }
		public XPoint(Point P){ _X = P.X; _Y = P.Y; }
		public XPoint(PointF P){ _X = P.X; _Y = P.Y; }
		public XPoint(Size P){ _X = P.Width; _Y = P.Height; }
		public XPoint(SizeF P){ _X = P.Width; _Y = P.Height; }
	
		#region Object
		public XPoint Clone(){ return new XPoint(X,Y); }
		public void CopyPoint(XPoint inPoint) { X=inPoint.X; Y=inPoint.Y; }
		public override string ToString() { return String.Format("XPoint:X:{0},Y:{1}",X,Y); }
		#endregion
		
	}
}
