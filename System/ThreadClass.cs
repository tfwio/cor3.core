/* oOo * 11/14/2007 : 10:22 PM */
using System;
using System.Cor3;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace System.Cor3
{
	/// <summary>
	/// a pointless thread class test
	/// </summary>
	[Obsolete]
	public class ThreadClass
	{
		public Thread theThread;
		protected bool _fin;
		public bool IsFinished { get { return _fin; } }
		
		public event NotifyProgress Progress;
		public event ThreadInfo Begat;
		public event ThreadInfo Complete;
		
		public ThreadClass() : this(false)
		{
		}
		public ThreadClass(bool AutoStart)
		{
			InitializeThread();
			if (AutoStart) theThread.Start();
		}
		
		virtual public void InitializeThread() {  theThread = new Thread(Begin); }
		virtual public void Begin() { OnBegat(); }
		virtual public void Terminate() { if (theThread.IsAlive) theThread.Abort(); }
		
		protected virtual void OnProgress(int current, int destination)
		{
			if (Progress != null) Progress(current, destination);
			if (current==destination) OnComplete();
		}
		protected virtual void OnBegat() { if (Begat!=null) Begat(); }
		protected virtual void OnComplete() { if (Complete!=null) Complete(); }
		
	}
}
