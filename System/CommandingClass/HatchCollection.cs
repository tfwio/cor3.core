/**
 * oIo * 2/14/2011 8:39 AM
 **/

using System;

namespace System
{
	public class HatchCollection<TEnum>
	{
		public DICT<TEnum,EventHatch<TEnum>> Collection
			= new DICT<TEnum, EventHatch<TEnum>>();

		public bool Contains(TEnum Key) { return Collection.ContainsKey(Key); }
		
		public void Execute(TEnum Key)
		{
			if (Collection.ContainsKey(Key)) Collection[Key].Call();
			else throw new NotImplementedException(Key.ToString());
		}
		
		public EventHatch<TEnum> this[TEnum Key]
		{
			get
			{
				if (!Contains(Key)) return null;
				return Collection[Key];
			}
		}
		#region Add
		public void Add(
			TEnum o,
			EventHatch<TEnum>.Trigger t,
			EventHatch<TEnum>.CanTrigger ct)
		{
			System.Diagnostics.Debug.Print("Added To Collection Via #1");
			EventHatch<TEnum> ehatch = new EventHatch<TEnum>(o,t,ct);
			ehatch.Executed += Exec;
			ehatch.CanExecuteChanged += CanDo;
			Collection.Add(o,ehatch);
		}

		/// <param name="o">Enumeration Value</param>
		/// <param name="t">Trigger </param>
		/// <param name="ct">Can Trigger</param>
		/// <param name="e1">EventHandler</param>
		/// <param name="e2">Is a CanHandleEventArgs EventHandler.</param>
		public void Add(
			TEnum o,
			EventHatch<TEnum>.Trigger t,
			EventHatch<TEnum>.CanTrigger ct,
			EventHandler e1,
			EventHandler<EventHatch<TEnum>.CanHandleEventArgs> e2)
		{
			System.Diagnostics.Debug.Print("Added To Collection Via #2");
			EventHatch<TEnum> ehatch = new EventHatch<TEnum>(o,t,ct);
			ehatch.Executed += e1;
			ehatch.Executed += Exec;
			ehatch.CanExecuteChanged += e2;
			ehatch.CanExecuteChanged += CanDo;
			Collection.Add(o,ehatch);
		}

		#endregion

		#region Parent Access
		/// <summary>
		/// we should use some sort of Argument that allows for us
		/// to know what is being called
		/// </summary>
		virtual public void Exec(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.Print("HatchCollection.Exec:{0}", sender);
		}

		virtual public void CanDo(object sender, EventHatch<TEnum>.CanHandleEventArgs e)
		{
			System.Diagnostics.Debug.Print("HatchCollection.CanDo:{0}", sender);
			System.Diagnostics.Debug.Print("CanDo[{0}]", e.CallerId);
		}
		#endregion

		public HatchCollection(bool autoFillCollection)
		{
			if (autoFillCollection)
			foreach (TEnum o in Enum.GetValues(typeof(TEnum)))
			{
				Add(o,null,null);
			}
		}

		public HatchCollection() : this(false)
		{
			System.Diagnostics.Debug.Print("HatchCollection Created");
		}
	}

}
