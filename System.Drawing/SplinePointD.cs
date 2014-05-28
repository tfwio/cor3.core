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
using System.Collections.Generic;

namespace System.Drawing
{
	public class SplinePointD : List<DPoint>
	{
		public DPoint[] VectorData { get { return ToArray(); } }
		public SplinePointD() : base() { }
		public SplinePointD(params VPointD[] value) : base(value) { }
	}
	public class SplinePointF : List<FloatPoint>
	{
		public PointF[] ToPointFArray()
		{
			List<PointF> points = new List<PointF>();
			foreach (PointF fp in this) points.Add(fp);
			PointF[] pf = points.ToArray();
			points.Clear();
			return pf;
		}

		public void TranslateTransform(FloatPoint offset)
		{
			for (int i=0; i < Count; i++)
			{
				this[i] += offset;
			}
		}
		public void ScaleTransform(FloatPoint offset)
		{
			for (int i=0; i < Count; i++)
			{
				this[i] *= offset;
			}
		}

		public PointF[] PointFData { get { return ToPointFArray(); } }
		public FloatPoint[] VectorData { get { return ToArray(); } }
		public SplinePointF() : base() { }
		public SplinePointF(params FloatPoint[] value) : base(value) { }
	}
}
