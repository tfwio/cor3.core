/*
 * oIo — 1/16/2011 — 8:29 PM
 */
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
using System.Drawing;

namespace System.Cor3.Drawing
{
	// NOTE: TEST
	// Line Generalization Algorithms
	/// <summary>
	/// HAS NOT BEEN TESTED
	/// </summary>
	public class McMaster // Smoothing
	{
		/// <summary>
		/// Line Generalization Algorighm: McMaster
		/// <para>1) Average the number of ‘ahead’ points to P1' (note that the average used is 5)</para>
		/// <para>2) Get a line for P1 to S1 (which is the middle point or the third point)</para>
		/// <para>2.1) P1 slides to the center of the new Line</para>
		/// --- This is our new Smoothed point ‘S1’ : s1 is the shifted position of P(i)
		/// </summary>
		/// <param name="pts">the number of points to average</param>
		/// <returns></returns>
		static public FloatPoint[] McMasterX(params FloatPoint[] pts)
		{
			// Averate the number of look-ahead points
			if (pts.Length <= 4) return pts;
			FloatPoint[] n5 = new FloatPoint[5];
			List<FloatPoint> S = new List<FloatPoint>();
			S.Add(pts[0]);
			for (int x = 0; x < pts.Length-5; x++)
			{
				n5 = Next(x,5,pts);
				FloatPoint avg = FloatPoint.Average(n5);
				FloatPoint SW = avg+(avg-pts[x+3])*0.5D;
				S.Add(SW);
			}
			S.Add(pts[pts.Length-1]);
			Array.Clear(n5,0,5);
			FloatPoint[] XS = S.ToArray();
			S.Clear(); S=null;
			return XS;
			
		}
		/// • Nothing to say
		static public FloatPoint[] McMasterH(params FloatPoint[] pts)
		{
			// Averate the number of look-ahead points
			if (pts.Length <= 4) return pts;
			FloatPoint[] n5 = new FloatPoint[5];
			List<FloatPoint> S = new List<FloatPoint>();
			for (int x = 0; x < pts.Length-5; x++)
			{
				n5 = Next(x,5,pts);
				FloatPoint avg = (PointF)FloatPoint.Average(n5);
				FloatPoint SW = avg+(avg-pts[x+3])*0.5D;
				S.Add((PointF)SW);
			}
			Array.Clear(n5,0,5);
			FloatPoint[] XS = S.ToArray();
			S.Clear(); S=null;
			return XS;
		}
		/// <summary>Do your checking before calling on me</summary>
		static public FloatPoint[] Next(long start, long len, params FloatPoint[] Pts)
		{
			FloatPoint[] tp = new FloatPoint[5];
			Array.Copy(Pts,start,tp,0,len);
			return tp;
		}
		/// 
		static public double Avg(params double[] points)
		{
			double temp = 0;
			foreach (double point in points) temp += point;
			return temp / points.Length;
		}
		
	}
}
