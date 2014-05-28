/* User: oIo * Date: 9/21/2010 * Time: 11:44 AM */
/* User: oIo * Date: 9/18/2010 * Time: 5:46 PM */
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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Cor3.Design
{
	//http://msdn.microsoft.com/en-us/library/ms171830.aspx
	public class DbActionList : DesignerActionList
	{
		public DbActionList(IComponent c) : base(c)
		{
		}
		
		string profileName = string.Empty;
		//[Editor]
		public string TableProfileName { get { return profileName; } set { profileName = value; } }
		/*public void RefreshDataTables()
		{
		}
		public void SomeOtherMethod()
		{
		}*/
        public DockStyle DockControl { get { return (this.Component as Control).Dock; } set { SetProp("Dock", value); } }
		virtual public Color BackgroundColor
		{
            get { return (this.Component as Control).BackColor; }
			set { SetProp("BackColor",value); }
		}
		virtual public Color ForeColor
		{
            get { return (this.Component as Control).ForeColor; }
			set { SetProp("ForeColor",value); }
		}
		
		protected void SetProp(string name, object value)
		{
            GetPropertyByName(name).SetValue(this.Component, value);
		}
		private PropertyDescriptor GetPropertyByName(String propName)
		{
			PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(this.Component)[propName];
			if (null == prop) throw new ArgumentException( "Matching Target property not found!",propName);
			else return prop;
		}
	}

}
