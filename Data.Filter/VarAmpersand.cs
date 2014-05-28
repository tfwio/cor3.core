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

using System.Collections.Generic;

namespace System
{
	
	public class VarAmpersand : SVar
	{
		static public new char[] trimChars = new Char[]{'@'};
		public override char[] TrimChars { get { return trimChars; } }
		protected override string QFmt { get { return "@{0}"; } }
		public VarAmpersand() : base(string.Empty,SVarFormat.VarAmpersand) { }
		public VarAmpersand(string input) : base( input.Trim(trimChars) , SVarFormat.VarAmpersand ) { }
		public VarAmpersand(string input, SVarFormat fmt) : base( input.Trim(trimChars) , fmt ) { }
		public override string Value { get { return base.Value; } }
		
		static public implicit operator VarAmpersand(string s){ return new VarAmpersand(string.Copy(s.TrimStart(trimChars)),SVarFormat.VarAmpersand); }
		static public implicit operator string(VarAmpersand s){ return string.Copy(s.Value); }
	}
}
