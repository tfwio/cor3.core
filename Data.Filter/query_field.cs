/* User: oIo * Date: 8/18/2010 * Time: 4:27 AM */
#region User/License
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


namespace System
{
	using System.Collections.Generic;
	/// <summary>
	/// SQL String utility foundation.
	/// <para>(obsoleted by <see cref="System.SVar" />)</para>
	/// </summary>
	public class query_field
	{
		internal protected char[] trimChars = new char[]{'\r','\n'};
		internal protected string _value;
		internal protected const string _qfmt = "`{0}`";
		virtual protected string QFmt { get { return _qfmt; } }
		public string InnerString { get { return  _value; } }
		virtual public string Value { get { return  string.Format(QFmt,_value); } }
		virtual public char[] TrimChars { get { return trimChars; } }
	
		internal protected query_field(string inval) { _value = inval; }
		internal protected query_field() : this(string.Empty) {  }
		//		static public implicit operator %(query_field s){ return new query_field(string.Copy(s)); }
		static public implicit operator query_field(string s){ return new query_field(string.Copy(s)); }
		static public implicit operator string(query_field s){ return string.Copy(s._value); }
	}
}
