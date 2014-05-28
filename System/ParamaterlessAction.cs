#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */

using System;
using System.Runtime.InteropServices;
using SF = System.Environment.SpecialFolder;

namespace System
{
	/// <summary>
	/// NOTE: We need an optional action to be performed on a succesfull
	/// execution.  This is our Pre and Post Parameterized callback.
	/// <para>
	/// Simply set the Pre or Post operation upon initialization for this
	/// to work.
	/// </para>
	/// </summary>
	public abstract class ParamaterlessAction<TEvent>
//		where TEvent:EventArgs
	{
		public ParamaterlessCallback PreAction = null;
		virtual public ParamaterizedCallback Pre { get; set; }
		virtual public void DefaultPreAction(object o, TEvent a){ this.PreAction.Invoke(); }
		
		public ParamaterlessCallback MainAction = null;
		virtual public ParamaterizedCallback Main { get; set; }
		virtual public void DefaultMainAction(object o, TEvent a){ this.MainAction.Invoke(); }
		
		public ParamaterlessCallback PostAction = null;
		virtual public ParamaterizedCallback Post { get; set; }
		virtual public void DefaultPostAction(object o, TEvent a){ this.PreAction.Invoke(); }
		
		virtual public string	Name { get;set; }
		virtual public string	Description { get;set; }
		
		public delegate void	ParamaterizedCallback(object sender, TEvent args);
		public delegate void	ParamaterlessCallback(/*params object[] xitems*/);
		public					ParamaterlessCallback Action { get { return Run; } }
		/// <summary>
		/// fasioned after sharpdevelop (just a little).
		/// </summary>
		virtual public void		Run(/*params object[] xitems*/) { if (Main!=null) MainAction.Invoke(); }
		virtual public void		Handler(object o, TEvent a)
		{
			if (this.Pre!=null) this.Pre.Invoke(o,a);
			if (this.MainAction!=null) this.Main.Invoke(o,a); else Run();
			if (this.Post!=null) this.Post.Invoke(o,a);
		}
		#if NET4
		// this never happens you know.
		virtual public void Handler2(object o, CanExecuteRoutedEventArgs a);
		#endif
		public ParamaterlessAction(){
			
		}
	}

	/// <summary>Provides a class for .NET 2.0 Compatible applications that
	/// can use a methods that can be used similarly to Commands.</summary>
	/// <description><para>Generally, it's expected that you derive from
	/// this class for action-bound classes.</para>
	/// <para>The class provides provisions for Pre, Action, and Post
	/// Handlers so that actions can be canceled (intuitively) from
	/// a Pre-Handler.</para>
	/// <para>Future versions of this class may support a sort of CanExecute
	/// Property and Handler.</para>
	/// </description>
	public abstract class TAction<TControl,TEvent> : ParamaterlessAction<TEvent>
	{
//		public TAction(ICommand cmd, TControl win) : base(win) {
//			this.Context.CommandBindings.Add(new CommandBinding(CommandShowOriginal,Handler));
//		}
		/// <summary>
		/// The owning class or context to which this type of action
		/// is bound.
		/// </summary>
		public TControl Context { get;set; }
		/// <summary>
		/// Parameterless constructor.  (use the Context-binding constructor)
		/// </summary>
		public TAction() : base(){}
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="Context"></param>
		public TAction(TControl Context) : base()
		{
			this.Context = Context;
		}
	}

}
