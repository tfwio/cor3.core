/**
 * oIo * 3/8/2011 6:01 PM
 **/
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ConsoleUtil;
namespace System
{
	/// <summary>
	/// Description of Stump.
	/// </summary>
	public class Stump
	{
		const string rex_arg1 = @"(/|-*)?([\w \\/.]*)(:)?";
		static readonly Regex r = new Regex(rex_arg1);
		public string Name;
		public string Value;
		public bool IsMatch = false;
		internal Match m;
		public List<ArgumentType> argtypes = new List<ArgumentType>();
		public List<string> strings = new List<string>();
		public List<Item> Items = new List<Item>();
		public Stump(string input, params string[] args)
		{
			m = r.Match(input);
			argtypes.Clear();
			strings.Clear();
			Name = m.Groups[0].Value;
			Value = m.Groups[0].Value;
			for (int i=0; i < m.Groups.Count; i++)
			{
				if (i==0) argtypes.Add(ArgumentType.capture);
				else if (m.Groups[i].Value=="/") { argtypes.Add(ArgumentType.param); IsMatch = true; }
				else if (m.Groups[i].Value=="-") { argtypes.Add(ArgumentType.param); IsMatch = true; }
				else if (m.Groups[i].Value==":") argtypes.Add(ArgumentType.ignore);
				else argtypes.Add(ArgumentType.value);
				if (IsMatch) strings.Add(m.Groups[i].Value);
				else strings.Add(Value = m.Value);
			}
		}
	}
}
