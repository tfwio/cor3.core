/**
 * oIo * 2/16/2011 10:40 AM
 * oIo — 12/17/2010 — 6:20 AM
 */
using System;
using Timer = System.Windows.Forms.Timer;

namespace System
{
	/// 
	public class CommandingClassWithTimer<TEnum> : CommandingClass<TEnum>, ICommandingClassWithTimer<TEnum>
	{
		Timer Blinker;
		protected TEnum SignalInitializeTimerState;
		protected TEnum SignalTimerResultSuccess;
		protected TEnum SignalTimerResultFailed;
	
		protected const int DefaultNumberOfIntervals = 3;
		protected const int DefaultIntervalSpeed = 111;
		public bool blinkOn = false;
		public int interval = 0;
	
		/// always returns true — override it
		public virtual bool TimerCondition {
			get { return true; }
			set { }
		}
	
		/// always returns true — override it
		public virtual bool TimerExecuteCondition {
			get { return true; }
			set { }
		}
	
		public CommandingClassWithTimer() : base()
		{
		}
	
		#region Triggers
		/// I don't know why this is here
		protected virtual void TimerTrigger(object sender, EventArgs e)
		{
			StartStat();
		}
	
		/// This is called direct from the timer
		void TimerElapsedHandler(object sender, EventArgs e)
		{
			TriggerTimer();
		}
		#endregion
	
		/// this should be generally the initialization process
		/// of the timer mechanism.
		/// <para>Signals SignalInitializeTimerState on completion.</para>
		/// <para>otherwise, you can override this function to control initialization.</para>
		protected virtual void InitialTimerState()
		{
			Blinker = new Timer();
			Blinker.Interval = DefaultIntervalSpeed;
			Blinker.Tick -= TimerElapsedHandler;
			Blinker.Tick += TimerElapsedHandler;
			XCommand.Execute(SignalInitializeTimerState);
		}
	
		/// this is the main trigger for the timer.
		/// <remarks>If not initialized will cause Exception.</remarks>
		protected virtual void StartStat()
		{
			Blinker.Stop();
			blinkOn = false;
			interval = 0;
			Blinker.Start();
		}
	
		/// timer EventHandler calls me
		protected virtual void TriggerTimer()
		{
			// finalize
			if (interval >= DefaultNumberOfIntervals) {
				Blinker.Stop();
				if (TimerExecuteCondition) {
					XCommand.Execute(SignalTimerResultSuccess);
				} else {
					XCommand.Execute(SignalTimerResultFailed);
				}
				return;
			}
			interval++;
		}
	
	}
}
