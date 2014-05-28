/* oOo * 11/14/2007 : 10:22 PM */
using System;
using System.Windows.Forms;
using System.WTF;

namespace System
{
	public partial class ControlUtil : TreeUtil
	{
		/// <returns>null on error.</returns>
		static public Control Obj2Ctl(object obj) { if (obj is Control) return obj as Control; return null; }
		
		/// <summary>This is generally abstraction for the Windows OS user-controls native to the dotnet environment.</summary>
		static public bool CkBox(CheckBox cb){ CkBox(cb,!cb.Checked); return cb.Checked; }
		
		static public bool CkBox(CheckBox cb, bool Validator){ cb.Checked = Validator; return cb.Checked; }
	}
}
