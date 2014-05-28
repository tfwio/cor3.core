/**
 * oIo * 3/8/2011 6:01 PM
 **/
using System;
using System.ConsoleUtil;
namespace System
{
	/// <summary>
	/// Description of Item.
	/// </summary>
	public struct Item
	{
		static public string GetName(Item itm)
		{
			switch (itm.Value)
			{
					case "d": case "decompile": return "decompile";
					case "o": case "output": return "output";
					default: return "Unknown";
			}
		}
		static public InputParameterType GetTypeName(Item itm)
		{
			switch (itm.Value)
			{
					case "d": case "decompile": return InputParameterType.Decompile;
					case "o": case "output": return InputParameterType.OutputFile;
					default: return InputParameterType.None;
			}
		}
		public string Name;
		public string Value;
		public bool IsMatch;
		public Item(string name, string value, bool match)
		{
			Name=name;
			Value=value;
			IsMatch=match;
		}
		static public implicit operator Item(Stump stp)
		{
			string nam = stp.Value;
			if (stp.IsMatch) nam = stp.m.Groups[2].Value;
			return new Item(stp.Name,nam,stp.IsMatch);
		}
		public override string ToString()
		{
			return string.Format("[Item Name={0}, Value={1}, IsMatch={2}]", Name, Value, IsMatch);
		}

	}
}
