#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */

using System;
using System.Runtime.InteropServices;
using SF = System.Environment.SpecialFolder;

namespace System
{
	public class Paths
	{
		static public string ApplicationData { get { return Environment.GetFolderPath(SF.ApplicationData); } }
		static public string Desktop { get { return Environment.GetFolderPath(SF.Desktop); } }
		static public string DesktopDirectory { get { return Environment.GetFolderPath(SF.DesktopDirectory); } }
		static public string LocalApplicationData { get { return Environment.GetFolderPath(SF.LocalApplicationData); } }
	}
}
