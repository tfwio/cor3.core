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
/**
 * 
 * User: tfw
 * Date: 11/12/2008
 * Time: 9:37 PM
 * 
**/
using System;
using System.Drawing.Drawing2D;
using System.Drawing.Utilities;

namespace System.Drawing
{
	/// <summary>
	/// semantics partly inspired by winforms.docking by weifen.luo.  Its just another one of those things.
	/// </summary>
	public struct Gradients
	{
		static readonly FloatPoint _def0 = new FloatPoint(0,0);
		static readonly FloatPoint _def1 = new FloatPoint(0,40);
		static public LinearGradientBrush GetLinearGradient(string c1, string c2) { return GetLinearGradient(_def0,_def1,ColourUtil.HexStr2Clr(c1),ColourUtil.HexStr2Clr(c2)); }
		static public LinearGradientBrush GetLinearGradient(FloatPoint p1, FloatPoint p2,string c1, string c2) { return GetLinearGradient(p1,p2,ColourUtil.HexStr2Clr(c1),ColourUtil.HexStr2Clr(c2)); }
		static public LinearGradientBrush GetLinearGradient(Color c1, Color c2) { return GetLinearGradient(_def0,_def1,c1,c2); }
		static public LinearGradientBrush GetLinearGradient(FloatPoint p1, FloatPoint p2, Color c1, Color c2) { return new LinearGradientBrush(p1,p2,c1,c2); }
		static public LinearGradientBrush GradientDarkBackground { get { return new LinearGradientBrush( _def0,_def1,SystemColors.Control,Color.FromArgb(0,SystemColors.ControlDarkDark)); } }
		//static public LinearGradientBrush GradientDarkBackground { get { return new LinearGradientBrush( _def0,_def1,Color.FromArgb(127,Color.FromArgb(0x7FFF)),Color.FromArgb(127,Color.Black)); } }
		static public LinearGradientBrush DocumentActive { get { return new LinearGradientBrush( _def0,_def1,SystemColors.AppWorkspace,Color.FromArgb(0,SystemColors.Control)); } }
		//static public LinearGradientBrush GradientRedDark { get { return new LinearGradientBrush( _def0,_def1,Color.FromArgb(127,Color.FromArgb(0x7F0000)),Color.FromArgb(127,Color.Black)); } }
		static public LinearGradientBrush ActiveCaption { get { return new LinearGradientBrush( _def0,_def1,SystemColors.ActiveCaption,Color.FromArgb(0,SystemColors.ActiveCaption)); } }
		static public LinearGradientBrush InactiveCaption { get { return new LinearGradientBrush( _def0,_def1,SystemColors.InactiveCaption,Color.FromArgb(0,SystemColors.InactiveCaption)); } }
		//static public LinearGradientBrush GradientBlueControl { get { return new LinearGradientBrush( _def0,_def1,Color.FromArgb(255,Color.FromArgb(0x7FFF)),Color.FromArgb(255,SystemColors.Control)); } }
	}
}