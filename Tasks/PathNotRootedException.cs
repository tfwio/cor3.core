/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 11/16/2013
 * Time: 6:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
namespace System.Tasks
{
	public class PathNotRootedException : Exception
	{
		public string InputFile {
			get {
				return inputFile;
			}
			set {
				inputFile = value;
			}
		}

		string inputFile;

		public override string Message {
			get {
				return string.Format("Input file \"{0}\" not rooted.  A rooted file contains a drive-letter and colon as the first two chars.", inputFile);
			}
		}

		public PathNotRootedException(string fname)
		{
			inputFile = fname;
		}
	}
}


