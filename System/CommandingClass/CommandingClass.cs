/**
 * oIo * 2/16/2011 10:40 AM
 * oIo — 12/17/2010 — 6:20 AM
 */
using System;
using System.Data;
using System.Drawing;
using System.Xml;

using Timer = System.Windows.Forms.Timer;

namespace System
{
	/// <summary>Implementor of the Command ideology</summary>
	public class CommandingClass<TEnum> : MarshalByRefObject, ICommandingClass<TEnum>
	{
		protected virtual void InitializeCommands()
		{
			XCommand = new HatchCollection<TEnum>();
		}

		public HatchCollection<TEnum> XCommand;

		public EventHatch<TEnum> this[TEnum Key] {
			get { return XCommand[Key]; }
		}

		public void RefreshEventHandler(TEnum key, EventHandler e)
		{
			if (XCommand.Contains(key)) {
				RemoveExecutedHandler(key, e);
				AddExecutedHandler(key, e);
			}
		}
		public void AddExecutedHandler(TEnum key, EventHandler handler)
		{
			if (XCommand.Contains(key))
				XCommand[key].Executed += handler;
		}
		public void SetAction(TEnum key, EventHatch<TEnum>.Trigger handler)
		{
			if (XCommand.Contains(key))
				XCommand[key].Delegated = handler;
		}
		public void RemoveExecutedHandler(TEnum key, EventHandler handler)
		{
			if (XCommand.Contains(key))
				XCommand[key].Executed -= handler;
		}
		public void ForceEventHandler(TEnum Key, EventHandler e)
		{
			if (!XCommand.Contains(Key))
				XCommand.Add(Key, null, null);
			AddExecutedHandler(Key, e);
		}

		/// <summary>
		/// Set and/or implemented if needed.
		/// (You're responsible for implementing this your-self)
		/// </summary>
		protected TEnum SignalClassInitialized;

		public CommandingClass()
		{
			InitializeCommands();
		}

	}

}
