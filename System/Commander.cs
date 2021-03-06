﻿#region User/License
// Copyright (c) 2005-2013 tfwroble
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion
// ----------------------------------------------------------------------------
// 'it just works'
// ----------------------------------------------------------------------------
// usage:
// [todo] web address or article
// ----------------------------------------------------------------------------
// why:
// you can look up command-line arguments fairly easy with very
// few lines of code.
// ----------------------------------------------------------------------------
// history:
// (1) in a old 'study-project' using sqlite to store 'resources' such as images and
// what-ever you might think to store in a resources or resx file.
// (2) added to my personal litte cor3 library (of dependency-less) cor3 library
// for use in a csharp command-line application/tool 'xrename' for batch rename
// of indexed (easily-sortable) files in a directory.
// ----------------------------------------------------------------------------

// TODO: BETTER DOCUMENTATION!!!
// best way to see how to use this is to see it in action with some examples.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System
{
	// feel free to optimize and/or
	class Commander
	{
		static public readonly char[] char_trimExt = {'*','.',' '};
		static public int ArgIndex = 0;
		internal List<string> Args, ArgsBackup;
		
        public void WaitForKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
		/// <summary>
		/// look up the first found index of the same argument.
		/// </summary>
		/// <param name="arguments">N arguments, all representing the same flag.</param>
		/// <returns>-1 when none found or the first found index.</returns>
		internal int GetIndex(params string[] arguments)
		{
			foreach (string argument in arguments)
			{
				if (Args.Contains(argument))
					return Args.IndexOf(argument);
			}
			return -1;
		}
		
		/// <summary>
		/// equivelant to 'GetValue(index,false);'
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		internal string PeekValue(int index)
		{
			return GetValue(index,false);
		}
		
		/// <summary>
		/// Get a value and delete from list of arguments: 'Args';
		/// Something like a Stack.Pop() operation.
		/// </summary>
		/// <param name="index">parameter index</param>
		/// <param name="andRemove">default is true.</param>
		/// <returns></returns>
		internal string GetValue(int index, bool andRemove=true)
		{
			string v = Args[index];
			if (andRemove) Args.RemoveAt(index);
			return v;
		}
		
		/// <summary>
		/// The following (param: getValue) statements apply only if the argument or tag is found.
		/// </summary>
		/// <param name="getValue">
		/// <para>True to return a value for the argument clearing both.</para>
		/// <para>False to clear the argument and return string.empty or null.</para>
		/// </param>
		/// <param name="attributes">
		/// <para>attributes to search for</para>
		/// <para>The first found attribute is processed and removed.</para>
		/// </param>
		/// <returns>
		/// (1) null if we have not found our argument.
		/// (2) string.Empty if we have found the argument (getValue=false).
		/// (3) string value if getValue=true and there is a value to return.
		/// </returns>
		internal string GetValue(bool getValue, params string[] attributes)
		{
			int index = -1;
			string returned = null;
			foreach (string attribute in attributes)
			{
				if (!Args.Contains(attribute)) continue;
				index = Args.FindIndex(x => x==attribute);
				
				if (index!=-1) Args.RemoveAt(index);
				else continue;
				
				// this could cause issues, but we leave it.
				if (Args.Count==index)   continue;
				if (Args[index][0]=='-') continue;
				if (Args[index][0]=='/') continue;
				
				// if we've gotten here, we get what we came for.
				if (getValue) { returned = Args[ index ]; Args.RemoveAt( index ); }
				else          { returned = string.Empty; break; }
				
			}
			return returned;
		}

		public bool HasFlag(string arg, bool clearFlag)
		{
			if (!HasValue(arg)) return false;
			int index = GetIndex(arg);
			
			Args.RemoveAt(index);
			return true;
		}
		
		/// <summary>
		/// this one is not ready yet.
		/// <para>We're looking up values for a provided flag.</para>
		/// </summary>
		/// <param name="attributes"></param>
		/// <returns></returns>
		internal List<string> GetValues(params string[] attributes)
		{
			int index = -1;
			List<string> values = new List<string>();
			
			foreach (string attribute in attributes)
			{
				// get the index of the attribute
				index = Args.FindIndex(x => x==attribute);
				
				// if found, assign index and remove first item at index
				if (index!=-1) {
					values.Add(Args[index]); // back up our current
					Args.RemoveAt(index);
				}
				
				// foreach-continue if no index
				else continue;
				
				while (true)
				{
					// break on next tag or end
					if (index == Args.Count) break;
					if (Args[index][0] == '-') break;
					if (Args[index][0] == '/') break;
					
					// continue if we've got arguments to count.
					string x = Args[index];
					Args.RemoveAt(index);
					values.Add(x.Trim('*','.',' '));
				}
			}
			return values;
		}
		
		public List<string> GetValuesForArgument(params string[] attributes)
		{
			return GetValuesForArgument(false, attributes);
		}
		
		// var finalize has not yet been implemented.
		List<string> GetValuesForArgument(bool finalize, params string[] attributes)
		{
			List<string> VALUE = new List<string>();
			foreach (var attr in attributes)
			{
				if (Args.Contains(attr)) {
					
					// get the index of the attribute
					int index = Args.FindIndex(x => x==attr);
					
					// iterate to the next for-loop point if necessary.
					if (index==-1) continue;
					
					// if found, assign index and remove first item at index
					// note that we always will find due to our 'args.contains()' usage.
					else if (index!=-1) {
//						VALUE.Add(Args[index]); // un-comment if we want the starting var-name
						Args.RemoveAt(index);
						VALUE.AddRange(GetValuesForIndex(index));
					}
					// foreach-continue if no index
					else continue;
					
				}
			}
			return VALUE;
		}
		
		public List<String> GetValuesForIndex(int index)
		{
			List<string> ITEMS = new List<string>();
			while (true) {
				if (index == Args.Count) break;
				if (Args[index][0] == '-') break;
				if (Args[index][0] == '/') break;
				string x = Args[index];
				Args.RemoveAt(index);
				
				Debug.WriteLine("adding: {0}",x);
				ITEMS.Add(x.Trim(char_trimExt));
			}
			return ITEMS;
		}
		
		/// <summary>
		/// use this method if
		/// (1) all known flags are stripped from arguments.
		/// </summary>
		/// <param name="attributes"></param>
		/// <returns></returns>
		List<string> GetLeftovers(params string[] attributes)
		{
			return GetValuesForArgument(true,attributes);
		}
		public string GetFlag(params string[] arg)
		{
			int index = GetIndex(arg);
			return (GetIndex(arg)!=-1) ? GetValue(true,arg) : null;
		}
		public bool HasValue(params string[] arg)
		{
			foreach (string a in arg) if (Args.Contains(a)) return true;
			return false;
		}
	}
}
