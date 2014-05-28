// oio * 10/26/2012 * 12:09 PM
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

namespace System.Internals
{
    // NOTE: UNTESTED GARBAGE!!!
	class UndoRedoCommandManager
	{
		public event EventHandler OnUndoRedoAction;
		
		public bool CanRedo { get { return RedoStack.Count > 0; } }
		public bool CanUndo { get { return UndoStack.Count > 0; } }
		
		Stack<ICommand> UndoStack, RedoStack;
		
		public void Clear()
		{
			UndoStack.Clear();
			RedoStack.Clear();
		}
		
		public void Undo()
		{
			if (CanUndo)
			{
				RedoStack.Push(UndoStack.Pop());
				RedoStack.Peek().Execute();
			}
		}
		public void Redo()
		{
			if (CanRedo)
			{
				UndoStack.Push(RedoStack.Pop());
				UndoStack.Peek().Execute();
			}
		}
		
		public UndoRedoCommandManager()
		{
			UndoStack = new Stack<ICommand>();
			RedoStack = new Stack<ICommand>();
		}
	
	}

	interface ICommand
	{
		void Execute();
		void UnExecute();
	}
	
	class TwoValueCommand<TControl,TValue> : ICommand
		where TControl:class
		where TValue:class
	{
		
		#region Properties
		protected TControl CommandControl { get;set; }
		protected TValue ValueOld { get;set; }
		protected TValue ValueNew { get;set; }
		#endregion
		
		public TwoValueCommand(TControl control, TValue valueold, TValue valuenew)
		{
			CommandControl = control;
			ValueNew = valuenew;
			ValueOld = valueold;
		}
		
		#region ICommand
		virtual public void Execute()
		{
			
		}
		virtual public void UnExecute()
		{
			
		}
		#endregion
		
	}
	
	class OneValueCommand<TControl,TValue> : ICommand
		where TControl:class
		where TValue:class
	{
		
		#region Properties
		protected TControl CommandControl { get;set; }
		protected TValue ValueNew { get;set; }
		#endregion
		
		public OneValueCommand(TControl control, TValue value)
		{
			CommandControl = control;
			ValueNew = value;
		}
		
		#region ICommand
		virtual public void Execute()
		{
			
		}
		virtual public void UnExecute()
		{
			
		}
		#endregion
		
	}
	
}
