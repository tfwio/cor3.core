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
using System;
using System.Drawing.Utilities.SuperOld;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.Drawing
{

	/// <remarks>
	/// Only Add and Neg calls have Integer parameters when compared to the
	/// other parameter based calls (because they were most recently added).
	/// </remarks>
	public class HPoint
	{
		static public HPoint Empty { get { return new HPoint(0f); } }
		static public HPoint One { get { return new HPoint(1f); } }

		float _X=0f, _Y=0f;
		[XmlAttribute] public float X { get { return _X; } set { _X = value; } }
		[XmlAttribute] public float Y { get { return _Y; } set { _Y = value; } }

		#region Properties
		[XmlIgnore] public float Bigger { get { return (X >= Y)? X: Y; } }
		[XmlIgnore] public bool IsLand { get { return X >= Y; } }
		/// <summary>zerod'</summary>
		[XmlIgnore] public double Slope { get  { return Math.Sqrt(Math.Pow(X,2)+Math.Pow(Y,2)); }  }
		#endregion
		#region Static Methods
		static public HPoint FlattenPoint(HPoint _pin, bool roundUp)
		{
			HPoint newP = _pin.Clone();
			if (newP.X==newP.Y) return newP;
			if (_pin.X > _pin.Y) { if (roundUp) newP.Y = newP.X; else newP.X = newP.Y; }
			else { if (!roundUp) newP.Y = newP.X; else newP.X = newP.Y; }
			return newP;
		}
		static public HPoint FlattenPoint(HPoint _pin) { return FlattenPoint(_pin,false); }
		/// <summary>same as FlattenPoint overload without boolean</summary>
		static public HPoint FlattenDown(HPoint _pin) { return FlattenPoint(_pin); }
		static public HPoint FlattenUp(HPoint _pin) { return FlattenPoint(_pin,true); }

		static public HPoint GetClientSize(Control ctl) { return ctl.ClientSize; }
		static public HPoint GetPaddingTopLeft(Padding pad) { return new HPoint(pad.Left,pad.Top); }
		static public HPoint GetPaddingOffset(Padding pad) { return new HPoint(pad.Left+pad.Right,pad.Top+pad.Bottom); }
//
		static public HPoint Angus(float offset, float ration, float phase) { return new HPoint(-Math.Sin(cvtf(ration,offset+phase)),Math.Cos(cvtf(ration,offset+phase))); }
		static public HPoint Angus(float offset, float ration) { return Angus(offset,ration,0.0f); }
		static float cvtf(float S, float I){ return (float)((Math.PI*2)*(I/S)); }
		/// scales src to dest
		static public HPoint Fit(HPoint dest, HPoint source)
		{
			return Fit(dest,source,scaleFlags.autoScale);
		}
		static public HPoint Fit(HPoint dest, HPoint source, scaleFlags f)
		{
			HPoint HX = dest/source;
			if (f== scaleFlags.autoScale) return (HX.Y > HX.X) ? source*HX.X : source * HX.Y;
			else return (f== scaleFlags.sWidth) ? source*HX.X : source*HX.Y;
		}
		#region Obsolete
/*		/// todo: replace this with something more accurate and faster
		/// use Boxed as the default scale flag
		static public HPoint Fit(Point dest, Point source, scaleFlags sf)
		{
			HPoint skaler = new HPoint();
			float tr=0;
			string omax,cmax;
			if (source.X >= source.Y) omax = "width"; else omax="height";
			if (dest.X >= dest.Y) cmax = "width"; else cmax="height";
			switch (cmax)
			{
				case "width": if (omax==cmax) sf = scaleFlags.sWidth; else sf = scaleFlags.sHeight; break;
				case "height": if (omax==cmax) sf = scaleFlags.sHeight; else sf = scaleFlags.sWidth; break;
			}
		skale:
			switch (sf)
			{
				case scaleFlags.sHeight:
					if ( source.Y > dest.Y ) { skaler.Y = dest.Y; tr = (float)((float)dest.Y / (float)source.Y); skaler.X = (int)Math.Round(source.X*tr); }
					else skaler = source; break;
				case scaleFlags.sWidth:
					if ( source.X > dest.X ) { skaler.X = dest.X; tr = (float)((float)dest.X / (float)source.X); skaler.Y = (int)Math.Round(source.Y*tr); }
					else skaler = source; break;
			}
			if ((skaler.X > dest.X) | (skaler.Y > dest.Y))
			{
				switch (sf)
				{
					case scaleFlags.sWidth: sf = scaleFlags.sHeight; goto skale;
					case scaleFlags.sHeight: sf = scaleFlags.sWidth; goto skale;
				}
			}
			return skaler;
		}*/
		#endregion
		#endregion
		#region Help
//		public XPoint Translate(XPoint offset, XPoint zoom, bool ZoomAfterOffset, bool MultiplyOffset)
//		{
//			return (ZoomAfterOffset) ? (this+((MultiplyOffset) ? offset*zoom : offset ))*zoom : (this*zoom)+((MultiplyOffset) ? offset*zoom : offset );
//		}
		/// In most scenarios (reminding my self of my application...)
		/// zoom is applied by the renderer, so we shouldn't need it?
		/// I could be wrong.  We could be zoomed in to draw.
		public HPoint Translate(HPoint offset, HPoint zoom)
		{
			return (this+offset)*zoom;
		}
		public HPoint Translate(double offset, double zoom)
		{
			return (this+new HPoint(offset))*new HPoint(zoom);
		}
		#endregion
		#region Helpers “Most of which are obsoltet”
		public HPoint GetRation(HPoint dst)
		{
			return dst/this;
		}
		public HPoint GetScaledRation(HPoint dst)
		{
			return this*(dst/this);
		}
		public float Dimension() { return X*Y; }
		/// <summary>Returns a new flattened point</summary>
		public HPoint Flat(bool roundUp) { return FlattenPoint(this,roundUp); }
		/// <summary>Flattens the calling point</summary>
		public void Flatten(bool roundUp) { HPoint f = Flat(roundUp); this.X = f.X; this.Y = f.Y; f = null; }
		/// <summary>use Flat or flatten calls.</summary>
		public HPoint ScaleTo(HPoint point)
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
		#endregion
		#region Operators
		static public HPoint operator +(HPoint a, HPoint b){ return new HPoint(a.X+b.X,a.Y+b.Y); }
		static public HPoint operator +(HPoint a, Point b){ return new HPoint(a.X+b.X,a.Y+b.Y); }
		static public HPoint operator +(HPoint a, int b){ return new HPoint(a.X+b,a.Y+b); }
		static public HPoint operator +(HPoint a, float b){ return new HPoint(a.X+b,a.Y+b); }
		static public HPoint operator +(HPoint a, double b){ return new HPoint(a.X+b,a.Y+b); }
		static public HPoint operator -(HPoint a){ return new HPoint(-a.X,-a.Y); }
		static public HPoint operator -(HPoint a, HPoint b){ return new HPoint(a.X-b.X,a.Y-b.Y); }
		static public HPoint operator -(HPoint a, Point b){ return new HPoint(a.X-b.X,a.Y-b.Y); }
		static public HPoint operator -(HPoint a, int b){ return new HPoint(a.X-b,a.Y-b); }
		static public HPoint operator -(HPoint a, float b){ return new HPoint(a.X-b,a.Y-b); }
		static public HPoint operator -(HPoint a, double b){ return new HPoint(a.X-b,a.Y-b); }
		static public HPoint operator /(HPoint a, HPoint b){ return new HPoint(a.X/b.X,a.Y/b.Y); }
		static public HPoint operator /(HPoint a, Point b){ return new HPoint(a.X/b.X,a.Y/b.Y); }
		static public HPoint operator /(HPoint a, int b){ return new HPoint(a.X/b,a.Y/b); }
		static public HPoint operator /(HPoint a, float b){ return new HPoint(a.X/b,a.Y/b); }
		static public HPoint operator /(HPoint a, double b){ return new HPoint(a.X/b,a.Y/b); }
		static public HPoint operator *(HPoint a, HPoint b){ return new HPoint(a.X*b.X,a.Y*b.Y); }
		static public HPoint operator *(HPoint a, Point b){ return new HPoint(a.X*b.X,a.Y*b.Y); }
		static public HPoint operator *(HPoint a, int b){ return new HPoint(a.X*b,a.Y*b); }
		static public HPoint operator *(HPoint a, float b){ return new HPoint(a.X*b,a.Y*b); }
		static public HPoint operator *(HPoint a, double b){ return new HPoint(a.X*(float)b,a.Y*(float)b); }
		static public HPoint operator %(HPoint a, HPoint b){ return new HPoint(a.X%b.X,a.Y%b.Y); }
		static public HPoint operator %(HPoint a, Point b){ return new HPoint(a.X%b.X,a.Y%b.Y); }
		static public HPoint operator ++(HPoint a){ return new HPoint(a.X++,a.Y++); }
		static public HPoint operator --(HPoint a){ return new HPoint(a.X--,a.Y--); }
		static public bool operator >(HPoint a,HPoint b){ return ((a.X>b.X) & (a.Y>b.Y)); }
		static public bool operator <(HPoint a,HPoint b){ return ((a.X<b.X) & (a.Y<b.Y)); }
		#endregion
		#region Operators Implicit
		static public implicit operator Point(HPoint a){ return new Point((int)a.X,(int)a.Y); }
		static public implicit operator PointF(HPoint a){ return new PointF(a.X,a.Y); }
		static public implicit operator Size(HPoint a){ return new Size((int)a.X,(int)a.Y); }
		static public implicit operator SizeF(HPoint a){ return new SizeF(a.X,a.Y); }
		static public implicit operator HPoint(Size s){ return new HPoint(s); }
		static public implicit operator HPoint(SizeF s){ return new HPoint(s); }
		static public implicit operator HPoint(Point s){ return new HPoint(s); }
		static public implicit operator HPoint(PointF s){ return new HPoint(s); }
		#endregion
		#region Maths
		public bool IsXG(HPoint P) { return X>P.X; }
		public bool IsYG(HPoint P) { return Y>P.Y; }
		public bool IsXL(HPoint P) { return X<P.X; }
		public bool IsYL(HPoint P) { return Y<P.Y; }

		public bool IsLEq(HPoint p) { return (X<=p.X) && (Y<=p.Y); }
		public bool IsGEq(HPoint p) { return (X>=p.X) && (Y>=p.Y); }
		
		public bool IsXGEq(HPoint P) { return IsXG(P)&IsXG(P); }
		public bool IsYGEq(HPoint P) { return IsYG(P)&IsYG(P); }
		public bool IsXLEq(HPoint P) { return IsXG(P)&IsXG(P); }
		public bool IsYLEq(HPoint P) { return IsYG(P)&IsYG(P); }
		public bool IsXEq(HPoint P) { return X==P.X; }
		public bool IsYEq(HPoint P) { return Y==P.Y; }

		public HPoint Multiply(params HPoint[] P) {
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new HPoint(X,Y)*P[0];
			HPoint NewPoint = new HPoint(X,Y)*P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint *= P[i];
			}
			return NewPoint;
		}
		public HPoint Multiply(params float[] P) {
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new HPoint(X,Y)*P[0];
			HPoint NewPoint = new HPoint(X,Y)*P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint *= P[i];
			}
			return NewPoint;
		}
		public HPoint Divide(params HPoint[] P)
		{
			if (P.Length==0) throw new ArgumentException("there is no data!");
			if (P.Length==1) return new HPoint(X,Y)/P[0];
			HPoint NewPoint = new HPoint(X,Y)/P[0];
			for (int i = 1; i < P.Length; i++)
			{
				NewPoint /= P[i];
			}
			return NewPoint;
		}

		public HPoint MulX(params HPoint[] P)
		{
			HPoint PBase = Clone();
			foreach (HPoint RefPoint in P) PBase.X *= RefPoint.X;
			return PBase;
		}
		public HPoint MulY(params HPoint[] P)
		{
			HPoint PBase = Clone();
			foreach (HPoint RefPoint in P) PBase.Y *= RefPoint.Y;
			return PBase;
		}
		public HPoint DivX(params HPoint[] P)
		{
			HPoint PBase = Clone();
			foreach (HPoint RefPoint in P) PBase.X /= RefPoint.X;
			return PBase;
		}
		public HPoint DivY(params HPoint[] P)
		{
			HPoint PBase = Clone();
			foreach (HPoint RefPoint in P) PBase.Y /= RefPoint.Y;
			return PBase;
		}
		public HPoint AddX(params HPoint[] P)
		{
			HPoint PBase = Clone();
			foreach (HPoint RefPoint in P) PBase.X += RefPoint.X;
			return PBase;
		}
		public HPoint AddY(params HPoint[] P)
		{
			HPoint PBase = Clone();
			foreach (HPoint RefPoint in P) PBase.Y += RefPoint.Y;
			return PBase;
		}
		public HPoint AddY(params int[] P)
		{
			HPoint PBase = Clone();
			foreach (int RefPoint in P) PBase.Y += RefPoint;
			return PBase;
		}
		public HPoint NegX(params HPoint[] P)
		{
			HPoint PBase = Clone();
			foreach (HPoint RefPoint in P) PBase.X -= RefPoint.X;
			return PBase;
		}
		public HPoint NegX(params int[] P)
		{
			HPoint PBase = Clone();
			foreach (int Ref in P) PBase.X -= Ref;
			return PBase;
		}
		public HPoint NegY(params HPoint[] P)
		{
			HPoint PBase = Clone();
			foreach (HPoint RefPoint in P) PBase.Y -= RefPoint.Y;
			return PBase;
		}
		public HPoint NegY(params int[] P)
		{
			HPoint PBase = Clone();
			foreach (int Ref in P) PBase.Y -= Ref;
			return PBase;
		}
		#endregion

		public HPoint(){ }
		public HPoint(float x, float y){ _X = x; _Y = y; }
		public HPoint(int value) : this(value,value) {  }
		public HPoint(float value) : this(value,value) {  }
		public HPoint(double value) : this(value,value) {  }
		public HPoint(double x, double y) : this((float)x,(float)y) {  }
		public HPoint(Point P){ _X = P.X; _Y = P.Y; }
		public HPoint(PointF P){ _X = P.X; _Y = P.Y; }
		public HPoint(Size P){ _X = P.Width; _Y = P.Height; }
		public HPoint(SizeF P){ _X = P.Width; _Y = P.Height; }

		#region Obj
		// Object ====================================
		public HPoint Clone(){ return (HPoint)(new HPoint(X,Y).Clone()); }
		public void CopyPoint(HPoint inPoint) { X=inPoint.X; Y=inPoint.Y; }
		public override string ToString() { return String.Format("HPoint:X:{0},Y:{1}",X,Y); }
		#endregion

	}

}
