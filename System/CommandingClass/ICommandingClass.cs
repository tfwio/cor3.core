/**
 * oIo * 2/16/2011 10:40 AM
 * oIo — 12/17/2010 — 6:20 AM
 */
using System;

namespace System
{
	public interface ICommandingClass<TEnum>
	{
		void RefreshEventHandler(TEnum key, EventHandler e);
		void AddExecutedHandler(TEnum key, EventHandler handler);
		void SetAction(TEnum key, EventHatch<TEnum>.Trigger handler);
		void RemoveExecutedHandler(TEnum key, EventHandler handler);
		void ForceEventHandler(TEnum Key, EventHandler e);
		
		// this was put here by generating an interface.
		// the Item is what is returned as “this[TEnum]”
		// EventHatch<TEnum> Item { get; }
	}
}
