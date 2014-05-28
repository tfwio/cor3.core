#pragma warning disable 1587,1591, 162
// OBSOLETE
/* oOo * 11/14/2007 : 10:22 PM */
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System
{
	namespace Cor3.Forms
	{
		[Serializable]
		/// <summary>
		/// <para>the MRU Item contains a function that creates a List of ToolStripItems.</para>
		/// <para>…Note that the ‘Tag’ property simply contains the Path of the file.</para>
		/// </summary>
		public class MRUItem
		{
			object tag = -1;
			
			public override bool Equals(object obj)
			{
				return this.FileName.Equals((obj as MRUItem).FileName);
			}
			public override int GetHashCode()
			{
				return FileName.GetHashCode();
			}

			public string ownerInfo 
			{
				get {
					if (tag==(object)-1) return string.Empty;
					return string.Format("\nOwner: {0}",Tag);
				}
			}
			public object Tag { get { return tag==(object)-1?string.Empty:tag; } }
			public string FileName { get { return Path.GetFileName(FilePath); } }
			public string ToolTip { get { return string.Format("‘{0}’\n{1}\n{2}{3}",FileName,DateTime.FromBinary(FileDate),Path.GetDirectoryName(FilePath),ownerInfo); } }
			
			[XmlAttribute] public string FilePath;
			[XmlAttribute] public string profile { get { return tag.ToString(); } set { tag = value; } }
			[XmlAttribute] public long FileDate = DateTime.Now.Ticks;

			// I'm thinking that this can be used to mark a Unicode file somehow
			// so that when the application goes to load a file, it can check up
			// on the remarks to find somthing useful for how to load the file?
		//		public string cmmt = string.Empty;
			public MRUItem()  { }
			public MRUItem(object ownner, string fileName) : this(fileName,DateTime.Now.ToBinary()) { }
			public MRUItem(string fileName) : this(fileName,DateTime.Now.ToBinary()) { }
			/// date should be: ?DateTime.ToString()?
			public MRUItem(string fileName, long dat) : this(fileName,dat,string.Empty) {}
			public MRUItem(string fileName, long dat, string comment)
			{
				FilePath=fileName; FileDate=dat;// cmmt=comment;
			}
			public ToolStripItem TsItem(EventHandler e)
			{
				ToolStripItem tsi = new ToolStripMenuItem(FileName,null,new EventHandler(e));
				tsi.Tag = string.Copy(FilePath);
				tsi.ToolTipText = this.ToolTip;
				return tsi;
			}
		}
	}

}
