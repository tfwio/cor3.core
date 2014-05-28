/* User: oIo * Date: 9/21/2010 * Time: 10:22 AM */
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
	/// <summary>
	/// As defined for css.  It apparently is based on a mm which
	/// in the case of css2 is defined as a macro parsable by GNU tools
	/// Flex and/or Bison.
	/// </summary>
	public class Metrics // as defined for css.  It apparently is based on a mm which in the case of css2 is defined as a macro
	{
		// see Printable RTF to see more on Windows Units for printing
		// we have a DPI for RTF Printing in there.  This could be PixelT
		public const double INCH = (25.4D * MM);
		public const double CM = 10D * MM;
		public const double MM = 1D;
		public static double def_mm = 1D;
		public const double PICA = (12D * INCH/72D * MM);
		public const double POINT = (INCH/72D * MM);
		public const double PixelT = MM/92D;
//		public Metrics() : base()
//		{
//			Add("INCH",INCH);
//			Add("CM",CM);
//			Add("MM",MM);
//			Add("PICA",PICA);
//			Add("POINT",POINT);
//			Add("PixelT",PixelT);
//		}
	}
}
