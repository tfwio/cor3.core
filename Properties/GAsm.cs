#region Using directives

using System;
using System.Reflection;
using System.Runtime.InteropServices;

#endregion

// TODO: Template this guy for SVN or GUT REVISION INFO

//[assembly: NeutralResourcesLanguageAttribute("en-US")]
[assembly: AssemblyFileVersionAttribute(ASM_VERSION.Version)]
[assembly: AssemblyVersion(ASM_VERSION.Version)]
[assembly: AssemblyCopyright(ASM_VERSION.CopyInfo)]
[assembly: AssemblyCompany("tfw.co")]
//[assembly: AssemblyCulture("en-US")]
[assembly: ObfuscateAssembly(true)]
//[assembly: AssemblyTrademark("")]

struct ASM_VERSION
{
//	internal const string vMajor			= "1";
//	internal const string vMajorIncrament	= "0";
//	internal const string VMinor			= "212";
//	internal const string VMinorIncrament	= "0";
	public   const string Version			= "1.0.3.3";
//	internal const string format			= "{0}.{1}.{2}.{3}";
//	static readonly DateTime CBegin			= new DateTime(2010,0,0);
//	static readonly DateTime CEnd			= new DateTime(2011,0,0);
	public   const string CopyInfo			= "© 2010-2011 tƒw.co";
//	public static readonly string AssemblyVersion = string.Format(format,vMajor,vMajorIncrament,VMinor,VMinorIncrament);
//	public static readonly string AssemblyCopyInfo = string.Format(format,CBegin,CEnd);
}