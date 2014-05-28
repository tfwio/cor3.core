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
	/// A syntax helper for interpreting and outputting a string value
	/// that represents a SqlServer Table or Field reference.
	/// </summary>
	/// <remarks>
	/// I'm not sure if this class is working perfectly.
	/// </remarks>
	public class SqlServerFieldString : SVar
	{
		/// <summary>
		/// 
		/// </summary>
		static public new char[] trimChars = new Char[]{'[',']'};
		/// <summary>
		/// 
		/// </summary>
		public override char[] TrimChars {
			get { return trimChars; }
		}
		/// <summary>
		/// 
		/// </summary>
		public SqlServerFieldString() : base(string.Empty,SVarFormat.VarSqlServer) { }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		public SqlServerFieldString(string input) : base( input.Trim(trimChars) , SVarFormat.VarDollar ) { }
		/// <summary>
		/// 
		/// </summary>
		public override string Value { get { return base.Value; } }
	
		/// <summary>
		/// Converts from a string to SqlServerFieldString.
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		/// if you send in the string string
		/// <code>string tablename = ‘tablename’; //
		/// string convertedTablename = (SqlServerFieldString)tablename; // convertedTablename is now: ‘[tablename]’
		/// </code>
		/// </returns>
		static public implicit operator SqlServerFieldString(string s){ return new SqlServerFieldString(string.Copy(s.Trim(trimChars))); }
		/// <summary>
		/// Converts from SqlServerFieldString to string
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		/// the string value (not sure, but I think the value will not contain braces)
		/// </returns>
		static public implicit operator string(SqlServerFieldString s){ return string.Copy(s.Value); }
	}
}
