#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */
using System;
using System.Windows.Forms;

namespace System
{
	public class Status : AssemblyDescription
	{
		#region Console
		static ConsoleColor Darker (ConsoleColor clr)
		{
			switch (clr)
			{
					case ConsoleColor.Blue: return ConsoleColor.DarkBlue;
					case ConsoleColor.Cyan: return ConsoleColor.DarkCyan;
					case ConsoleColor.Gray: return ConsoleColor.DarkGray;
					case ConsoleColor.DarkGray: return ConsoleColor.Black;
					case ConsoleColor.Green: return ConsoleColor.DarkGreen;
					case ConsoleColor.Magenta: return ConsoleColor.DarkMagenta;
					case ConsoleColor.Red: return ConsoleColor.DarkRed;
					case ConsoleColor.White: return ConsoleColor.Gray;
					case ConsoleColor.Yellow: return ConsoleColor.DarkYellow;
					default: return ConsoleColor.DarkGray;
			}
		}
		static ConsoleColor Lighter(ConsoleColor clr)
		{
			switch (clr)
			{
					case ConsoleColor.DarkBlue: return ConsoleColor.Blue;
					case ConsoleColor.DarkCyan: return ConsoleColor.Cyan;
					case ConsoleColor.DarkGreen: return ConsoleColor.Green;
					case ConsoleColor.DarkMagenta: return ConsoleColor.Magenta;
					case ConsoleColor.DarkRed: return ConsoleColor.Red;
					case ConsoleColor.DarkYellow: return ConsoleColor.DarkYellow;
					case ConsoleColor.Gray: return ConsoleColor.White;
					case ConsoleColor.DarkGray: return ConsoleColor.Gray;
					case ConsoleColor.Black: return ConsoleColor.DarkGray;
					default: return ConsoleColor.White;
			}
		}
		public static void Message(ConsoleColor c, object t, object m) { Message(c,"{0}: ",t,"{0}\n",m); }
		public static void Message(ConsoleColor c, object t, string fm, params object[] m) { Message(c,"{0}: ",t,fm,m); }
		public static void Message(ConsoleColor c, string ft, object t, string fm, params object[] m)
		{
			ConsoleColor fg = Console.ForegroundColor;
			ConsoleColor bg = Console.BackgroundColor;
	
			Console.ForegroundColor = Darker(c);
			Console.Write(ft,t);
			Console.ForegroundColor = c;
			Console.Write(fm,m);
	
			Console.ForegroundColor = fg;
			Console.BackgroundColor = bg;
		}
	
		public static void MessageR(object t, object m) { Message(ConsoleColor.Red,t,m); }
		public static void MessageR(string ft, object t, string fm, params object[] m) { Message(ConsoleColor.Red,ft,t,fm,m); }
		public static void MessageB(object t, object m) { Message(ConsoleColor.Blue,t,m); }
		public static void MessageB(string ft, object t, string fm, params object[] m) { Message(ConsoleColor.Blue,ft,t,fm,m); }
		public static void MessageG(object t, object m) { Message(ConsoleColor.Green,t,m); }
		public static void MessageG(string ft, object t, string fm, params object[] m) { Message(ConsoleColor.Green,ft,t,fm,m); }
		public static void MessageC(object t, object m) { Message(ConsoleColor.Cyan,t,m); }
		public static void MessageC(string ft, object t, string fm, params object[] m) { Message(ConsoleColor.Cyan,ft,t,fm,m); }
		public static void MessageM(object t, object m) { Message(ConsoleColor.Magenta,t,m); }
		public static void MessageM(string ft, object t, string fm, params object[] m) { Message(ConsoleColor.Magenta,ft,t,fm,m); }
		public static void MessageY(object t, object m) { Message(ConsoleColor.Yellow,t,m); }
		public static void MessageY(string ft, object t, string fm, params object[] m) { Message(ConsoleColor.Yellow,ft,t,fm,m); }
		public static void MessageGray(object t, object m) { Message(ConsoleColor.Gray,t,m); }
		public static void MessageGray(string ft, object t, string fm, params object[] m) { Message(ConsoleColor.Gray,ft,t,fm,m); }
		public static void MessageW(object t, object m) { Message(ConsoleColor.White,t,m); }
		public static void MessageW(string ft, object t, string fm, params object[] m) { Message(ConsoleColor.White,ft,t,fm,m); }
		#endregion
	}
}
