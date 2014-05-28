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
/* oOo * 11/14/2007 : 10:22 PM */
using System;

namespace System.IO
{

	public class CheckSum : ITableChecksum {
		public long Check(long length, BinaryReader reader)
		{
			long len = length/4, i =-1, Sum = 0;
			while (i++ < len) Sum += reader.ReadInt32();
			return Sum;
		}
		//		public ULONG CalcTableChecksum(ULONG Table, ULONG Length)
		//		{
		//			ULONG Sum = 0L;
		//			public long cursor=0, data;
		//			ULONG *Endptr = Table+((Length+3) & ~3) / sizeof(ULONG);
		//			while (Table < EndPtr) Sum += *Table++;
		//			return Sum;
		//		}
	}
}
