/* User: oIo * Date: 8/18/2010 * Time: 4:27 AM */
using System;
using System.Collections.Generic;
using System.Linq;
namespace System
{
	public class Option
	{
		public int IndexParent;
		public int Index;
		public string Value;
//		public string Value;
		public Option(int IndexParent, int Index, string Value)
		{
//			var processes =
//				from process in System.Diagnostics.Process.GetProcesses()
//				where process.ProcessName.StartsWith("s")
//				select new {process.Id, Name = process.ProcessName};
//
//			foreach (var process in processes)
//				Console.WriteLine(process);

			this.IndexParent = IndexParent;
			this.Index = Index;
			this.Value = Value;
		}
	}
	public class ArgDefinition
	{
		public readonly string[] Options;
		public string OptionsAttr { get { return string.Join(", ", this.Options); } }
		public readonly string Name;
		public readonly string[] Description;
		public string DescriptionAttr { get { return string.Join("\n",Description); } }
		public readonly string Example;
		
		public bool Contains(string option) { return Options.Contains(option); }
		
		public ArgDefinition(string name, string example, string description, params string[] tags)
		{
			this.Name = name;
			this.Description = new string[]{description};
			this.Example = example;
			this.Options = tags;
		}
		public ArgDefinition(string str)
		{
			Stack<String> ops = new Stack<string>(str.Split(':'));
			this.Options = ops.Pop().Trim(' ').Split(' ');
			ops = new Stack<string>(ops.Pop().Trim(' ').Split('\\').Reverse());
			this.Name=ops.Pop();
			this.Example = ops.Pop();
			if (ops.Count>0)
			{
				this.Description = ops.ToArray();
			}
			ops.Clear();
			ops = null;
		}
	}
	public class ExecutionSettings : IDisposable
	{
		public readonly string argTypesAtt = null;
		public List<ArgDefinition> definitions = new List<ArgDefinition>();
		public DictionaryList<int,Option> options = new DictionaryList<int, Option>();
		
		public bool HasKey(string Key)
		{
			List<Option> list = this[Key];
			if (list==null) return false;
//			list.Clear();
//			list = null;
			return true;
		}
		public string GetValue(string Key)
		{
			if (!HasKey(Key)) return null;
			return this[Key].First().Value;
		}
		public List<string> GetValues(string Key)
		{
			if (!HasKey(Key)) return null;
			List<string> l = new List<string>();
			foreach (Option o in this[Key]) l.Add(o.Value);
			return l;
		}
		
		#region Static
		static List<ArgDefinition> GetDefinitions(string argdefinitions)
		{
			List<ArgDefinition> l = new List<ArgDefinition>();
			foreach (string arg in argdefinitions.Split('\n'))
			{
				if (!string.IsNullOrEmpty(arg.Trim('\r','\n','\t',' '))) l.Add(new ArgDefinition(arg.Trim('\r','\n','\t',' ')));
			}
			return l;
		}
		static protected void GetHelp(params ArgDefinition[] defs)
		{
			foreach (ArgDefinition def in defs)
			{
				XLog.WriteY(def.OptionsAttr,": {0}\n",def.Name);
				string addto = "\n  * ";
				XLog.Write("  * ");
				XLog.WriteG("",string.Join(addto,def.Description ?? new string[]{""}));
				XLog.Write("\n");
			}
			XLog.Write("\n------------------------\n\nPress a key to quit.");
			
			Console.Read();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="typesAttribute"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		static public ExecutionSettings CheckSection(string optionsAttributeString, params string[] args)
		{
			ExecutionSettings config = new ExecutionSettings(optionsAttributeString,args);
			Stack<int> tagItems = new Stack<int>();
			int counter = 0;
			foreach (string arg in args)
			{
				string targ = arg.TrimStart('-','/');
				if (config.IsArgument(targ))
				{
					tagItems.Push(counter);
					config.options.CreateKey(counter);
				}
				else
				{
					int ic = tagItems.Peek();
					config.options[ic].Add(new Option(ic,counter,args[counter]));
				}
				counter++;
			}
			return config;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="typesAttribute"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		static public ExecutionSettings FromArgs(string typesAttribute, params string[] args)
		{
			ExecutionSettings config = CheckSection(typesAttribute, args);
			
			Console.WriteLine();
			Console.WriteLine("Beginning our parsing");
			Console.WriteLine("--------------------------------------------");
			// TODO: Implement Functionality Here
			
			// 1. find the configuration file
			// 2. database information (dbname, tablename)
			// 3. template-name
			foreach (int x in config.options.KeyArray)
			{
				Console.Write("Arg: #{0:d3} ",x);
				XLog.Write(ConsoleColor.Yellow,args[x],"\n");
				foreach (Option o in config.options[x]) Console.WriteLine("  + {0}",o.Value);
			}
			// any remaining elements are to be collected and looked at
			return config;
		}
		#endregion

		public string[] Arguments = null;
		public Dictionary<int,string> ArgumentIndex = new Dictionary<int, string>();
		
		public List<Option> this[string Key]
		{
			get
			{
				int index = -1;
				try {
				index = ArgumentIndex
						.Where( o => o.Value == Key )
						.First().Key;
				} catch { }
				return index == -1 ? null : options[index];
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		virtual public bool IsArgument(string tagname)
		{
			throw new NotImplementedException();
		}
		
		virtual public void PrintOptions(params string[] args)
		{
			foreach (int x in this.options.KeyArray)
			{
				XLog.Write("Arg: #{0:d3} ",x);
				XLog.Write(ConsoleColor.Yellow,args[x],"\n");
				foreach (Option o in this.options[x]) XLog.WriteLine("  + {0}",o.Value);
			}
		}
		
		void Initialize(params string[] args)
		{
			Stack<int> tagItems = new Stack<int>();
			int counter = 0;
			foreach (string arg in args)
			{
				string targ = arg.TrimStart('-','/');
				if (this.IsArgument(targ))
				{
					tagItems.Push(counter);
					this.options.CreateKey(counter);
					this.ArgumentIndex.Add(counter,targ);
				}
				else
				{
					int ic = tagItems.Peek();
					this.options[ic].Add(new Option(ic,counter,args[counter]));
				}
				counter++;
			}
		}
		
		public ExecutionSettings(string argsAttribute, params string[] args)
		{
			this.definitions = GetDefinitions(this.argTypesAtt = argsAttribute);
			Initialize(args);
			PrintOptions(args);
		}
		
		public void Dispose()
		{
			definitions.Clear();
			definitions = null;
			foreach (int i in options.Keys) options[i].Clear();
			options.Clear();
			options = null;
		}
	}
}
