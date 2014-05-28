/*
 * Created by SharpDevelop.
 * User: tfw
 * Date: 9/19/2008
 * Time: 3:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Collections;
using System.Collections.Generic;

/* oOo * 11/14/2007 : 10:50 PM */
namespace System {
	
	namespace Cor3 	{

		/// <summary>we'll see how bad an idea this was. </summary>
		public class SimpleType<T>  {
			T _data;
			public virtual T Data { get {return _data;} set {_data = (T)value;} }
			public SimpleType(T data) { _data = data; }
			public override string ToString() { return _data.ToString(); }
		}
		public class SimpleCollectionTrigger<T> : SimpleCollection<T>
		{
			public delegate void Trigger(EventTrigger args);
			public event Trigger OnTrigger;
			public class EventTrigger /*: EventArgs*/
			{
				public T trigger_value;
				public EventTrigger(T in_value) { trigger_value = in_value; }
			}
			internal protected void TriggerEvent(EventTrigger args) { if (OnTrigger!=null) OnTrigger(args); }
			virtual protected void TriggerExchange(EventTrigger arg) { }
			
			public SimpleCollectionTrigger() : base()
			{
				OnTrigger += new Trigger(TriggerExchange);
			}
			public SimpleCollectionTrigger(List<T> list) : base() { AddRange(list); }
			
			public override int Add(T item) { TriggerEvent(new EventTrigger(item)); return base.Add(item); }
			public override void AddRange(List<T> array) { foreach (T item in array) Add(item); }
		}
	}
}
