#pragma warning disable 1587,1591, 162
// OBSOLETE
/* oOo * 11/14/2007 : 10:22 PM */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.Cor3.Forms
{
	public class MRUClass : SerializableClass<MRUClass>
	{
		#region Static & Constant
		internal protected const string default_file_name = "mru.xml";
		internal protected static string default_directory_path = Global.GetFileInfo(Application.ExecutablePath).Directory.FullName;
		#endregion
		
		[XmlIgnore] public EventHandler toolstripitem_click_handler;
		
		protected internal List<MRUItem> innerList = new List<MRUItem>();
		[XmlElement]
		public MRUItem[] Items { get { return innerList.ToArray(); } set { innerList = new List<MRUItem>(value); } }
		
		#region Paths
		[XmlIgnore] internal protected string mru_directory_path = default_directory_path;
		[XmlIgnore] internal protected string mru_file_name = default_file_name;
		[XmlIgnore] virtual public string FilePath { get { return Path.Combine(mru_directory_path,mru_file_name); } }
		#endregion

		#region Path Information
		[XmlIgnore] internal protected DirectoryInfo DirectoryInfo { get { return new DirectoryInfo(mru_directory_path); } }
		[XmlIgnore] internal protected FileInfo FileInfo { get { return new FileInfo(FilePath); } }
		// —————————————————————————————
		[XmlIgnore] internal protected bool Exists { get { return File.Exists(FilePath); } }
		#endregion

		ToolStripItem MenuItemClear
		{
			get
			{
				return new ToolStripMenuItem("Clear Recent Items",null,eClearEm);
			}
		}
		void eClearEm(object sender, EventArgs args)
		{
			this.innerList.Clear();
			this.Save();
		}
		public ToolStripItem[] ToolStripItems
		{
			get
			{
				List<ToolStripItem> ix = new List<ToolStripItem>();
				ix.Add(MenuItemClear);
				ix.Add(new ToolStripSeparator());
				foreach (MRUItem i in innerList) ix.Add(i.TsItem(toolstripitem_click_handler));
				return ix.ToArray();
			}
		}
		internal protected void print_info() { Global.statG("NumItems (MRU): {0}",innerList.Count); }
		[XmlIgnore] public string InfoString { get{ return string.Format("NumItems (MRU): {0}",innerList.Count); } }

		/// <param name="mru_full_file_path">
		/// <para>if the file doesn't exist, then it should be created when serialization takes place.</para>
		/// </param>
		/// <param name="handler">
		/// <para>the default event, assigned to the created tool-strip items.</para>
		/// </param>
		public MRUClass(string mru_full_file_path, EventHandler handler) : base()
		{
			mru_directory_path = Path.GetDirectoryName(mru_full_file_path);
			mru_file_name = Path.GetFileName(mru_full_file_path);
			toolstripitem_click_handler = handler;
			MRUClass moo = new MRUClass();
			if (File.Exists(FilePath))
			{
				try {
					moo = Load(FilePath);
					Global.statR("{0} — {1}",moo,moo.Items.Length);
					Global.statRd("{0}",FilePath);
				} catch (Exception) {
//						Global.statR("{0} — {1}",ex.Message,FilePath);
					moo = new MRUClass(handler,mru_directory_path,mru_file_name);
				}/* finally {  }*/
			}
			else
			{
				Global.statR("File didn't exist");
				moo = new MRUClass(handler,mru_directory_path,mru_file_name);
			}
			Items = moo.Items;
		}
		public MRUClass() : this(null,default_directory_path,default_file_name) {}

		public MRUClass(EventHandler eventh, string initial_directory_path, string initial_mru_file_name) : base()
		{
			toolstripitem_click_handler = eventh;
			mru_file_name = initial_mru_file_name;
			mru_directory_path = initial_directory_path;
		}
		/// <param name="eventh">Attached to the toolstrip-menu-item reflecting the MRU-file</param>
		/// <param name="initial_directory_path">the default directory to store the mru file</param>
		public MRUClass(EventHandler eventh, string initial_directory_path)
			: this(eventh,initial_directory_path,default_file_name)
		{
		}
		
		#region Item Dictionary
		[XmlIgnore] public Dictionary<string,MRUItem> ItemDict
		{
			get
			{
				Dictionary<string,MRUItem> dct = new Dictionary<string,MRUItem>();
				foreach (MRUItem i in innerList)
				{
					dct.Add(i.FilePath,i);
				}
				return dct;
			}
		}
		// —————————————————————————————
		public void AddItem(string name, long date)
		{
			print_info();
			Dictionary<string,MRUItem> ti = ItemDict;
			if (ti.ContainsKey(name))
			{
				Global.statR("DID THE ITEM GET REMOVED?... {0}",innerList.Remove(ti[name]));
			}
			else Global.statR("NOT REMOVED");
			innerList.Insert(0,new MRUItem(name,date));
			innerList.Sort(SortByDate);
			Save();
			ti.Clear();
			ti=null;
		}
		public void AddItem(string name) { AddItem(name,DateTime.Now.ToBinary()); }
		int SortByDate(MRUItem a, MRUItem b)
		{
			return b.FileDate.CompareTo(a.FileDate);
		}
		#endregion
		virtual public void Save()
		{
			this.Save(FilePath,this);
		}
	}
}

