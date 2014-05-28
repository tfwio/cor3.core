/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 11/16/2013
 * Time: 6:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace System.Tasks
{
	public class NormalizePathOptions
	{
		#region Path Separator(s)
		public char PathSeparator {
			get {
				return pathSeparator;
			}
			set {
				pathSeparator = value;
				illegalPathSeparator = value == '/' ? '\\' : '/';
			}
		}

		char pathSeparator = '/';

		public char IllegalPathSeparator {
			get {
				return illegalPathSeparator;
			}
			set {
				illegalPathSeparator = value;
				pathSeparator = value == '/' ? '\\' : '/';
			}
		}

		char illegalPathSeparator = '\\';

		#endregion
		
		/// <summary>
		/// Weather or not to match case when comparing strings.
		/// We reccommend that you leave this at default=true.
		/// </summary>
		public bool MatchCase {
			get {
				return matchCase;
			}
			set {
				matchCase = value;
			}
		}

		bool matchCase = true;

		/// <summary>
		/// If set to true, no (known) Exception is thrown and
		/// any exception should be handled using ConsoleError/IO.
		/// </summary>
		public bool IsConsoleEnabled {
			get {
				return isConsoleEnabled;
			}
			set {
				isConsoleEnabled = value;
			}
		}

		bool isConsoleEnabled = false;

		public bool UseFullPathWhenShorter {
			get {
				return useFullPathWhenShorter;
			}
			set {
				useFullPathWhenShorter = value;
			}
		}

		bool useFullPathWhenShorter = false;
	}
}


