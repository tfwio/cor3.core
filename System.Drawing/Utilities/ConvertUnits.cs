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

	public class ConvertUnits
	{
//		static public double MetersToFeet(int met) { return 0d; }
//		static public double FeetToMeters(int met) { return 0d; }
		//http://eosweb.larc.nasa.gov/EDDOCS/Wavelength_Program.html
		public const double met_to_feet = 3.28d; // meters to feet
		public const double spd_of_ligh = 299792458; // meters per second
		public enum band { am,fm }
		public enum units { feet }
		static public double WaveLengthFromFrequency(double freq , band tp)
		{
			// IF units = "feet" [Note: units are, by default, in meters]
			//THEN
			//wavelength = wavelength x METERS TO FEET
			double output = 0d;
			switch (tp)
			{
					case band.am: output = spd_of_ligh/freq; break;
					case band.fm: output = spd_of_ligh*(1000/freq); break;
			}
			return output;
		}
	}
}