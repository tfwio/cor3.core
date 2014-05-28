/* oOo * 11/14/2007 : 10:22 PM */

using System;
namespace System.IO
{
	public delegate bool SaveInputFile(string file, LoadSaveFlags f);
	public class InputFile
	{
		string filename = string.Empty;
		
		virtual public string filefilter { get { return "All Files|*"; } }
		virtual public string FileName { get { return filename; } set { filename=value; } }
		
		virtual public void Open()
		{
			string file = ControlUtil.FGet(filefilter);
			if (file==string.Empty) return;
			FileName = file;
		}
		virtual public void Open(string filen)
		{
			if (filen==string.Empty) return;
			FileName = filen;
		}
		
		virtual public void Save()
		{
			
		}
		virtual public void Save(string fname)
		{
			
		}
		
		public InputFile() { }
		public InputFile(bool ShowOpen) { if (ShowOpen) Open(); }
	}
}

