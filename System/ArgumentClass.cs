/**
 * oIo * 3/8/2011 6:03 PM
 **/
using System;
using System.Collections.Generic;
using System.ConsoleUtil;

namespace System
{
	/// ArgumentClass---I can't say I remember why I constructed this.
	/// Looks like it might be a 
	public class ArgumentClass : AssemblyDescription
	{
		/// <summary>
		/// Returns true if the InputParameterType.Decompile Flag is present.
		/// </summary>
		public bool HasDecompile { get { return Sections.ContainsKey(InputParameterType.Decompile); } }
		/// <summary>
		/// Returns true if the InputParameterType.Compile Flag is present.
		/// </summary>
		public bool HasCompile { get { return Sections.ContainsKey(InputParameterType.Compile); } }
		/// <summary>
		/// Returns true if the InputParameterType.Decompile and InputParameterType.Compile Flags are present.
		/// </summary>
		public bool HasKeys {  get { return HasDecompile & HasCompile; } }
		/// <summary>
		/// A dictionary of sections.
		/// </summary>
		public Dictionary<InputParameterType,List<Item>> Sections;
		
		/// <summary>
		/// Adds a section provided the provided <strong>key</strong>.
		/// </summary>
		/// <param name="key">A <see cref="InputParameterType" /> value.</param>
		void CreateSection(InputParameterType key)
		{
			if (!Sections.ContainsKey(key)) Sections.Add(key,new List<Item>());
		}
		
		/// <summary>
		/// Gets a section from the <see cref="Sections" /> dictionary.
		/// </summary>
		public List<Item> this[InputParameterType key] { get { return Sections[key]; } }

		/// <summary>
		/// The arguments supplied to the application.
		/// </summary>
		string[] args = null;
		
		/// <summary>
		/// a list of <see cref="ArgumentType"/> types.
		/// </summary>
		List<ArgumentType> argtypes = new List<ArgumentType>();
		
		/// <summary>
		/// This set of stumps is set inside the <see cref="GetParse(string[])" /> method.
		/// </summary>
		public List<Stump> stumps = new List<Stump>();
		
		/// <summary>
		/// another set of stumps.  This one is assigned within the constructor via
		/// usage of the <see cref="Stumpie" /> accessor/automator.
		/// </summary>
		List<Stump> sx = null;

		Stump CheckArgument(int index) { return CheckArgument(args[index]); }
		Stump CheckArgument(string index) { return new Stump(index); }

		void GetParse(params string[] arguments)
		{
			args = arguments;
			for (int i=0; i < args.Length;i++)
				stumps.Add(CheckArgument(i));
		}
		public ArgumentClass(params string[] arguments)
		{
			GetParse(arguments);
			sx = Stumpie;
		}
		/// <summary>
		/// (read only)  A set of stumps created a runtime.
		/// </summary>
		public List<Stump> Stumpie
		{
			get
			{
				Sections = new Dictionary<InputParameterType, List<Item>>();
				Console.ForegroundColor = ConsoleColor.White;
				Stack<Stump> Matches = new Stack<Stump>();
				Stump flat = null;
				InputParameterType sname = InputParameterType.Decompile;
				foreach (Stump def in stumps)
				{
					if (def.IsMatch)
					{
						Matches.Push(def);
						CreateSection(sname=Item.GetTypeName(def));
					}
					else
					{
						if (Matches.Count!=0)
						{
							Matches.Peek().Items.Add(def);
							this[sname].Add(def);
						}
						else
							// possibly, no token was used and we are dealing
							// with an input file.
						{
							flat = def;
							Console.ForegroundColor = ConsoleColor.White;
							CreateSection(sname=InputParameterType.Decompile);
							this[sname].Add(def);
						}
					}
				}
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("#\n# Finished\n#");
				List<Stump> sx = new List<Stump>(Matches.ToArray());
				sx.Reverse();
				return sx;
			}
		}
	}
}
