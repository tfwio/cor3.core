/**
 * oIo * 2/16/2011 10:40 AM
 * oIo — 12/17/2010 — 6:20 AM
 */
using System;

namespace System
{
	public interface ICommandingClassWithTimer<TEnum> : ICommandingClass<TEnum>
	{
		bool TimerCondition { get; set; }
		bool TimerExecuteCondition { get; set; }
	}
}
