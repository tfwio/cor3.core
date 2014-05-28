/* oOo * 11/14/2007 : 10:22 PM */
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace System
{
	public class LoggerAttribute : Attribute
	{
		logT tmsg;
		public logT TMsg {
			get { return tmsg; }
			set { tmsg = value; }
		}

		String name;
		public string Name {
			get { return name; }
			set { name = value; }
		}

		String filter;
		public string Filter {
			get { return filter; }
			set { filter = value; }
		}
	
		String[] message;
		public string[] Message {
			get { return message; }
			set { message = value; }
		}
	
		public LoggerAttribute(logT tmsg, string Name, string Filter, params string[] Message)
		{
			this.tmsg = tmsg;
			this.name = Name;
			this.filter = Filter;
			this.message  = Message;
		}
		[Conditional("CONSOLE")]
		public void Log()
		{
			Logger.Log(tmsg,name,filter,message);
		}
	}
}
