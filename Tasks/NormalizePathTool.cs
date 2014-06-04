/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 11/16/2013
 * Time: 6:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;

// http://stackoverflow.com/questions/1395205/better-way-to-check-if-path-is-a-file-or-a-directory-c-net

namespace System.Tasks
{
	/// <summary>
	/// Description of NormalizePathTask.
	/// </summary>
	public /*partial*/ class NormalizePathTool
	{
		#region Constant
		const string template = @"NormalizePathTool
=================

GENERALLY:
  Similar Index    = {0}
  Target Length    = {1}
  Target Remainder = {2}
  Base   Length    = {16}
  Base   Remainder = {17}

SIMILARITY:
  {11}

TARGET (DESTINATION):
  Disk  = '{14}'
  Path  = '{9}'
  IsDir =  {3,5}, Exists = {4,5}
  After = '{12}'
  FName = '{5}'

BASE (CONTEXT):
  Disk  = '{15}'
  Path  = '{9}'
  IsDir =  {6,5}, Exists = {7,5}
  After = '{14}'
  FName = '{8}'

RESULT:
  Full  = {25}
  FullIsShorter  = {27} (Longer)
  Exist = {24}
  ResultCount = {20} / ResultFullCount = {23}
  ResultPath = {22}

";
///*00*/ LastSimilarIndex + added,
///*01*/ TargetList.Count,
///*02*/ TargetList.Count-(LastSimilarIndex + added),
///*03*/ IsTargetDir,
///*04*/ TargetExists,
///*05*/ TargetFileName,
///*06*/ IsBaseDir,
///*07*/ BaseExists,
///*08*/ baseFileName,
///*09*/ TargetPath,
///*10*/ TargetPathLength,
///*11*/ BasePath,
///*12*/ CommonPath,
///*13*/ CommonPathLength,
///*14*/ TargetRemainderPath,
///*15*/ TargetRemainderPathLength,
///*16*/ BaseRemainderPath,
///*17*/ BaseRemainderPathLength,
///*18*/ TargetDisk,
///*19*/ BaseDisk,
///*20*/ BaseList.Count,
///*21*/ BaseList.Count-(LastSimilarIndex + added),
///*22*/ ResultPath,
///*23*/ ResultPathLength,
///*24*/ ResultPathExists,
///*25*/ ResultPathFull,
///*26*/ ResultPathFullLength,
///*27*/ ResultIsLonger
		#endregion
		
		#region Props
		static public readonly NormalizePathOptions DefaultOptions = new NormalizePathOptions
		{
			MatchCase              = true,
			IsConsoleEnabled       = false,
			UseFullPathWhenShorter = false
		};
		
		/// <summary>
		/// The path result of our inquary.
		/// </summary>
		public string ResultPath { get; private set; }
		public int ResultPathLength { get; private set; }
		/// <summary>
		/// The path result of our inquary.
		/// </summary>
		public string ResultPathFull { get; private set; }
		public int ResultPathFullLength { get; private set; }
		
		/// <summary>
		/// The path result of our inquary.
		/// </summary>
		public FileInfo ResultPathFileInfo { get; private set; }
		
		/// <summary>
		/// The path result of our inquary.
		/// </summary>
		public bool ResultPathExists { get; private set; }
		
		/// <summary>
		/// Indicates that `ResultPath` is longer then the actual `TargetPath`.
		/// <para>If Options.UseFullPathWhenShorter is true and indeed the full
		/// TargetPath is shorter, then `ResultPath` will be set to the full
		/// `TargetPath`.</para>
		/// </summary>
		public bool ResultIsLonger { get; private set; }
		
		/// <summary>
		/// Destination path.  The file we are providing a path for.
		/// </summary>
		public string TargetPath {
			get { return targetPath; }
			set { targetPath = value; }
		} string targetPath;
		public int TargetPathLength { get;set;}
		
		string TargetDisk, BaseDisk;
		
		/// <summary>
		/// Context/Start-path.
		/// </summary>
		public string BasePath {
			get { return basePath; }
			set { basePath = value; }
		} string basePath;
		public int BasePathLength { get;set; }
		
		/// <summary>
		/// if TargetPath points to a file, we strip it from the TargetPath and put it here.
		/// </summary>
		public string TargetFileName {
			get { return targetFileName; }
			set { targetFileName = value; }
		} string targetFileName;
		
		/// <summary>
		/// if BasePath points to a file, we strip it from the BasePath and put it here.
		/// </summary>
		public string BaseFileName {
			get { return baseFileName; }
			set { baseFileName = value; }
		} string baseFileName;
		
		string CommonPath, TargetRemainderPath, BaseRemainderPath;
		int CommonPathLength, TargetRemainderPathLength, BaseRemainderPathLength;
		
		public NormalizePathOptions Options { get; private set; }
		
		List<string> TargetList, BaseList;
		List<string> CommonList          = new List<string>();
		List<string> TargetRemainderList = new List<string>();
		List<string> BaseRemainderList   = new List<string>();
		
		int  IntCompare        = 0;
		bool IsEqualLength     = false;
		bool IsBaseLonger      = false;
		
		bool TargetExists      = false;
		bool IsTargetFile      = false;
		bool IsTargetDir       = false;
		bool IsTargetRooted    = false;
		
		bool BaseExists        = false;
		bool IsBaseFile        = false;
		bool IsBaseDir         = false;
		bool IsBaseRooted      = false;
		
		int MaxBounds          = 0;
		int LastSimilarIndex   = 0;
		
		// 1 to normalize zero-inclusive index.
		const int added = 1;
		#endregion
		
		#region Bool Checks
		bool IsPathRooted(string path)
		{
			return path !=null && path.Length >= 2 && path[1]==':';
		}
		bool IsFilePath(string path)
		{
			if (File.Exists(path)) return true;
			if (Directory.Exists(path)) return false;
			return false;
		}
		bool IsDirectoryPath(string path)
		{
			if (File.Exists(path)) return false;
			if (Directory.Exists(path)) return true;
			return false;
		}
		/// <summary>
		/// Get a value indicating weather or not l (length) is contained
		/// within the bounds of a list.
		/// </summary>
		/// <param name="a">Generic list</param>
		/// <param name="l">Length</param>
		/// <returns>true if contained, false if otherwise.</returns>
		bool IsBound(IList<string> a, int l) { return l < a.Count; }
		/// <summary>
		/// only compares against string values contained.
		/// if you send in an index point not contained in the array, results in error.
		/// </summary>
		/// <param name="a">array a</param>
		/// <param name="b">array b</param>
		/// <param name="i">array index position</param>
		/// <returns></returns>
		bool CompareSegment(IList<string> a, IList<string> b, int i)
		{
			if (Options.MatchCase && a[i]==b[i]) { return true; }
			else if (a[i].ToLower()==b[i].ToLower()) { return true; }
			
			return false;
		}
		#endregion
		
		string Pad(string input, int l, bool isLeftPad = true)
		{
			string output = input;
			for (int i = 0; i < l; i++) output = isLeftPad ? string.Concat("../",output) : string.Concat(output,"/..");
			return output;
		}
		
		static Func<NormalizePathTool,string,string> TargetPathAction = (tool,fname)  => tool.TargetPath.Replace(string.Concat("/",fname),string.Empty);
		
		/// <summary>
		/// hehe
		/// </summary>
		public void Run()
		{
			// TARGET
			TargetList         = new List<string>(this.targetPath.Split('/'));
			IsTargetRooted     = IsPathRooted(this.targetPath);
			IsTargetFile       = IsFilePath(this.targetPath);
			IsTargetDir        = IsDirectoryPath(this.targetPath);
			TargetExists       = IsTargetDir ? Directory.Exists(this.targetPath) : File.Exists(this.targetPath);
			TargetDisk         = IsTargetRooted ? TargetList[0] : string.Empty;
			if (IsTargetFile)
			{
				int index      = TargetList.Count-1;
				TargetFileName = TargetList[index];
				TargetList     . RemoveAt(index);
				TargetPath     = TargetPathAction(this,TargetFileName);
				TargetPathLength = targetPath.Split(Options.PathSeparator).Length;
			}
			// BASE
			BaseList           = new List<string>(this.basePath.Split('/'));
			IsBaseRooted       = IsPathRooted(this.basePath);
			IsBaseFile         = IsFilePath(this.basePath);
			IsBaseDir          = IsDirectoryPath(this.basePath);
			BaseExists         = IsTargetDir ? Directory.Exists(this.targetPath) : File.Exists(this.targetPath);
			BaseDisk           = IsBaseRooted ? BaseList[0] : string.Empty;
			if (IsBaseFile)
			{
				int index      = BaseList.Count-1;
				baseFileName   = BaseList[index];
				BaseList       . RemoveAt(index);
				BasePath       = BasePath.Replace(string.Concat("/",BaseFileName),string.Empty);
				BasePathLength = BasePath.Split(Options.PathSeparator).Length;
			}
			// INTS
			IntCompare         = TargetList.Count - BaseList.Count;
			IsEqualLength      = IntCompare == 0;
			IsBaseLonger       = IntCompare < 0;
			MaxBounds          = Math.Max(TargetList.Count, BaseList.Count);
			LastSimilarIndex   = 0;
			
			if (!(IsTargetRooted || IsBaseRooted) && Options.IsConsoleEnabled)
			{
				Console.Error.WriteLine("Both paths must begin with drive-letter + semi-colon. (EG: c:/)");
				Console.Error.WriteLine("EG: 'c:/'");
				Console.Error.WriteLine();
				return;
			}
			else if (!(IsTargetRooted || IsBaseRooted) && !Options.IsConsoleEnabled)
			{
				if (!IsTargetRooted) throw new PathNotRootedException(targetPath);
				else if (!IsBaseRooted) throw new PathNotRootedException(basePath);
				return;
			}
			
			for (int i = 0; i < MaxBounds; i++)
			{
				if (!IsBound(TargetList,i)) break;
				if (!IsBound(BaseList,  i)) break;
				if (CompareSegment(TargetList,BaseList,i)) LastSimilarIndex = i;
			}
			
			for (int i=0; i <= LastSimilarIndex; i++) CommonList.Add(TargetList[i]);
			if (LastSimilarIndex > TargetList.Count) return;
			for (int i=LastSimilarIndex+1; i < TargetList.Count; i++) TargetRemainderList.Add(TargetList[i]);
			for (int i=LastSimilarIndex+1; i < BaseList.Count; i++) BaseRemainderList.Add(BaseList[i]);
			
			CommonPath = string.Join("/",CommonList.ToArray());
			CommonPathLength = CommonPath.Split(Options.PathSeparator).Length;
			
			TargetRemainderPath = string.Join("/",TargetRemainderList.ToArray());
			TargetRemainderPathLength = TargetRemainderPath.Split(Options.PathSeparator).Length;
			
			BaseRemainderPath = string.Join("/",BaseRemainderList.ToArray());
			BaseRemainderPathLength = BaseRemainderPath.Split(Options.PathSeparator).Length;
			
			ResultPath = IsTargetFile ? Path.Combine(TargetRemainderPath,TargetFileName) : TargetRemainderPath;
			ResultPathLength = ResultPath.Split(Options.PathSeparator).Length;
			string temp = Pad(ResultPath,BaseList.Count-(LastSimilarIndex + added));
			ResultPath = NormalizeSeparators(temp);
			ResultPathFull = NormalizeSeparators(Path.Combine(BasePath,ResultPath));
			ResultPathFullLength = ResultPathFull.Split(Options.PathSeparator).Length;
			
			if (ResultPath.Length > (TargetPath.Length+1+TargetFileName.Length) && Options.UseFullPathWhenShorter) {
				ResultIsLonger = true;
				ResultPath = TargetPath;
				ResultPathFull = ResultPath;
			}
			
			ResultPathExists = IsTargetFile ? File.Exists(ResultPathFull) : Directory.Exists(ResultPathFull);
			
			if (Options.IsConsoleEnabled) PrintInfo();
			
		}
		
		string NormalizeSeparators(string input)
		{
			return input.Replace(Options.IllegalPathSeparator,Options.PathSeparator);
		}
		
		/// <summary>
		/// both input paths must contain the root element such as 'c:'.
		/// <para>target path is the one that is returned only it is returned relating to/from the base-path.</para>
		/// <para>another way to look at this process is traveling from pathBase to pathTarget.</para>
		/// </summary>
		/// <param name="pathTarget">The path we're traveling to.</param>
		/// <param name="pathBase">The path we're starting out at.</param>
		public NormalizePathTool(string pathTarget, string pathBase, NormalizePathOptions options)
		{
			this.Options       = options;
			this.targetPath    = NormalizeSeparators(pathTarget);
			this.basePath      = NormalizeSeparators(pathBase);
		}
		
//	}
//	partial public class NormalizePathTask
//	{
		void PrintInfo()
		{
			Console.Write(
				template,
				/*00*/ LastSimilarIndex + added,
				/*01*/ TargetList.Count,
				/*02*/ TargetList.Count-(LastSimilarIndex + added),
				/*03*/ IsTargetDir,
				/*04*/ TargetExists,
				/*05*/ TargetFileName,
				/*06*/ IsBaseDir,
				/*07*/ BaseExists,
				/*08*/ baseFileName,
				/*09*/ TargetPath,
				/*10*/ TargetPathLength,
				/*11*/ BasePath,
				/*12*/ CommonPath,
				/*13*/ CommonPathLength,
				/*14*/ TargetRemainderPath,
				/*15*/ TargetRemainderPathLength,
				/*16*/ BaseRemainderPath,
				/*17*/ BaseRemainderPathLength,
				/*18*/ TargetDisk,
				/*19*/ BaseDisk,
				/*20*/ BaseList.Count,
				/*21*/ BaseList.Count-(LastSimilarIndex + added),
				/*22*/ ResultPath,
				/*23*/ ResultPathLength,
				/*24*/ ResultPathExists,
				/*25*/ ResultPathFull,
				/*26*/ ResultPathFullLength,
				/*27*/ ResultIsLonger
//				/*00*/ LastSimilarIndex + added,
//				/*01*/ TargetList.Count,
//				/*02*/ TargetList.Count-(LastSimilarIndex + added),
//				/*03*/ IsTargetDir,
//				/*04*/ TargetExists,
//				/*05*/ TargetFileName,
//				/*06*/ IsBaseDir,
//				/*07*/ BaseExists,
//				/*08*/ baseFileName,
//				/*09*/ TargetPath,
//				/*10*/ TargetPathLength,
//				/*11*/ BasePath,
//				/*12*/ CommonPath,
//				/*13*/ CommonPathLength,
//				/*14*/ TargetRemainderPath,
//				/*15*/ TargetRemainderPathLength,
//				/*16*/ BaseRemainderPath,
//				/*17*/ BaseRemainderPathLength,
//				/*18*/ TargetDisk,
//				/*19*/ BaseDisk,
//				/*20*/ BaseList.Count,
//				/*21*/ BaseList.Count-(LastSimilarIndex + added),
//				/*22*/ ResultPath,
//				/*23*/ ResultPathLength,
//				/*24*/ ResultPathExists,
//				/*25*/ ResultPathFull,
//				/*26*/ ResultPathFullLength,
//				/*27*/ ResultIsLonger
			);
		}
	}
}
