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
	public class TimedTextComponent : Component
	{
		IComponent statusMain = null;
		private Timer statTimer;
		
		public int TimerInterval
		{
			get { return statTimer.Interval; }
			set { statTimer.Interval = value; }
		}
		
		public TextBox TextControl {
			get { return (TextBox) statusMain; }
			set { statusMain = value; }
		}
		
		TextBox SetTextBoxText(IComponent c) {
			statusMain = c;
			return (TextBox)statusMain;
		}
		
		bool HasNoControl { get { return (statusMain==null); } }
		bool Ticking { get { return statTimer.Enabled; } set { statTimer.Enabled = value; } }
		string lastStatus = string.Empty;
		
		public string StatusMain { get { if (HasNoControl) return null; return TextControl.Text??""; } set { TextControl.Text = value; } }
		
		public string FlashStatus { set { if (HasNoControl) return; StartStatus(value); } }
		
		
		void StartStatus(string value)
		{
			if (Ticking) { statTimer.Stop(); StatusMain = value; statTimer.Start(); }
			else { lastStatus = StatusMain; StatusMain = value; statTimer.Start(); }
		}
		void eTick (object s, EventArgs e) { statTimer.Stop(); StatusMain = lastStatus; }
		
		public TimedTextComponent()
		{
			InitializeComponent();
		}
		void InitializeComponent()
		{
			statTimer = new Timer();
			statTimer.Enabled = false;
			statTimer.Interval = 800;
			statTimer.Tick += eTick;
		}
	}
}
