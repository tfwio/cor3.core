/* oOo * 11/14/2007 : 10:22 PM */
using System;

namespace System.IO
{
	public class InputFileManager
	{
		virtual public void Load()
		{
		}
		virtual public void Load(string remarks)
		{
		}
		
		virtual public void Save(string filename) { Save(filename,LoadSaveFlags.Save); }
		virtual public void SaveAs(string filename) { Save(filename,LoadSaveFlags.SaveAs); }
		virtual public void SaveCopy(string filename) { Save(filename,LoadSaveFlags.SaveCopy); }
		virtual public void Save(string filename, LoadSaveFlags f) { eSaveInput(filename,f); }
		
		/// this is just a demo procedure.
		public SaveInputFile LoadInput;
		public SaveInputFile SaveInput;
		
		protected bool SaveInputFile(string filename, LoadSaveFlags f)
		{
			if (SaveInput!=null) return SaveInput(filename,f);
			return false;
		}
		
		virtual public bool eSaveInput(string file, LoadSaveFlags flg)
		{
			/*Save(file);*/ return true;
		}
		
		public InputFileManager()
		{
			SaveInput += eSaveInput;
			//			SaveInput += eLoadInput;
		}
	}
}
