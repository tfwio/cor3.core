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

namespace System.Drawing.Utilities
{
	namespace SuperOld
	{
		using System.Windows.Forms;
		
		public interface IBufferedRenderer
		{
			void DoRender(Graphics fx);
			void UpdateRect(Control ctl);
			void UpdateGraphics(Control ctl);
			void Update(Control ctl);
			void Render(Graphics fx);
			void Apply(object sender, PaintEventArgs e);
		}
		public class RenderDict : DICT<string,IBufferedRenderer> {  }
		public class BrushDict : DICT<string,Brush> {  }
		public class PenDict : DICT<string,Pen> {  }
		public class ColorDict : DICT<string,Color> {  }
		public class BitmapDict : DICT<string,Bitmap> {  }
		public class ImageDict : DICT<string,Image> {  }
		public class SizeDict : DICT<string,FloatPoint> {  }
		public class RectDict : DICT<string,FloatRect> {  }

		public class Dicts
		{
//		static public RenderTypeDict RenderTypeDictEmpty { get { return new RenderTypeDict(); } }
//		static public RenderRegionDict RenderRegionDictEmpty { get { return new RenderRegionDict(); } }
			static public RenderDict RenderDictEmpty { get { return new RenderDict(); } }
			static public BrushDict BrushDictEmpty { get { return new BrushDict(); } }
			static public PenDict PenDictEmpty { get { return new PenDict(); } }
			static public ColorDict ColorDictEmpty { get { return new ColorDict(); } }
			static public BitmapDict BitmapDictEmpty { get { return new BitmapDict(); } }
			static public ImageDict ImageDictEmpty { get { return new ImageDict(); } }
			static public SizeDict SizeDictEmpty { get { return new SizeDict(); } }
			static public RectDict RectDictEmpty { get { return new RectDict(); } }
		}
	}
}
