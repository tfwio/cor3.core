//using System;

namespace System.Tools
{
	static void KeyGen(string AsmName, string CsAssemblyInfo, string BinKey)
	{
		byte[] bytes = System.IO.File.ReadAllBytes(BinKey);
		string cat = AsmName+",PublicKey=";
		foreach (byte b in bytes) cat += b.ToString("X2");
		string csasmnfo = System.IO.File.ReadAllText(CsAssemblyInfo);
		string csasmnfo_out = csasmnfo.Replace("@key",cat);
		System.IO.File.Delete(CsAssemblyInfo);
		System.IO.File.WriteAllText(CsAssemblyInfo,csasmnfo_out);
		bytes = null;
		cat = null;
	}
}