/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 2/4/2013
 * Time: 11:32 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace System.Cor3
{
	/// <summary>
	/// Description of ExceptionDialog.
	/// </summary>
	public partial class ExceptionDialog : Form
	{
		public string ExceptionText { get { return richTextBox1.Text; } set { richTextBox1.Text = value; } }
		
		public ExceptionDialog(Exception innerException)
		{
			InitializeComponent();
			ExceptionText = innerException.ToString();
		}
	}
}
