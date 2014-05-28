/**
 * oIo * 2/14/2011 10:53 AM
 **/
using System;
using System.Collections.Generic;

namespace System
{
	
	public interface IUndoState<TEnum> {
		TEnum Act { get; }
		bool CanUndo { get; }
		bool CanRedo { get; }
	}
	public class IThinkICanUndoEngine<T,TEnum>
		where T:IUndoState<TEnum>
		where TEnum:class
	{
		
		public bool CanUndo { get { return stack.Peek().CanUndo; } }
		public bool CanRedo { get { return stack.Peek().CanUndo; } }
		
		protected Stack<T> stack;
		virtual public void Undo()
		{
			
		}
		virtual public void Redo()
		{
			
		}
	}
	/// <summary>
	/// where TEnum is a Enumeration specifying API
	/// </summary>
	public class EventHatch<TEnum>
	{
		/// 
		public delegate void CanTrigger(CanHandleEventArgs e);
		/// 
		public delegate void Trigger();
		/// the internal action, as the (EventHandler) ‘Executed’
		/// is the UI response to the action.
		public Trigger		Delegated;
		/// 
		public CanTrigger	DelegateApproval;

		/// 
		public bool HasTrigger { get { return Delegated==null; } }
		/// 
		public bool HasCanTrigger { get { return DelegateApproval==null; } }
		/// 
		public bool WillExecute { get { return (!HasTrigger)|HasCanTrigger|(!CanExecute); } }

		#region Properties
		/// 
		protected bool canExecute = true;
		/// 
		public bool CanExecute
		{
			get
			{
				if (!HasCanTrigger) DelegateApproval( new CanHandleEventArgs(CallId, false, canExecute) );
				return canExecute;
			}
			set { canExecute=value; OnCanExecuteChanged(); }
		}

		/// 
		public TEnum CallId { get { return callId; } set { callId = value; } }
		TEnum callId;
		#endregion
		
		virtual public void Call()
		{
			System.Diagnostics.Debug.Print("Invoked via ‘{0}’.Call()", CallId);
			if (!WillExecute)
			{
				System.Diagnostics.Debug.Print("BUT WONT EXECUTE");
				return;
			}
			Delegated.Invoke();
			OnExecuted(EventArgs.Empty);
		}
		virtual public void CallEvent(object sender, EventArgs e) { Call(); }

		/// 
		public EventHatch()
		{
		}

		/// 
		public EventHatch(TEnum enumName, Trigger t, CanTrigger ct)
		{
			callId = enumName;
			Delegated = t;
			DelegateApproval = ct;
		}

		#region Event Handlers
		/// This could do just as well as a standard eventhandler
		/// where the sender would be this EventHatch.
		public event EventHandler<CanHandleEventArgs> CanExecuteChanged;
		/// 
		protected virtual void OnCanExecuteChanged() { if (CanExecuteChanged != null) CanExecuteChanged(this, new CanHandleEventArgs(CallId,false,canExecute)); }

		/// 
		public event EventHandler Executed;
		/// 
		protected virtual void OnExecuted(EventArgs e) { if (Executed != null) Executed(this, e); }

		/// 
		public event EventHandler<CanHandleEventArgs> CanHandle;
		/// 
		protected virtual void OnCanExecute(CanHandleEventArgs e) { if (CanExecute) if (CanHandle != null) CanHandle(this, e); }

		#endregion

		#region CanHandleEventArgs
		/// 
		public class CanHandleEventArgs : EventArgs
		{
			/// 
			internal TEnum CallerId;
			/// 
			public bool IsHandled, IsOperable;
			/// 
			public CanHandleEventArgs(TEnum cid) : this(cid,false,true)
			{
				CallerId = cid;
			}
			/// 
			public CanHandleEventArgs(TEnum cid, bool isHandled, bool isOper)
				: this(cid)
			{
				IsHandled = isHandled;
				IsOperable = isOper;
			}
		}
		#endregion

	}
}