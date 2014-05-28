#region User/License
// oio * 200--01-04 * 02:33 PM

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
using System.Drawing;
using System.Windows.Forms;

namespace GenericWAV.Views
{
	/// <summary>
	/// Description of spl.
	/// </summary>
	class SplashForm : Form
	{
		public SplashForm()
		{
			InitializeComponent();
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			Close();
		}
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			Close();
		}
		#region Design
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// SplashForm
			// 
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SplashForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "spl";
			this.TopMost = true;
			this.ResumeLayout(false);
		}
		#endregion
	}
	public class SplashFormController : IDisposable
	{
		protected Form MainForm { get;set; }
		Bitmap BackgroundImage { get;set; }
		SplashForm spx { get;set; }
		bool AutoHide { get;set; }
		FloatPoint ClientSize { get;set; }
		public bool IsVisible { get;set; }
		
		public SplashFormController(Form main, Bitmap img, bool autoHide)
		{
			IsVisible = false;
			MainForm = main;
			BackgroundImage = img;
			ClientSize = img.Size;
			AutoHide = autoHide;
			Create();
			Initialize();
		}
		void IDisposable.Dispose()
		{
			if (this.spx!=null) {
				spx.Dispose();
				spx = null;
				BackgroundImage.Dispose();
				BackgroundImage = null;
			}
		}
		
		#region Restore Callback
		void Event_Splash_Hide(object sender, CancelEventArgs e)
		{
			ActionDestroy();
		}
		void Event_Splash_Hide(object o, EventArgs s)
		{
			ActionDestroy();
		}
		#endregion
		
		void Create()
		{
			if (spx!=null) {
				spx.Dispose();
				spx = null;
			}
			spx = new SplashForm();
			spx.StartPosition = FormStartPosition.CenterScreen;
			spx.Closed += Event_Splash_Hide;
			spx.Size = ClientSize;
			spx.BackgroundImage = BackgroundImage;
		}
		
		void Initialize()
		{
			if (AutoHide) MainForm.Hide();
			spx.SuspendLayout();
			spx.ResumeLayout();
			IsVisible = true;
			spx.Show(MainForm);
		}
		
		public void ActionDestroy()
		{
			if (!IsVisible) return;
			spx.Dispose();
			spx = null;
			IsVisible = false;
			if (AutoHide) {
				MainForm.Show();
				MainForm.BringToFront();
			}
		}
		
		public void ShowCb(bool auto)
		{
			if (IsVisible) return;
			if (auto) MainForm.Hide();
			Create();
			Show();
		}
		
		public void Event_Splash_Show(object sender, System.EventArgs e)
		{
			ShowCb(AutoHide);
		}
		
		void Show()
		{
			spx.SuspendLayout();
			spx.StartPosition = FormStartPosition.CenterScreen;
			spx.ResumeLayout();
			spx.Show();
			IsVisible = true;
		}
	}
}
