/* oOo * 11/14/2007 : 10:22 PM */
using System.Collections.Generic;
using System.Windows.Forms;

namespace System.WTF
{
	/// <summary>
	/// This class is simply a rename of our standard DICT class.
	/// </summary>
	[Obsolete]
	public class Dict : DICT<object,object>
	{
		public Dict() : base() {}
		public Dict(IDictionary<object,object> d) : base(d) {}
		public Dict(params DictNode[] nodes) : base(nodes) {}
	}
	/// <summary>
	/// This tree helper is very old—yet had been over-used in some old code that
	/// had been written, so remains.
	/// </summary>
	[Obsolete]
	public partial class TreeUtil
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		static public TreeNode tree_find_first_node(TreeNode node)
		{
			TreeNode tn = node;
			while (tn.Parent!=null) tn = tn.Parent;
			return tn;
		}

		#region the old stuff
		#region ' ToTag '
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ctl">Acts on ToolStripItem,Menu,Control. All else is 'null'.</param>
		/// <returns></returns>
		static public object ToTag(object ctl)
		{
			object tag = null;
			if (ctl is ToolStripItem) return (ctl as ToolStripItem).Tag;
			else if (ctl is Menu) return (ctl as Menu).Tag;
			else if (ctl is Control) return (ctl as Control).Tag;
			return tag;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ctl">Acts on ToolStripItem,Menu,Control. All else is 'null'.</param>
		/// <returns></returns>
		static public T ToTag<T>(object ctl) { return (T)ToTag(ctl); }
		#endregion
		#region ' TnTag '
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tv"></param>
		/// <param name="tag"></param>
		/// <returns></returns>
		static public bool TnTag(TreeView tv, object tag) { foreach (TreeNode ts in tv.Nodes) if (ts.Tag == tag) return true; return false; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tn"></param>
		/// <param name="tag"></param>
		/// <returns></returns>
		static public bool TnTag(TreeNode tn, object tag) { foreach (TreeNode ts in tn.Nodes) if (ts.Tag == tag) return true; return false; }
		#endregion
		#region ' TnByTag '
		/// <summary>
		/// returns null if the tag is not in the root nodes, the treenode otherwise.
		/// </summary>
		static public TreeNode TnByTag(TreeView tv, object tag) { TreeNode tr = null; if (TnTag(tv,tag)){ foreach (TreeNode tn in tv.Nodes) { if (tn.Tag==tag) tr = tn; } } return tr; }
		/// <summary>
		/// returns null if the tag is not in the root nodes, the treenode otherwise.
		/// </summary>
		static public TreeNode TnByTag(TreeNode tn, object tag) { TreeNode tr = null; if (TnTag(tn,tag)){ foreach (TreeNode tc in tn.Nodes) { if (tc.Tag==tag) tr = tn; } } return tr; }
		#endregion
		#region ' TnInsGet '
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tv"></param>
		/// <param name="tag"></param>
		/// <param name="text"></param>
		/// <param name="ntag"></param>
		/// <returns></returns>
		static public TreeNode TnInsGet( TreeView tv, object tag, string text, object ntag ) { TreeNode tr = null; if (TnTag(tv,tag)) { return TnByTag(tv,tag); } else { tr = tv.Nodes.Add(text); tr.Tag = ntag; } return tr; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tn"></param>
		/// <param name="tag"></param>
		/// <param name="text"></param>
		/// <param name="ntag"></param>
		/// <returns></returns>
		static public TreeNode TnInsGet(TreeNode tn, object tag,string text, object ntag ) { TreeNode tr = null; if (TnTag(tn,tag)) { return TnByTag(tn,tag); } else { tr = tn.Nodes.Add(text); tr.Tag = ntag; } return tr; }
		#endregion
		#region ' TnTryInsert '
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tv"></param>
		/// <param name="tag"></param>
		/// <param name="text"></param>
		/// <param name="ntag"></param>
		/// <returns></returns>
		static public TreeNode TnTryInsert( TreeView tv, object tag, string text, object ntag ) { TreeNode tr = null; if (TnTag(tv,tag)) { tr = (TnByTag(tv,tag).Nodes.Add(text)); tr.Tag = ntag; } else { tr = tv.Nodes.Add(text); tr.Tag = ntag; } return tr; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tn"></param>
		/// <param name="tag"></param>
		/// <param name="text"></param>
		/// <param name="ntag"></param>
		/// <returns></returns>
		static public TreeNode TnTryInsert(TreeNode tn, object tag,string text, object ntag) { TreeNode tr = null; if (TnTag(tn,tag)) { tr = TnByTag(tn,tag).Nodes.Add(text); tr.Tag = ntag; } else { tr = tn.Nodes.Add(text); tr.Tag = ntag; } return tr; }
		#endregion
		#region ' TnFromString '
		/// <summary>
		/// [reserved] this is a placeholder—has not been implemented.
		/// </summary>
		/// <param name="tv"></param>
		/// <returns>ALWAYS RETURNS NULL</returns>
		[Obsolete]
		static public TreeNode TnFromString(TreeView tv)
		{
			return null;
		}
		#endregion
		#region ' TreeList '
		/// <summary>
		/// THIS FUNCTION DOES NOTHING!
		/// </summary>
		/// <param name="tv"></param>
		/// <returns></returns>
		static public string TreeList(TreeView tv)
		{
			foreach (TreeNode tn in tv.Nodes)
			{
				
			}
			return null;
		}
		#endregion
		#endregion
	}
}
