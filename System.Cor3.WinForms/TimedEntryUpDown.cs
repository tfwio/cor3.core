#region User/License
// oio * 10/26/2012 * 12:26 PM

// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace System.Cor3.WinForms
{
	/// <summary>
	/// Description of UserControl1.
	/// </summary>
	public class TimedEntryUpDown : Component
	{
		const	int		IdleTime = 900;
		Timer	timer;
		
		IComponent NumericUpDownControl;
		public System.Windows.Forms.NumericUpDown UpDownControl {
			get { return NumericUpDownControl as System.Windows.Forms.NumericUpDown; }
			set { InitializeControl(value); }
		}
	
		public event EventHandler TimeOut;
		public void OnTimeOut(EventArgs o)
		{
			if (TimeOut!=null) TimeOut(null,o);
		}
		
		public void TimedOut(object sender, EventArgs e)
		{
			timer.Stop();
			OnTimeOut(EventArgs.Empty);
			Global.statC("Timer: Stopped");
		}
		
		void InitializeControl(IComponent cvalue)
		{
			NumericUpDownControl = cvalue;
			Inititialize();
			UpDownControl.ValueChanged -= UpdownChanged;
			UpDownControl.ValueChanged += UpdownChanged;
		}
		
		void UpdownChanged(object sender, EventArgs e)
		{
			Start();
		}
		
		public void Inititialize()
		{
			if (timer==null) timer = new Timer();
			timer.Interval = IdleTime;
			timer.Tick -= TimedOut;
			timer.Tick += TimedOut;
		}
		public void Start()
		{
			timer.Tick -= TimedOut;
			if (timer.Enabled) 
			{
				Global.statGd("Timer Stopped — ‘Stop()’");
				timer.Stop();
			}
			timer.Tick += TimedOut;
			timer.Start();
			Global.statG("Timer started");
		}
		public void Stop()
		{
			timer.Stop();
		}
	}
	/// <summary>
	/// Description of UserControl1.
	/// </summary>
	public class TextBoxTimer : Component
	{
		const	int		IdleTime = 900;
		
		IComponent _TextBoxControl;
		Timer	timer;
		
		public event EventHandler TimeOut;
		
		public System.Windows.Forms.TextBox TextBoxControl {
			get { return _TextBoxControl as System.Windows.Forms.TextBox; }
			set { InitializeControl(value); }
		}
	
		public void OnTimeOut(EventArgs o)
		{
			if (TimeOut!=null) TimeOut(null,o);
		}
		
		public void TimedOut(object sender, EventArgs e)
		{
			timer.Stop();
			OnTimeOut(EventArgs.Empty);
		}
		
		void InitializeControl(IComponent cvalue)
		{
			_TextBoxControl = cvalue;
			Inititialize();
			TextBoxControl.TextChanged -= ValueChanged;
			TextBoxControl.TextChanged += ValueChanged;
		}
		
		void ValueChanged(object sender, EventArgs e)
		{
			Start();
		}
		
		public void Inititialize()
		{
			if (timer==null) timer = new Timer();
			timer.Interval = IdleTime;
			timer.Tick -= TimedOut;
			timer.Tick += TimedOut;
		}
		public void Start()
		{
			timer.Tick -= TimedOut;
			if (timer.Enabled) 
			{
				Global.statGd("Timer Stopped — ‘Stop()’");
				timer.Stop();
			}
			timer.Tick += TimedOut;
			timer.Start();
			Global.statG("Timer started");
		}
		public void Stop()
		{
			timer.Stop();
		}
	}
	

}
