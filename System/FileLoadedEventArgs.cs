/* oOo * 11/14/2007 : 10:22 PM */
using System;
using System.IO;

namespace System
{
	public class FileLoadedEventArgs : EventArgs
	{
		public FileInfo FileInfo;
		public FileLoadedEventArgs(FileInfo fi)
		{
			FileInfo = fi;
		}
	}
}
