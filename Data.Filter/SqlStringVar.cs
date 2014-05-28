/* User: oIo * Date: 8/18/2010 * Time: 4:27 AM */
/* The directory these file is in should probably not be in this project. */
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
	/// Strips ‘\’.  Isn't this class supposed to do something more?
	/// <para>
	/// note that this class is generally used to represent a Field within a (SQL) Query-Expression.
	/// </para>
	/// </summary>
	public class SqlStringVar : SVar
	{
		/// <summary>
		/// a (T:char[]) character array containing characters to trim: <code>trimChars = new char[]{'\''};</code>
		/// </summary>
		static public new char[] trimChars = new char[]{'\''};
		/// <summary>
		/// An overridable version of static public field ‘trimChars’ (which should probably not be static).
		/// </summary>
		public override char[] TrimChars { get { return trimChars; } }
		/// <summary>
		/// Defaults to <code>base(string.Empty,SVarFormat.Quoted)</code>.
		/// <para>
		/// The default, parameterless constructor.
		/// </para>
		/// </summary>
		public SqlStringVar() : base(string.Empty,SVarFormat.Quoted) { }
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="input">The input string for the field value.</param>
		public SqlStringVar(string input) : base( input.Trim(trimChars) , SVarFormat.VarDollar ) { }
		public override string Value { get { return base.Value; } }
	
		static public implicit operator SqlStringVar(string s){ return new SqlStringVar(string.Copy(s.Trim(trimChars))); }
		static public implicit operator string(SqlStringVar s){ return string.Copy(s.Value); }
	}
}
