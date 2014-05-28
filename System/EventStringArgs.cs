/* oOo * 11/14/2007 : 10:22 PM */
using System;

namespace System
{
	public class EventStringArgs : EventArgs
	{
		public string data;
		public string[] info;
		public EventStringArgs(string _data) { data = _data; }
		public EventStringArgs(string _data, params string[] _info) :this(_data) { info = _info; }
	}
}
