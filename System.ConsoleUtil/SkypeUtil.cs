/**
 * oIo * 3/8/2011 6:03 PM
 **/
using System;

namespace System.ConsoleUtil
{
    static public class SkypeUtil
    {
        static public string SkypePath
        {
            get
            {
                return Reg.GetKeyValueString(@"Software\Skype\Phone","SkypePath");
            }
        }
        
        static public bool HasSkype
        {
            get
            {
                string skypePath = SkypePath;
                bool starter = skypePath!=null;
                if (starter) return System.IO.File.Exists(skypePath);
                return false;
            }
        }
        
        static public void SkypeCall(string num)
        {
            if (!HasSkype)
            {
                System.Windows.Forms.MessageBox.Show( "Skype could not be found!", "Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            System.Diagnostics.Process.Start( SkypePath, string.Format("/callto:{0}",num) );
        }
    }
}
