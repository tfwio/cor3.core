/* tfwroble : 11/14/2007 : 10:22 PM (probably much older then this) */
using System;
using System.Windows.Forms;

namespace System
{
	/// <summary>
	/// usage isn't reccomended for most of the ControlUtil.X.cs files here.
	/// </summary>
	public partial class ControlUtil
	{
		#region " Filter Types "
		public const string AllFiles = "All Files|*";
		public const string XmlFile = "Xml File|*.xml";
		public const string TextFile = "Text File|*.txt";
		
		public const string RawFile = "Raw|*.raw";
		public const string BinFile = "Bin|*.bin";

		public const string PngImage = "Png Image|*.png";
		public const string BmpImage = "Bitmap (Microsoft)|*.bmp";
		public const string GifImage = "GIF Image|*.gif";
		public const string JpegImage = "JPGE Image|*.jpg;*.jpeg";
		
		static public bool HasCat(string refr) { return HasCat(refr,'|'); }
		static public bool HasCat(string refr,char str) { return refr.IndexOf(str)!=-1; }
		static public bool HasCat(string refr,string str) { return refr.IndexOf(str)!=-1; }
		static public string Make(char sep,params string[] input)
		{
			string r = string.Empty;
			foreach (string s in input)
			{
				r = string.Concat(r,(!hs(s,sep)) ? sep.ToString():string.Empty,s);
			}
			return r.Trim(sep);
		}

		static public bool hs(string input, char split) { return input.IndexOf(split)!=-1; }
		static public string s0(string input, char split) { return input.Split(split)[0]; }
		static public string s1(string input, char split) { return input.Split(split)[1]; }
		static public string s2(string input, char split) { return input.Split(split)[2]; }

		public const string AS3File = "ActionScript3|*.as3";
		
		public const string WaveFile = "Wave File (Microsoft)|*.wav;*.wave";
		public const string AifFile = "Wave File (Apple)|*.aif;*.aiff;*.aifc";

		public const string SmfFile = "Standard Midi Format|*.smf";
		public const string MidiFile = "General Midi Format|*.mid;*.midi";

		public const string RmfFile = "Rich Midi Format|*.rmf";
		public static string WaveFiles { get { return string.Concat(WaveFile,"|",AifFile); } }
		public static string XmlFull { get { return string.Concat(XmlFile,"|",AllFiles); } }
		public static string MidiFiles { get { return string.Concat(SmfFile,"|",MidiFile,"|",RmfFile); } }

		public const string RtfFile = "Rich Text Format|*.rtf";
		public const string HtmlFiles = "Html Formats|*.htm;*.html";
		#endregion
		#region ' FGet '
		/// <summary>
		/// uses a OpenFileDialog to return a file
		/// </summary>
		/// <param name="F">File-Filter</param>
		/// <returns>string path</returns>
		static public string FGet(string F) {
			string relay = string.Empty;
			OpenFileDialog of = new OpenFileDialog();
			of.Filter = F;
			if (of.ShowDialog() == DialogResult.OK) relay = of.FileName;
			of.Dispose();
			of = null;
			return relay;
		}
		/// <summary>
		/// uses a OpenFileDialog to return a file
		/// </summary>
		/// <param name="F">File-Filter</param>
		/// <param name="T">Dialog Title</param>
		/// <returns></returns>
		static public string FGet(string F, string T) {
			string relay = string.Empty;
			OpenFileDialog of = new OpenFileDialog();
			of.Filter = F;
			of.Title = T;
			if (of.ShowDialog() == DialogResult.OK)
				relay = of.FileName;
			of.Dispose();
			of = null;
			return relay;
		}
		#endregion
		#region ' FSave '
		static public string FSave(string S, string F, string T, out string [] filenames)
		{
			string relay = string.Empty;
			SaveFileDialog sf = new SaveFileDialog();
			if (!string.IsNullOrEmpty(S)) sf.FileName = S;
			if (!string.IsNullOrEmpty(F)) sf.Filter   = F;
			if (!string.IsNullOrEmpty(T)) sf.Title    = T;
			relay = sf.FileName;
			if (sf.ShowDialog() == DialogResult.OK) {
				relay = sf.FileName;
				filenames = sf.FileNames;
			}
			else filenames = null;
			sf.Dispose();
			return relay;
		}
		static public string FSave(string S,string F, string T)
		{
			string relay = string.Empty;
			SaveFileDialog sf = new SaveFileDialog();
			if (!string.IsNullOrEmpty(S)) sf.FileName = S;
			if (!string.IsNullOrEmpty(F)) sf.Filter   = F;
			if (!string.IsNullOrEmpty(T)) sf.Title    = T;
			if (sf.ShowDialog() == DialogResult.OK) {
				relay = sf.FileName;
			}
			sf.Dispose();
			return relay;
		}
		static public string FSave(string F) {
			return FSave(null,F,null);
		}
		static public string FSave(string F, string T) {
			return FSave(null,F,T);
		}
		#endregion
	}
}
