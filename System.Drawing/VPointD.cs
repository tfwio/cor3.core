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

namespace System.Drawing
{
	public class VPointD : DPoint
	{
		double _Z = 0.0f;
		public double Z { get { return _Z; } set { _Z = value; } }
		public double[] v { get { return new double[]{ X,Y,Z }; } }
		
		public VPointD(double x, double y, double z) : base(x,y) { _Z = z; }
		public VPointD(DPoint _pin, double z) : this(_pin.X,_pin.Y,z) { }
	}
//	public class VPointF : FloatPoint
//	{
//		float _Z = 0.0f;
//		public VPointF(float x, float y, float z) : base(x,y) { _Z = z; }
//		public VPointF(FloatPoint _pin, float z) : this(_pin.X,_pin.Y,z) { }
//	}
}
