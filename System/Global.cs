/* oOo * 11/14/2007 : 10:22 PM */
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace System
{
	public class Global : System.Cor3.last_addon
	{
		static public PropertyGrid _props;
		static public PropertyGrid PropertyGrid { get { return _props; } set { _props=value; } }
	}

}
