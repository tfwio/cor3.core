/**
 * oIo * 3/8/2011 8:29 PM
 **/
// OBSOLETE
using System;
using System.IO;
using System.Reflection;
namespace System
{
	
	/// AssemblyDescription.
	public class AssemblyDescription
	{
		public static readonly Assembly asm = Assembly.GetEntryAssembly();
		public static readonly FileInfo exe = new FileInfo(asm.Location);
	}

}