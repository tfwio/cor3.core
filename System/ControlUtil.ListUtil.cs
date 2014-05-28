/* oOo * 11/14/2007 : 10:22 PM */
using System;
using System.Text;
using System.Windows.Forms;

namespace System
{
	/// <summary>
	/// listview division of the ControlUtil
	/// </summary>
	public partial class ControlUtil
	{
		#region ' lvcols '
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="columns"></param>
		static public void lvcols(ListView lv, params string[] columns) {
			lv.Columns.Clear();
			foreach (string str in columns) { lv.Columns.Add(str);  }
		}
		#endregion
		#region ' lvsize '
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="style"></param>
		static public void lvsize(ListView lv, ColumnHeaderAutoResizeStyle style) {
			try { foreach (ColumnHeader ch in lv.Columns) ch.AutoResize(style);
			} catch {}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="style"></param>
		static public void lvsize(ListView[] lv, ColumnHeaderAutoResizeStyle style) { for (int i=0;i<lv.Length;i++) lvsize(lv[i],style); }
		#endregion
		#region ' lVisi '
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="flag"></param>
		static public void lVisi(ListView lv, bool flag) { lv.Visible = flag; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lvz"></param>
		/// <param name="flag"></param>
		static public void lVisi(ListView[] lvz, bool flag) { foreach (ListView lv in lvz) lv.Visible = flag; }
		#endregion
		#region ' lvAG '
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="groupName"></param>
		public static void lvAG(ListView lv, string groupName) { if (lv.Groups[groupName]==null) lv.Groups.Add(groupName,groupName); }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="groupName"></param>
		/// <param name="itemText"></param>
		/// <returns></returns>
		public static ListViewItem lvAG(ListView lv, string groupName, string itemText) { lvAG(lv,groupName); ListViewItem lvi = lv.Items.Add(itemText); lvi.Group = lv.Groups[groupName]; return lvi; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="groupName"></param>
		/// <param name="itemText"></param>
		/// <param name="ndx"></param>
		/// <returns></returns>
		public static ListViewItem lvAG(ListView lv, string groupName, string itemText, int ndx) { lvAG(lv,groupName); ListViewItem lvi = lv.Items.Add(itemText,ndx); lvi.Group = lv.Groups[groupName]; return lvi; }
		#endregion
		#region ' LvCh '
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="item"></param>
		public static void LvCh(ListView lv, params ColumnHeader[] item) { lv.Columns.AddRange(item); }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="item"></param>
		public static void LvCh(ListView lv, params string[] item) { foreach (string i in item) lv.Columns.Add(i); }
		#endregion
		#region ' LvAdd ' (also old-as .Net v1.0-then upgraded to 2 or upgrading?.. don-remember)
		// try not to reference anything old like this
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="lvi"></param>
		public static void LvAdd(ListView lv, ListViewItem[] lvi) { lv.Items.AddRange(lvi); }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="lvi"></param>
		/// <returns></returns>
		public static ListViewItem LvAdd(ListView lv, ListViewItem lvi) { return lv.Items.Add(lvi); }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public static ListViewItem LvAdd(ListView lv, string item) { return lv.Items.Add(item); }
		#endregion
		#region ' addlv ' (also old)
		/// <summary>
		/// I think this started around CSMID, but elaborated here.
		/// </summary>
		/// <param name="lvx"></param>
		/// <param name="ast"></param>
		/// <param name="colar"></param>
		/// <param name="indx"></param>
		/// <remarks>Many of these functions were written originally as a helper for .Net v1.0</remarks>
		public static void addlv(ListView lvx, string[] ast, System.Drawing.Color colar, int indx) { ListViewItem lvi = new ListViewItem(ast[0], indx); lvi.BackColor=colar; for (int i=1;i<ast.Length;i++) lvi.SubItems.Add(ast[i]); lvx.Items.Add(lvi); }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lvx"></param>
		/// <param name="ast"></param>
		/// <param name="colar"></param>
		public static void addlv(ListView lvx, string[] ast, System.Drawing.Color colar) { ListViewItem lvi = new ListViewItem(ast[0]); lvi.BackColor=colar; for (int i=1;i<ast.Length;i++) lvi.SubItems.Add(ast[i]); lvx.Items.Add(lvi); }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lvx"></param>
		/// <param name="ast"></param>
		/// <param name="indx"></param>
		public static void addlv(ListView lvx, string[] ast, int indx)
		{
			ListViewItem lvi = new ListViewItem(ast[0], indx);
			for (int i=1;i<ast.Length;i++)
			{
				ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
				lvsi = lvi.SubItems.Add(ast[i]);
				lvsi.Tag = i;
			}
			lvx.Items.Add(lvi); }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lvx"></param>
		/// <param name="ast"></param>
		/// <returns></returns>
		public static ListViewItem addlv(ListView lvx, string[] ast)
		{
			ListViewItem lvi = new ListViewItem(ast[0]);
			for (int i=1;i<ast.Length;i++) lvi.SubItems.Add(ast[i]);
			lvx.Items.Add(lvi); return lvi;
		}
		#endregion
	
		#region ' lvCodePage ' — must be from when I was looking more into unicode
		/// <summary>
		/// Provides a set of columns: ('CodePage','Name','Display Name')<br/>
		/// to the destination ListView.
		/// </summary>
		/// <remarks>
		/// Equivelant to calling ‘lvCodePage(lv,false)’
		/// </remarks>
		/// <param name="lvCodePages"></param>
		static public void lvCodePage(ListView lvCodePages){lvCodePage(lvCodePages,false);}
		/// <summary>
		/// Provides a set of columns: ('CodePage','Name','Display Name')<br/>
		/// to the destination ListView.
		/// </summary>
		/// <param name="lvCodePages">The ListView whose content is modified.</param>
		/// <param name="UseTag">indicates weather or not to assign the code page to the ListViewItem.Tag property.</param>
		static public void lvCodePage(ListView lvCodePages, bool UseTag)
		{
			lvCodePages.View = View.Details;
			lvCodePages.FullRowSelect = true;
			lvCodePages.Clear();
			ControlUtil.lvcols(lvCodePages,new string[]{"CodePage", "Name", "Display Name"});
			foreach (EncodingInfo E in Encoding.GetEncodings())
			{
				ListViewItem lvi = addlv(
					lvCodePages,
					new string[]{E.CodePage.ToString(),E.Name,E.DisplayName}
				);
				if (UseTag) lvi.Tag = E.CodePage;
			}
			ControlUtil.lvsize(lvCodePages, ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		#endregion
	}
}
