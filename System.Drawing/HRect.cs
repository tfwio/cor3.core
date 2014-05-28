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
/* THIS CALSS HAS NOT YET FULLY BEEN TESTED */
using System;

namespace System.Drawing
{
	#region “ Depreceated HRect ”
	public class HRect : FloatRect {
        ////////////////////////////////////////////////////////////////////////
        //	
		static public implicit operator Rectangle(HRect a){ return new Rectangle((int)a.X,(int)a.Y,(int)a.Width,(int)a.Height); }
		static public implicit operator RectangleF(HRect a){ return new RectangleF(a.X,a.Y,a.Width,a.Height); }
		static public implicit operator HRect(Rectangle a){ return new HRect(a.X,a.Y,a.Right,a.Bottom); }
		static public implicit operator HRect(RectangleF a){ return new HRect(a.X,a.Y,a.Right,a.Bottom); }
       public HRect() :base() {}
		public HRect(float x, float y, float width, float height) : base(x,y,width,height) { Location = new FloatPoint(x,y); Size = new FloatPoint(width,height); }
		public HRect(int x, int y, int width, int height) : this((float)x,(float)y,(float)width,(float)height) {}
		public HRect(FloatPoint L, FloatPoint S) : this(L.X,L.Y,S.X,S.Y) {}
		public HRect(Rectangle R) : this(R.X,R.Y,R.Width,R.Height) { }
		public HRect(RectangleF R) : this(R.X,R.Y,R.Width,R.Height) { }
		public HRect(PointF Loc, SizeF Siz) : this(Loc.X,Loc.Y,Siz.Width,Siz.Height) {}
		
	}
	#endregion

}
