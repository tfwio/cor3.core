/* oOo * 11/14/2007 : 10:22 PM */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace System
{
	public partial class ControlUtil {
	
		#region ' EmumMenuItem<T> '
		static public void ToolStripDropdownEnum<T>(ToolStripDropDownButton btn, EventHandler handler)
		{
			List<ToolStripItem> list = new List<ToolStripItem>();
			foreach (object o in Enum.GetValues(typeof(T)))
			{
				if (btn.DropDownItems.ContainsKey(o.ToString())) continue;
				ToolStripItem tsi = new ToolStripMenuItem(o.ToString());
				tsi.Name = o.ToString();
				tsi.Tag = o;
				tsi.Click += handler;
				list.Add(tsi);
			}
			btn.DropDownItems.AddRange(list.ToArray());
		}
		static public void ToolStripDropdownEnum<T>(ToolStripDropDownButton btn, EventHandler handler, bool SortItems)
		{
			List<ToolStripItem> list = new List<ToolStripItem>();
			foreach (object o in Enum.GetValues(typeof(T)))
			{
				if (btn.DropDownItems.ContainsKey(o.ToString())) continue;
				ToolStripItem tsi = new ToolStripMenuItem(o.ToString());
				tsi.Name = o.ToString();
				tsi.Tag = o;
				tsi.Click += handler;
				list.Add(tsi);
			}
			if (SortItems) list.Sort(SortMenuItems);
			btn.DropDownItems.AddRange(list.ToArray());
		}
		static public int SortMenuItems(ToolStripItem ts0, ToolStripItem ts1)
		{
			return ts0.Text.CompareTo(ts1.Text);
		}
		static public ToolStripMenuItem EmumMenuItem<T>(string name,EventHandler ItemHandler)
		{
			ToolStripMenuItem tss = new ToolStripMenuItem(name);
			string[] names = Enum.GetNames(typeof(T));
			foreach (string nam in Enum.GetNames(typeof(T)))
			{
				T im = (T)Enum.Parse((typeof(T)),nam);
				ToolStripMenuItem tsii = ControlUtil.TSItem(nam,(T)(Enum.Parse(typeof(T),nam)),ItemHandler);
				tsii.Tag = im;
				tss.DropDownItems.Add(tsii);
			}
			return tss;
		}
		#endregion
		#region ' bool IsCk '
		static public bool IsCk(ToolStripMenuItem ts){ return ts.Checked = !ts.Checked; }
		#endregion
		#region ' TSBtn, TSBStyle '
		static public void TSBStyle(MenuStrip menu, ToolStripItemDisplayStyle ds) { foreach (ToolStripItem btn in menu.Items) { if (btn is ToolStripButton) btn.DisplayStyle = ds; } }
	
		static public ToolStripButton TSBtn(string N, ToolStripItemAlignment A) { ToolStripButton item = TSBtn(N); item.Alignment = A; return item; }
		static public ToolStripButton TSBtn(string N, EventHandler E, Padding P, ToolStripItemAlignment A) { ToolStripButton item = TSBtn(N,E,P); item.Alignment = A; return item; }
		static public ToolStripButton TSBtn(string N, EventHandler E, ToolStripItemAlignment A) { ToolStripButton item = TSBtn(N,E); item.Alignment = A; return item; }
		static public ToolStripButton TSBtn(string N, object T, EventHandler E, ToolStripItemAlignment A) { ToolStripButton item = TSBtn(N,T,E); item.Alignment = A; return item; }
		static public ToolStripButton TSBtn(string N, object T, EventHandler E, Padding P) { ToolStripButton item = TSBtn(N,T,E); item.Padding = P; return item; }
		static public ToolStripButton TSBtn(string N, object T, EventHandler E, Bitmap B) { ToolStripButton item = TSBtn(N,T,E); item.Image = (System.Drawing.Image)B; return item; }
		static public ToolStripButton TSBtn(string N, object T, EventHandler E, Image I) { ToolStripButton item = TSBtn(N,T,E); item.Image = I; return item; }
		static public ToolStripButton TSBtn(string N, EventHandler E,Padding P) { ToolStripButton item = TSBtn(N,E,null,P); return item; }
		static public ToolStripButton TSBtn(string N, EventHandler E) { ToolStripButton item = TSBtn(N); item.Click += new EventHandler(E); return item; }
		static public ToolStripButton TSBtn(string N, object T, EventHandler E) { ToolStripButton item = TSBtn(N,T); item.Click += new EventHandler(E); return item; }
		static public ToolStripButton TSBtn(string N, Image img, EventHandler E) { ToolStripButton item = TSBtn(N); item.Click += new EventHandler(E); item.Image = img; return item; }
		static public ToolStripButton TSBtn(string N, Image img, object T, EventHandler E) { ToolStripButton item = TSBtn(N,T); item.Click += new EventHandler(E); item.Image = img; return item; }
		static public ToolStripButton TSBtn(string N, Image img, object T, EventHandler E, ToolStripItemAlignment A) { ToolStripButton item = TSBtn(N,T); item.Click += new EventHandler(E); item.Image = img; item.Alignment = A; return item; }
		static public ToolStripButton TSBtn(string N, object T) { ToolStripButton item = new ToolStripButton(N); item.Tag = T; return item; }
		static public ToolStripButton TSBtn(string N) { ToolStripButton item = new ToolStripButton(N); return item; }
		static public ToolStripSplitButton TSBtn(string N,ToolStripItem[] btns) { ToolStripSplitButton item = new ToolStripSplitButton(N); item.DropDownItems.AddRange(btns); return item; }
		#endregion
		#region ' TSDd '
		static public ToolStripDropDownButton TSDd(string title, Image image, object tag, EventHandler handler, ToolStripItem[] items) { ToolStripDropDownButton tsmi = TSDd(title); if (image!=null) tsmi.Image = image; tsmi.DropDownItems.AddRange(items); tsmi.Click += handler; tsmi.Tag = tag; return tsmi; }
		static public ToolStripDropDownButton TSDd(string T, ToolStripItem[] items) { ToolStripDropDownButton item = TSDd(T); item.DropDownItems.AddRange(items); return item; }
		static public ToolStripDropDownButton TSDd(string T) { ToolStripDropDownButton item = new ToolStripDropDownButton(T); return item; }
		#endregion
		#region ' TSCondEnable '
		static public void TSCondEnable(ToolStripItem I, bool Condition) { TSCondEnable(I,Condition,false); }
		static public void TSCondEnable(ToolStripItem I, bool Condition, bool invert) { if (!invert) { if (Condition) I.Enabled = true; else I.Enabled = false; } else { if (Condition) I.Enabled = !true; else I.Enabled = !false; } }
		#endregion
		#region ' TSItem '
		//static public ToolStripMenuItem TSItem<T>(string ItemName, object ItemTag, ApiEvent<T> Handler) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Name = ItemName; Item.Tag = ItemTag; Item.Click += Handler; return Item; }
		static public ToolStripSeparator TSItem() { return new System.Windows.Forms.ToolStripSeparator(); }
		static public ToolStripSeparator TSItem(ToolStripItemAlignment A) { ToolStripSeparator item = TSItem(); item.Alignment = A; return TSItem(); }
		static public ToolStripMenuItem TSItem(string ItemName,Image img) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Image = img; Item.Name = ItemName; return Item; }
		static public ToolStripMenuItem TSItem(string ItemName) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Name = ItemName; return Item; }
		static public ToolStripMenuItem TSItem(string ItemName, EventHandler Handler) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Name = ItemName; Item.Click += Handler; return Item; }
		static public ToolStripMenuItem TSItem(string ItemName, Image img, object ItemTag, EventHandler Handler, ToolStripItem[] SubItems) { System.Windows.Forms.ToolStripMenuItem tsmi = TSItem(ItemName,img,ItemTag,Handler); tsmi.DropDownItems.AddRange(SubItems); return tsmi; }
		static public ToolStripMenuItem TSItem(string ItemName, Image img, object ItemTag, EventHandler Handler) { System.Windows.Forms.ToolStripMenuItem tsmi = TSItem(ItemName,ItemTag,Handler); if (img != null) tsmi.Image = img; return tsmi; }
		static public ToolStripMenuItem TSItem(string ItemName, object ItemTag, EventHandler Handler) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Name = ItemName; Item.Tag = ItemTag; Item.Click += Handler; return Item; }
		static public ToolStripMenuItem TSItem(string I, Image img, object T, EventHandler E, Keys K) { System.Windows.Forms.ToolStripMenuItem Item = new ToolStripMenuItem(I,img,E); Item.Tag = T; Item.ShortcutKeys = Keys.Control | K; Item.ShortcutKeyDisplayString = "Ctrl+"+K.ToString(); return Item; }
		static public ToolStripMenuItem TSItem(string I, object T, EventHandler E, Keys K) { System.Windows.Forms.ToolStripMenuItem Item = TSItem(I,T,E); Item.ShortcutKeys = Keys.Control | K; Item.ShortcutKeyDisplayString = "Ctrl+"+K.ToString(); return Item; }
		static public ToolStripMenuItem TSItem(string ItemName, object tag, EventHandler even, ToolStripItemAlignment align) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Tag = tag; Item.Alignment = align; Item.Click += even; Item.Name = ItemName; return Item; }
		#endregion
		#region ' TSSBtn '
		static public ToolStripSplitButton TSSBtn(string name, object tag, EventHandler bob,ToolStripItemAlignment align) { ToolStripSplitButton item = new ToolStripSplitButton(name,null,bob); item.Name = name; item.Tag = tag; item.Alignment = align; return item; }
		static public ToolStripSplitButton TSSBtn(string title, Image image, object tag, EventHandler handler, ToolStripItem[] items) { ToolStripSplitButton tsmi = TSSBtn(title,tag,handler,image); tsmi.DropDownItems.AddRange(items); return tsmi; }
		static public ToolStripSplitButton TSSBtn(string title, Image image, ToolStripItem[] items) { ToolStripSplitButton tsmi = TSSBtn(title,null,delegate{},image); tsmi.DropDownItems.AddRange(items); return tsmi; }
		static public ToolStripSplitButton TSSBtn(string name, object tag, EventHandler bob, Image img) { ToolStripSplitButton item = new ToolStripSplitButton(name,null,bob); item.Name = name; item.Tag = tag; item.Image = img; return item; }
		#endregion
	
	}
}
