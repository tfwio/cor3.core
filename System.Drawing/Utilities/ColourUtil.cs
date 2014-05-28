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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace System.Drawing.Utilities
{
	public class ColourUtil
	{
		static public string Color2HexStr(Color ColorPicker)
		{
			return string.Format("#{0:X6}",ColorPicker.ToArgb()&0xFFFFFF);
		}
		static public Color HexStr2Clr(string value) { return HexStr2Clr(255,value); }
		static public Color HexStr2Clr(int alpha, string value)
		{
			int reference = 0;
			try { reference = int.Parse(value.TrimStart('#'),System.Globalization.NumberStyles.HexNumber); }
			catch{System.Windows.Forms.MessageBox.Show("color conversion error");}
			return System.Drawing.Color.FromArgb(alpha,System.Drawing.Color.FromArgb(reference));
			//return = string.Format("#{0:X8}",ColorPicker.ToArgb());
			//return string.Format("#{0:X6}",ColorPicker.ToArgb()&0xFFFFFF);
		}
		static public IList<Color> RgbInt
		{
			get
			{
				List<Color> clist = new List<Color>();
				clist.AddRange((CastColor(8,(int)0xFF0000)));		// R+G
				clist.AddRange((CastColor(16,(int)0xFF00,true)));	// G-R
				clist.AddRange((CastColor(0,(int)0xFF00)));			// G+B
				clist.AddRange((CastColor(8,(int)0xFF,true)));		// B-G
				clist.AddRange((CastColor(16,(int)0xFF)));			// B+R
				clist.AddRange((CastColor(0,(int)0xFF0000,true)));	// R-B
				return clist;
			}
		}
		static public IList<int> RGB_List
		{
			get
			{
				List<int> clist = new List<int>();
				clist.AddRange(CastInt(8,(int)0xFF0000));		// R+G
				clist.AddRange(CastInt(16,(int)0xFF00,true));	// G-R
				clist.AddRange(CastInt(0,(int)0xFF00));			// G+B
				clist.AddRange(CastInt(8,(int)0xFF,true));		// B-G
				clist.AddRange(CastInt(16,(int)0xFF));			// B+R
				clist.AddRange(CastInt(0,(int)0xFF0000,true));	// R-B
				return clist;
			}
		}
		static public IList<int> CMY_List
		{
			get
			{
				List<int> clist = new List<int>();
				clist.AddRange(CastInt(8,(int)0xFF,true));		// B-G
				clist.AddRange(CastInt(16,(int)0xFF));			// B+R
				clist.AddRange(CastInt(0,(int)0xFF0000,true));	// R-B
				clist.AddRange(CastInt(8,(int)0xFF0000));		// R+G
				clist.AddRange(CastInt(16,(int)0xFF00,true));	// G-R
				clist.AddRange(CastInt(0,(int)0xFF00));			// G+B
				return clist;
			}
		}

		static public float Hue(int clr) { return IntColor(clr).GetHue(); }
		static public float Hue(Color clr) { return clr.GetHue(); }
		static public float Sat(int clr) { return IntColor(clr).GetSaturation(); }
		static public float Sat(Color clr) { return clr.GetSaturation(); }

		#region Unused Color Flags
		[Flags] public enum uRGB : uint
		{
			Opaque		= 0xFF000000,//4278190080
			Black		= 0x000000,//0
			White		= 0xFFFFFF,//16777215
			Red			= 0xFF0000,//16711680
			Yellow		= Red|Green,//16776960
			Green		= 0x00FF00,//65280
			Cyan		= Blue|Green,//65535
			Blue		= 0x0000FF,//255
			Magenta		= Red|Blue,//16711935
		}
		[Flags] public enum RYGCBM : int
		{
			Red			= 0x100,
			Yellow		= 0x110,
			Green		= 0x010,
			Cyan		= 0x011,
			Blue		= 0x001,
			Magenta		= 0x101,
		}
		[Flags] public enum iRGB : int
		{
			Black		= 0x000000,//0
			White		= 0xFFFFFF,//16777215
			Red			= ((0xFF)<<16),//16711680,-65788
			Yellow		= Red|Green,//16776960,-131322
			Green		= ((0xFF)<<8),//65280,-196856
			Cyan		= Blue|Green,//65535
			Blue		= (0xFF),//255
			Magenta		= Red|Blue,//16711935
		}
		#endregion
	
		static int[] colorIncr = new int[768];
	
		#region Cast()
		static public IList<int> CastInt(int shift) { return CastInt(shift,0,false); }
		static public IList<int> CastInt(int shift, int append) { return CastInt(shift,append,false); }
		static public IList<int> CastInt(int shift, bool inv) { return CastInt(shift,0,inv); }
		static public IList<int> CastInt(int shift, int append, bool inv)
		{
			List<int> list = new List<int>();
			for (int i=0; i<=255; i++) list.Add(((int)i<<shift)+append);
			if (inv) list.Reverse();
			return list;
		}
		static public IList<Color> CastColor(int shift) { return CastColor(shift,0,false); }
		static public IList<Color> CastColor(int shift, int append) { return CastColor(shift,append,false); }
		static public IList<Color> CastColor(int shift, bool inv) { return CastColor(shift,0,inv); }
		static public IList<Color> CastColor(int shift, int append, bool inv)
		{
			List<Color> list = new List<Color>();
			for (int i=0; i<=255; i++) list.Add(IntColor(((int)i<<shift)+append));
			if (inv) list.Reverse();
			return list;
		}
		#endregion

		#region ColorFromInt
		public struct IntColorv
		{
			public int Value;
			public Color Color { get { return Color.FromArgb(Value); } set { Value=value.ToArgb(); } }
			public IntColorv(int rgb) { Value = Color.FromArgb(rgb).ToArgb(); }
			public IntColorv(params int[] val) { Value =  val.Length>3 ? (val[0]<<24)+(val[1]<<16)+(val[2]<<8)+(val[3]): (val[0]<<16)+(val[1]<<8)+(val[2]); }
			public IntColorv(int alpha,int rgb) { Value = (alpha<<24)+rgb; }
		}

		static public Color IntColor(int rgb) { return IntColor(255,rgb); }
		static public Color IntColor(params int[] val) { return val.Length>3 ? Color.FromArgb(val[0],val[1],val[2],val[3]): Color.FromArgb(val[0],val[1],val[2]); }
		static public Color IntColor(int alpha,int rgb) { return Color.FromArgb(alpha,Color.FromArgb(rgb)); }
		#endregion
	
		#region I have no idea how this got here?
		public void SetMatrix(Graphics gfx, ColorMatrix mtx, FloatRect rect)
		{
			gfx.SetClip(rect,CombineMode.Replace);
			//gfx.MultiplyTransform(mtx); //??
			gfx.ResetClip();
		}
	//wtf
		static ColorMatrix matrix = new ColorMatrix();
		static public ColorMatrix Matrix { get { return matrix; } set { matrix = value; } }
		#endregion

	}
}
