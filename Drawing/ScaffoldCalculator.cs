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

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

/* User: oIo * Date: 9/21/2010 * Time: 10:22 AM */

namespace System.Cor3.Drawing
{
	// NOTE: TEST
	/// <summary>
	/// A helpful little utility that demonstrates organizing a grid of columns and rows,
	/// where the content can have any number of columns per row depending on the width of
	/// the content.
	/// <para>
	/// I suppose that I could just have called this FlowContent Renderer and you would have
	/// gotten the idea.
	/// </para>
	/// </summary>
	public class ScaffoldCalculator<TRows,TCols> // : DICT<long,FloatRect>
	{
		public FloatPoint Dimensions;
		
		public DictionaryList<int,long> dic = new DictionaryList<int,long>(); // our static index is virtual per item in this dic
		
		public FloatPoint[] Rows;

		public ScaffoldCalculator(FloatPoint dimensions, DICT<long,FloatRect> newItems)
			//	: base(newItems)
		{
			Dimensions = dimensions;
			Calculate(newItems);
		}

		float GetBigger(params float[] value)
		{
			float tvalue = 0;
			foreach (float f in value) tvalue = (tvalue > f) ? tvalue : f;
			return tvalue;
		}
		
		FloatPoint GetBiggerX(params FloatPoint[] value)
		{
			FloatPoint tvalue = FloatPoint.Empty;
			foreach (FloatPoint f in value) tvalue.X = (tvalue.X > f.X) ? tvalue.X : f.X;
			return tvalue;
		}
		
		FloatPoint GetBiggerY(params FloatPoint[] value)
		{
			FloatPoint tvalue = FloatPoint.Empty;
			foreach (FloatPoint f in value)
				tvalue.Y = (tvalue.Y > f.Y) ? tvalue.Y : f.Y;
			return tvalue;
		}
		
		public bool IsFit(FloatRect r, float X)
		{
			if ((r.Width + X) > Dimensions.X)
				return false;
			return true;
		}
		
		public FloatPoint GetRowWidths(DICT<long,FloatRect> basis, params long[] items)
		{
			FloatPoint p = new FloatPoint(0,0);
			foreach (long i in items)
			{
				p.Y = GetBigger(basis[i].X,(float)basis[i].Size.Y,p.Y);
			}
			return p;
		}
		
		public FloatPoint[] GetRows(DICT<long,FloatRect> basis)
		{
			List<FloatPoint> list = new List<FloatPoint>();
			foreach( int i in dic.KeyArray)
			{
				list.Add(GetRowWidths(basis,dic[i].ToArray()));
			}
			return list.ToArray();
		}
		public Rectangle[] RowRects()
		{
			FloatPoint LastPoint = FloatPoint.Empty;
			List<Rectangle> Rects = new List<Rectangle>();
			int RowCoun = 0;
			foreach (FloatPoint hp in Rows)
			{
				Rects.Add(
					new Rectangle(
						(int)hp.X,
						(int)LastPoint.Y,
						(int)Rows[RowCoun].X,
						(int)Rows[RowCoun].Y
					));
				LastPoint.Y = Rows[RowCoun].Y+LastPoint.Y;
				RowCoun++;
			}
			return Rects.ToArray();
		}
		public DictionaryList<int,long> Calculate(DICT<long,FloatRect> newItems)
		{
			int col = 0;
			float tcol = 0;
			DictionaryList<int,long> data = new DictionaryList<int, long>();
			List<long> hp = new List<long>();
			foreach (long l in newItems.Keys)
			{
				if (IsFit(newItems[l],tcol))
				{
					hp.Add(l); tcol += newItems[l].Width;
				}
				else
				{
					data.Add( col++, new List<long>( hp.ToArray() ) );
					hp.Clear();
					tcol = 0;
					hp.Add(l);
					tcol += newItems[l].Width;
				}
			}
			data.Add( col++, new List<long>( hp.ToArray() ) );
			dic = data;
			Rows = GetRows(newItems); // we could have gotten these?
			return data;
		}

	}
}