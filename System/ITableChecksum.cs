﻿/* oOo * 11/14/2007 : 10:22 PM */

using System;

namespace System.IO
{
	public interface ITableChecksum
	{
		long Check(long length, BinaryReader reader);
		//	long Calculate(long length, int pos, BinaryReader reader);
	}

}
