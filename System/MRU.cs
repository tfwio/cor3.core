/* oOo * 11/14/2007 : 10:22 PM */
// OBSOLETE

using System;
using System.Collections.Generic;
using System.Cor3.Forms;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.IO
{
	/// <summary>
	/// A MRU Event Exposes the current path being added to the general list.
	/// </summary>
	public delegate void MruEvent(string path);
	
	/// <summary>
	/// The MRU class is far from perfect and for this reason is considered obsolete, however
	/// provides a good placeholder for a good MRU class to be designed—which is something that
	/// we're intent on re-constructing.
	/// </summary>
	[XmlRootAttribute("MRU")]
	[Obsolete]
	public class MRU // revision 13
		: SerializableList<MRUItem>
	{

		/// <summary>
		/// The default file-name used to save/load MRU items.
		/// </summary>
		/// <remarks>
		/// this file should be in respect to the application directory,
		/// and does not take Windows7 into consideration (so see any
		/// documentation you can find on how windows7 may automatically
		/// deal with placement of this file if the application directory
		/// is used).
		/// </remarks>
		protected static readonly string default_file_name = "mru.xml";

		/// <summary>
		/// see ‘default_file_name’
		/// </summary>
		/// <remarks>
		/// note: this class is in progress—the constructor should have the ability
		/// to rename the default_file_name.
		/// </remarks>
		[XmlIgnore] static public string filename { get { return Path.Combine(directory,default_file_name); } }
		/// <summary>
		/// 
		/// </summary>
		[XmlIgnore] public EventHandler eh;
		/// <summary>
		/// 
		/// </summary>
		[XmlIgnore] public Dictionary<string,MRUItem> ItemDict
		{
			get
			{
				Dictionary<string,MRUItem> dct = new Dictionary<string,MRUItem>();
				foreach (MRUItem i in Items)
				{
					dct.Add(i.FilePath,i);
				}
				return dct;
			}
		}
		/*———————————————————————————*/
		/// <summary>
		/// 
		/// </summary>
		[XmlIgnore] static public string directory = string.Empty;
		/// <summary>
		/// 
		/// </summary>
		[XmlIgnore] static bool Exists { get { return File.Exists(filename); } }
		/*———————————————————————————*/
		/// <summary>
		/// 
		/// </summary>
		public ToolStripItem[] ToolStripItems
		{
			get
			{
				List<ToolStripItem> ix = new List<ToolStripItem>();
				foreach (MRUItem i in Items) ix.Add(i.TsItem(eh));
				return ix.ToArray();
			}
		}
		/*———————————————————————————*/
		/// <summary>
		/// 
		/// </summary>
		void info() { Global.statG("NumItems (MRU): {0}",Items.Length); }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="date"></param>
		public void AddItem(string name, long date) {
			info();
			Dictionary<string,MRUItem> ti = ItemDict;
			if (ti.ContainsKey(name)) innerList.Remove(ti[name]);
			innerList.Insert(0,new MRUItem(name,date)); Save();
			ti.Clear(); ti=null;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		public void AddItem(string name) {
			AddItem(name,DateTime.Now.ToBinary()); Save();
		}
		/// <summary>
		/// 
		/// </summary>
		public void Save() { Serial.SerializeXml(filename,typeof(MRU),this); }
		/// <summary>
		/// The default constructor.
		/// </summary>
		public MRU() : base() {  }
		/// <summary>
		/// constructor.
		/// </summary>
		/// <param name="eventh">This event </param>
		/// <param name="basepath"></param>
		public MRU(EventHandler eventh,string basepath) : base() { this.eh = eventh; directory = basepath; }
		
		static public MRU Load(string basepath,EventHandler e)
		{
			directory = basepath;
			MRU moo = (File.Exists(filename)) ? Serial.DeSerialize<MRU>(filename):
				moo = new MRU(e,basepath);
			moo.eh = e;
			return moo;
		}
	}

}
