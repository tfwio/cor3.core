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
/* User: oIo * Date: 9/21/2010 * Time: 10:22 AM */
using System;
using System.ComponentModel;

namespace System.Drawing
{

	public class FloatRectCorners
	{
		float[] Corners = new float[4];
	
		bool HasIdenticalValues
		{
			get
			{
				int i = 1;
				float lastvalue = Corners[0];
				while (i++ < Corners.Length)
				{
					if (lastvalue.CompareTo(Corners[i])!=0) return false;
					lastvalue = Corners[i];
				}
				return true;
			}
		}
		[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
		public float All { get { return HasIdenticalValues? TopLeft: -1f; } set { if (value== -1f) return; Corners = new float[]{value,value,value,value}; } }
		public float TopLeft { get { return Corners[0]; } set { Corners[0] = value; } }
		public float TopRight { get { return Corners[1]; } set { Corners[1] = value; } }
		public float BottomRight { get { return Corners[2]; } set { Corners[2] = value; } }
		public float BottomLeft { get { return Corners[3]; } set { Corners[3] = value; } }
		public FloatRectCorners() : this(7f)
		{
		}
		public FloatRectCorners(float all) : this(all,all,all,all)
		{
		}
		public FloatRectCorners(float tl, float tr, float br, float bl)
		{
			Corners = new float[4]{tl,tr,br,bl};
		}
		
	}
}
