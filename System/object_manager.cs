/* oOo * 11/19/2007 : 8:00 AM */
// OBSOLETE
using System;
using System.Xml.Serialization;

namespace System.Cor3
{
	public interface IObjectManager<TControl>
	{
		TControl Client { get;set; }
		void Initialize();
		void AddEvents();
		void RemoveEvents();
	}
	public class object_manager<TClient> : IObjectManager<TClient>
	{
		[XmlIgnore] public TClient parent_obj;
		[XmlIgnore] virtual public TClient Client { get { return parent_obj; } set { parent_obj=value; } }
	
		/// <remarks> Calls <c>AddEvents()</c> </remarks>
		virtual public void Initialize () { AddEvents(); }
		/// <summary> virtualized </summary>
		virtual public void AddEvents () { }
		/// <summary> virtualized </summary>
		virtual public void RemoveEvents () { }
	
		public object_manager () { }
		public object_manager (TClient control) : this(control,true) { }
		public object_manager (TClient control, bool do_init)
		{
			parent_obj = control;
			if (do_init) Initialize();
		}
	
	}
	
	/// <summary>
	/// Orignally, the class was named ‘ObjectManager’ however Microsoft was using
	/// the class-name for a class of their own.
	/// </summary>
	public class ObjectManager2<TClient> : IObjectManager<TClient>
	{
		internal TClient parent_obj;
		[XmlIgnore]
		virtual public TClient Client { get { return parent_obj; } set { parent_obj=value; } }
	
		/// <remarks> Calls <c>AddEvents()</c> </remarks>
		virtual public void Initialize () { AddEvents(); }
		/// <summary> virtualized </summary>
		virtual public void AddEvents () { }
		/// <summary> virtualized </summary>
		virtual public void RemoveEvents () { }
	
		public ObjectManager2 () { }
		public ObjectManager2 (TClient control) : this(control,true) { }
		public ObjectManager2 (TClient control, bool do_init)
		{
			parent_obj = control;
			if (do_init) Initialize();
		}
	
	}
}
