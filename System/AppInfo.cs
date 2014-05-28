/* oOo * 11/19/2007 : 8:00 AM */
using System;

namespace System
{
	// (System.Windows.Forms compatible)
	static public class AppInfo
	{
		static public void Init(string dir, string settings)
		{
			AppInfo.AppDirectoryName = dir;
			AppInfo.AppFileSettings  = settings;
		}
	
		// Required
		/// <summary>
		/// The container directory such as '%appdata%/local/[AppDirectoryName]/[AppFileSettings]'
		/// </summary>
		static public string AppDirectoryName = string.Empty;
		/// <summary>
		/// The container directory such as '%appdata%/local/[AppDirectoryName]/[AppFileSettings]'
		/// </summary>
		static public string AppFileSettings = string.Empty;
	
		// DirectoryInfo
		static public System.IO.DirectoryInfo DirectoryGlobalInfo { get { return new System.IO.DirectoryInfo(System.Windows.Forms.Application.UserAppDataPath); } }
		static public System.IO.DirectoryInfo DirectoryLocalInfo  { get { return new System.IO.DirectoryInfo(System.Windows.Forms.Application.LocalUserAppDataPath); } }
	
		// Global
		static public string DirectoryGlobal = System.Windows.Forms.Application.UserAppDataPath;
		static public bool   HasDirectoryGlobal { get { return System.IO.Directory.Exists(DirectoryLocalInfo.Parent.FullName); } }
		static public string FileLocal(string path0) { return System.IO.Path.Combine(DirectoryLocalInfo.Parent.FullName,path0); }
		static public string AppFileSettingsGlobal { get { return FileGlobal(AppFileSettings); } }
		static public bool   HasFileSettingsGlobal { get { return System.IO.File.Exists(AppFileSettingsGlobal); } }
	
		// Local
		static public string DirectoryLocal  = System.Windows.Forms.Application.LocalUserAppDataPath;
		static public bool   HasDirectoryLocal  { get { return System.IO.Directory.Exists(DirectoryLocal); } }
		static public string FileGlobal(string path0) { return System.IO.Path.Combine(DirectoryLocalInfo.Parent.FullName,path0); }
		static public bool   HasFileSettingsLocal { get { return System.IO.File.Exists(AppFileSettingsLocal); } }
		static public string AppFileSettingsLocal { get { return FileLocal(AppFileSettings); } }
	}
}
