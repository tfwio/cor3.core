#region User/License
// Copyright (c) 2005-2013 tfwroble
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace System.Internals
{
	// for IViewPoint<...>
	using Keys = System.Windows.Forms.Keys;
	using ToolStripItem = System.Windows.Forms.ToolStripItem;

	/// <summary>
	/// </summary>
	public /*partial*/ class UserView<TParent> : UserView where TParent:System.Windows.Forms.Form
	{
		#region Properties
		/// <summary>
		/// </summary>
		virtual public TParent ViewForm
		{
			get;
			set;
		}
		#endregion
	}
	
	/// <summary>
	/// This class is just a placeholder for a view, so UserView types
	/// can be enumerated from this assembly.
	/// </summary>
	public /*partial*/ class UserView : System.Windows.Forms.UserControl
	{
		#region Methods
		/// <summary>
		/// </summary>
		virtual public void EnterState(bool soft)
		{
		}
		/// <summary>
		/// </summary>
		virtual public void RefreshView() {  }
		/// <summary>
		/// </summary>
		public void RefreshView(object sender, EventArgs e)
		{
			RefreshView();
		}
		#endregion
		#region Properties
		/// <summary>
		/// </summary>
		public IViewPoint Owner
		{
			get;
			private set;
		}
		#endregion
		#region .ctor
		/// <summary>
		/// </summary>
		public UserView() : base()
		{
			InitializeComponent();
		}
		/// <summary>
		/// </summary>
		public UserView(IViewPoint owner) : this()
		{
			Owner = owner;
		}
		#endregion
		#region Design

		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			//
			// UserView
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(419, 286);
			this.Name = "UserView";
			this.ResumeLayout(false);
		}
		#endregion
	}

	/// <summary>
	/// </summary>
	public interface IViewPointAction<TEvent>
	{
		#region Properties

		/// <summary>
		/// </summary>
		string Title {
			get;
			set;
		}
		/// <summary>
		/// </summary>
		string Description {
			get;
			set;
		}
		/// <summary>
		/// </summary>
		string Group {
			get;
			set;
		}
		/// <summary>
		/// </summary>
		Action<object,TEvent> Command {
			get;
		}

		#endregion
		#region Methods
		/// <summary>
		/// </summary>
		void RunCommand(object senser, TEvent e);
		#endregion
	}

	/// <summary>
	/// </summary>
	public class ViewPointAction : IViewPointAction<EventArgs>
	{
		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			get;
			set;
		}
		/// <summary>
		/// </summary>
		public string Description
		{
			get;
			set;
		}
		/// <summary>
		/// </summary>
		public string Group
		{
			get;
			set;
		}
		/// <summary>
		/// </summary>
		public Action<object,EventArgs> Command
		{
			get;
			set;
		}
		#endregion
		#region Methods
		/// <summary>
		/// </summary>
		virtual public void RunCommand(object sender, EventArgs e)
		{
			if (Command!=null) Command(sender,e);
		}
		/// <summary>
		/// </summary>
		static public ViewPointAction Create(string title, string description, string groupn, Action<Object,EventArgs> cmd)
		{
			var action = new ViewPointAction();
			action.Title = title;
			action.Description = description;
			action.Group = groupn;
			action.Command = cmd;
			return action;
		}
		#endregion
	}

	/// <summary>
	/// Flags to specify the default type of control to be shown.
	/// In the case that more then one ViewPointType is attributed,
	/// the application is responsible for determining the type of
	/// view shown.
	/// </summary>
	[Flags] public enum ViewPointType {
		#region All
		/// <summary>
		/// If not defined, the application uses whatever method it
		/// is designed to ;)
		/// </summary>
		@Undefined = 0x00,
		/// <summary>
		/// UserControl view will be constrained to the host's view-panel.
		/// </summary>
		@UserControl,
		/// <summary>
		/// If we use the Dialog view, the dialog will pop up
		/// and lock the parent window.
		/// </summary>
		@Dialog = 0x10,
		/// <summary>
		/// If we use the window view, then the window will pop up
		/// and will not lock the previous.
		/// </summary>
		@Window,
		#endregion
	}
	/// <summary>
	/// A hierarchical unique interface
	/// </summary>
	public interface IViewState
	{
	}
	/// <summary>
	/// </summary>
	public interface IViewPoint
	{
		#region All
		/// <summary>
		/// </summary>
		Icon MainIcon {
			get;
		}
		/// <summary>
		/// </summary>
		Dictionary<string,ViewPointAction> Actions {
			get;
		}
		/// <summary>
		/// </summary>
		ViewPointType ViewType {
			get;
		}
		/// <summary>
		/// If provided, is used to order the provided menu items.
		/// ShortcutKeys didn't pan out as a ordering mechanism—hopefully this will work.
		/// </summary>
		int? InsertionIndex {
			get;
		}
		/// <summary>
		/// used as a caption for the control and/or ToolStripMenuItem.
		/// </summary>
		string Title {
			get;
		}
		/// <summary>
		/// used as a caption for the control and/or ToolStripMenuItem.
		/// </summary>
		string Description {
			get;
		}
		/// <summary>
		/// For the ToolStripMenuItem; See <see cref="GetMenuItems" />.
		/// </summary>
		/// <remarks>
		/// This is also used to sort our items where the Home item is the first,
		/// and each other view uses F5-F10.  We won't provide more then
		/// the five as noted above.
		/// </remarks>
		Keys? ShortcutKeys {
			get;
		}
		/// <summary>
		/// We provide this specifically so that a separator can be added.
		/// </summary>
		/// <returns></returns>
		IEnumerable<ToolStripItem> GetMenuItems(EventHandler handler);
		#endregion
	}
	public interface IUserViewPoint : IViewPoint<UserView>
	{
	}
	public interface IViewPoint<TView>: IViewPoint where TView:UserView
	{
		#region Methods
		/// <summary>
		/// Provide a fresh copy of the main view.
		/// </summary>
		/// <remarks>
		/// Note that in the future it may be wise to add
		/// a BackupOnChange property or something similar where
		/// the control stays in memory when you change views.
		/// </remarks>
		/// <returns></returns>
		TView GetView();
		#endregion
	}
	public class ViewPoint<TView> : ViewPointBase, IViewPoint<TView> where TView:UserView
	{
		#region Actions

		/// <summary>
		/// </summary>
		virtual public Dictionary<string,ViewPointAction> Actions
		{
			get {
				return __actions;
			}
			internal set {
				__actions = value;
			}
		} internal Dictionary<string,ViewPointAction> __actions = new Dictionary<string, ViewPointAction>();

		/// <summary>
		/// </summary>
		public void AddAction(string Key, ViewPointAction action)
		{
			Actions.Add( Key , action );
		}

		/// <summary>
		/// </summary>
		public void AddAction(string Key, string nam, string dsc, string grp, Action<object,EventArgs> cmd)
		{
			Actions.Add( Key , ViewPointAction.Create(nam,dsc,grp,cmd) );
		}

		#endregion

		#region Properties
		/// <summary>
		/// </summary>
		virtual public Icon MainIcon
		{
			get {
				return null;
			}
		}

		/// <summary>
		/// </summary>
		public ViewPointType ViewType
		{
			get {
				return viewType;
			}
			internal set {
				viewType = value;
			}
		} ViewPointType viewType = ViewPointType.Undefined;

		/// <summary>
		/// </summary>
		virtual public int? InsertionIndex
		{
			get {
				return null;
			}
		}

		/// <summary>
		/// </summary>
		public virtual Nullable<Keys> ShortcutKeys
		{
			get {
				return shortcutKeys;
			}
		} Nullable<Keys> shortcutKeys = null;

		/// <inheritdoc />
		virtual public IEnumerable<ToolStripItem> GetMenuItems(EventHandler handler)
		{
			System.Windows.Forms.ToolStripMenuItem itm = new System.Windows.Forms.ToolStripMenuItem(Title,null,handler);
			if (ShortcutKeys.HasValue) itm.ShortcutKeys = ShortcutKeys.Value;
			itm.ToolTipText = Description;
			itm.Tag = this;
			return new System.Windows.Forms.ToolStripMenuItem[] { itm };
		}

		/// <summary>
		/// </summary>
		public virtual string Title
		{
			get {
				return title;
			}
		} internal string title = null;

		/// <summary>
		/// </summary>
		public virtual string Description
		{
			get {
				return description;
			}
		} internal string description = null;

		/// <summary>
		/// </summary>
		public TView View
		{
			get;
			set;
		}

		/// <summary>
		/// </summary>
		virtual public TView GetView()
		{
			return View;
		}

		#endregion

		#region .ctor
		/// <summary>
		/// </summary>
		public ViewPoint() : base()
		{
		}
		#endregion
	}
	/// <summary>
	/// The one thing that must be is a parameterless constructor
	/// </summary>
	public class ViewPoint : ViewPoint<UserView>
	{
		#region .ctor
		/// <summary>
		/// </summary>
		public ViewPoint() : base()
		{
		}
		#endregion
	}
	public class ViewPointBase
	{
		#region Properties
		/// <summary>
		/// </summary>
		public List<ViewPoint> ViewCollection;
		#endregion
		#region Static
		/// <summary>
		/// </summary>
		static UserView GetUserView(ViewPoint viewPoint)
		{
			return viewPoint.GetView();
		}
		/// <summary>
		/// </summary>
		static System.Windows.Forms.Form DialogForViewPoint<T>(System.Windows.Forms.Form parentWnd, ViewPoint<T> viewPoint)
			where T:UserView
		{
			T newView = viewPoint.GetView();
			System.Windows.Forms.Form form = new System.Windows.Forms.Form();
			form.Controls.Add(newView);
			newView.Dock = System.Windows.Forms.DockStyle.Fill;
			if (viewPoint.ViewType== ViewPointType.Window) {
				form.Show(parentWnd);
			} else if (viewPoint.ViewType == ViewPointType.Dialog) {
				form.ShowDialog(parentWnd);
			} else {
				System.Windows.Forms.MessageBox.Show("Check your window, control or dialog implementation mr developer.","Mr. Developer needs to think about this...");
			}
			return form;
		}
		
		/// <summary>
		/// </summary>
		static public List<ViewPoint> EnumerateViewTypes(Assembly containedAssembly)
		{
			return EnumerateViewTypes<ViewPoint>(containedAssembly);
		}
		
		/// <summary>
		/// </summary>
		static public List<TView> EnumerateViewTypes<TView>(params Assembly[] containedAssembly)
			where TView:class, IViewPoint
		{
			List<TView> types = new List<TView>();
			foreach (Assembly mrAssembly in containedAssembly)
				foreach (Type t in mrAssembly.GetTypes())
					if (t.BaseType == typeof(TView))
						types.Add(Activator.CreateInstance(t) as TView);
			return types;
		}

		#endregion
		#region Methods
		/// <summary>
		/// </summary>
		public void SetViewContainer(Assembly containerAssembly)
		{
			this.ViewCollection.Clear();
			AddViewAssembly(containerAssembly);
		}
		/// <summary>
		/// </summary>
		public void AddViewAssembly(Assembly containerAssembly)
		{
			this.ViewCollection.AddRange(EnumerateViewTypes(containerAssembly));
		}
		#endregion
		#region .ctor
		/// <summary>
		/// </summary>
		public ViewPointBase(Assembly containedAssembly)
		{
			EnumerateViewTypes(containedAssembly);
		}
		/// <summary>
		/// </summary>
		public ViewPointBase()
		{
		}
		#endregion
	}
	static class Extensions
	{
		/// <summary>
		/// </summary>
		static public P FindView<F,P,V>(this List<P> ViewPoints, string Key)
			where P:ViewPoint<V>
			where V:UserView<F>
			where F:System.Windows.Forms.Form
		{
			foreach (P view in ViewPoints) if (view.Title==Key) return view;
			return null;
		}
	}
	
	public class ViewMaster<TView> where TView:UserView
	{
		Assembly ExecutingAssembly { get { return Assembly.GetExecutingAssembly(); } }
		protected IList<Assembly> AssemblyCollection { get;set; }
		
		public IList<string> Keys { get;set; }
		
		public IList<TView> ViewCollection { get; set; }
		
		public ViewMaster(Assembly asm=ExecutingAssembly)
		{
			AssemblyCollection = new List<Assembly>(){ asm };
			ViewCollection = new List<TView>();
			Keys = new List<string>();
			
			foreach (var a in AssemblyCollection)
			{
				var x = ViewPoint.EnumerateViewTypes<TView>(a);
				foreach (var v in x) ViewCollection.Add(v);
			}
			foreach (IViewPoint view in ViewCollection)
				Keys.Add(view.Title);

		}
	}
	
	#region Old
//	public interface IViewPointNode
//	{
//		string Group { get; set; }
//		string Title { get; set; }
//		string Description { get; set; }
//	}
//	public class ViewPointItem : IViewPointNode
//	{
//		public string Group { get; set; }
//		public string Title { get; set; }
//		public string Description { get; set; }
//		public List<IViewPointAction> Actions { get;set; }
//	}
	#endregion

}
