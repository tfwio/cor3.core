/*
 * Created by SharpDevelop.
 * User: tfw
 * Date: 9/19/2008
 * Time: 3:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

/* oOo * 11/14/2007 : 10:50 PM */
namespace System
{
	public class Mirror : Cor3.Introvert
	{
		public Mirror(object o) : base(o)
		{
		}
	}
	namespace Cor3
	{
		public delegate bool CheckAssembly(Type asm, params object[] values);
		public delegate bool CheckField(FieldInfo fieldInfo, params object[] values);
		public delegate bool CheckEvent(EventInfo eventInfo, params object[] values);
		public delegate bool CheckParameter(ParameterInfo paramInfo, params object[] values);
		public delegate bool CheckMethod(MethodInfo methodInfo, params object[] values);
		public delegate bool CheckProperty(PropertyInfo propertyInfo, params object[] values);
		public delegate bool CheckType(Type type, params object[] values);
		/// <summary>
		/// started out as a simple exersize.
		/// -- removed more then half of the original code.
		/// -- all of the contents were static.
		/// -- now it should no longer be static.
		/// 
		/// -- it would be interesting to see soemthing like serializable intolerance?
		/// -- perhaps this would be a good class to look at xml content?
		/// ---- or rather an override once this is inheritable?
		/// </summary>
		public class Introvert
		{
			public enum Action
			{
				Nameapces,
				Types
			}
			
//			Delegate ckType = Delegate.CreateDelegate(typeof(CheckType),

			static public bool CheckSetter(PropertyInfo info, params object[] values) { return info.CanWrite; }
			static public bool CheckAssembly(Assembly type, params object[] values) {
//				return type.has;
				return false;
			}
			// takes a single string parameter
			static public bool CheckType_Namespace(Type type, params object[] objs) {
//				return ;
				object o = type.Namespace;
				if (o == objs[0]) return true;
				return false;
			}

			static public List<string> GetNamespaces(Assembly asm)
			{
				List<string> list = new List<string>();
				Type[] x = asm.GetExportedTypes();
				foreach (Type t in x)
				{
					string ns = t.Namespace;
					if (!list.Contains(ns)) list.Add(ns);
				}
				Array.Clear(x,0,x.Length);
				x = null;
				return list;
			}

//			static public List<string> TypeFromNamespace(Assembly asm, params CheckType[] conditions)
//			{
//				List<string> types = new List<string>();
//				foreach (Type T in asm.GetExportedTypes())
//				{
//					bool pass = false;
//					if (conditions==null) { pass = true; }
//					else foreach (CheckType condition in conditions)
//					{
//						if (condition(T)) { pass = true; }
//					}
//					if (pass) types.Add(T.Namespace);
//				}
//				return types;
//			}
			
			static public List<Type> GetAssemblyTypes(Assembly asm, params CheckType[] conditions)//, Action condition
			{
				List<Type> types = new List<Type>();
				foreach (Type T in asm.GetExportedTypes())
				{
					bool pass = false;
					if (conditions==null) { pass = true; }
					else foreach (CheckType condition in conditions)
					{
						if (condition(T)) { pass = true; }
					}
					if (pass) types.Add(T);
				}
				return types;
			}
			static public List<Type> GetNamespaceTypes(Assembly asm, string ns, params CheckType[] conditions)//, Action condition
			{
				List<Type> types = new List<Type>();
				foreach (Type T in asm.GetExportedTypes())
				{
					bool pass = false;
					if (conditions==null) { pass = true; }
					else foreach (CheckType condition in conditions)
					{
						if (condition(T,ns)) { pass = true; }
					}
					if (pass) types.Add(T);
				}
				return types;
			}
			static public List<PropertyInfo> GetProperties<T>(BindingFlags flags, params CheckProperty[] conditions)
			{
				List<PropertyInfo> props = new List<PropertyInfo>();
				foreach (PropertyInfo p in typeof(T).GetProperties(flags))
				{
					bool pass = false;
					if (conditions==null) { pass = true; }
					else foreach (CheckProperty condition in conditions)
					{
						if (condition(p)) { pass = true; }
					}
					if (pass) props.Add(p);
				}
				return props;
			}
			/// <param name="conditions">condition is a function prototype</param>
			static public List<PropertyInfo> GetProperties<T>(params CheckProperty[] conditions)
			{
				return GetProperties<T>((BindingFlags)bf,conditions);
			}
			protected const int bf = (int)(
				BindingFlags.PutDispProperty
				|BindingFlags.PutRefDispProperty
				|BindingFlags.SetProperty
				|BindingFlags.Instance
				|BindingFlags.Public
			);
			protected const TSpec ALLSPEC =
				TSpec.CONSTRUCTORS|
				TSpec.ATTRIBUTES|
				TSpec.ATTRIBUTES_DEEP|
				TSpec.EVENTS|
				TSpec.FIELDS|
				TSpec.MEMBERS|
				TSpec.METHODS|
				TSpec.NESTED_TYPES|
				TSpec.PROPERTIES;
			#region ' Fields '
			public object pov;
			public Type obj;
			public object Pov { get { return pov; } set { pov = value; obj = pov.GetType(); } }
			#endregion
			
			#region ' ConstructorInfo[] Constructors '
			public ConstructorInfo[] Constructors { get { if (pov==null) return null; return obj.GetConstructors(); } set {} }
			#endregion
			#region ' object[] Attributes '
			public object[] Attributes { get { if (pov==null) return null; return obj.GetCustomAttributes(true); } }
			#endregion
			#region ' EventInfo[] Events '
			public EventInfo[] Events { get { if (pov==null) return null; return obj.GetEvents(); } set {} }
			#endregion
			#region ' FieldInfo[] Fields '
			public FieldInfo[] Fields { get { if (pov==null) return null; return obj.GetFields(); } set {} }
			#endregion
			#region ' MemberInfo[] Members '
			public MemberInfo[] Members { get { if (pov==null) return null; return obj.GetMembers(); } set {} }
			#endregion
			#region ' MethodInfo[] Methods '
			public MethodInfo[] Methods { get { if (pov==null) return null; return obj.GetMethods(); } set {} }
			#endregion
			#region ' Type[] NesT '
			public Type[] NesT { get { if (pov==null) return null; return obj.GetNestedTypes(); } set {} }
			#endregion
			#region ' PropertyInfo[] Properties '
			public PropertyInfo[] Properties { get { if (pov==null) return null; return obj.GetProperties(); } set {} }
			#endregion

			public Introvert(object o) { Pov = o; }
			
			#region ' Hashtable ToHashtable '
			public Hashtable ToHashtable()
			{
				Hashtable table = new Hashtable();
				int i=0;
				foreach (object o in Attributes) table.Add(i,o);
				foreach (ConstructorInfo o in Constructors) table.Add(i,o);
				foreach (EventInfo o in Events) table.Add(i,o);
				foreach (FieldInfo o in Fields) table.Add(i,o);
				foreach (MemberInfo o in Members) table.Add(i,o);
				foreach (MethodInfo o in Methods) table.Add(i,o);
				foreach (Type o in NesT) table.Add(i,o);
				foreach (PropertyInfo o in Properties) table.Add(i,o);
				return table;
			}
			#endregion
			#region ' ToDictionary(object o) '
			public Dictionary<string,string> ToDictionary(object o)
			{
				if (o==null) throw new ArgumentException();
				pov = o; obj = pov.GetType();
				Dictionary<string,string> dic = new Dictionary<string, string>();
				foreach(FieldInfo fi in Fields) { string str = GetDeep<string>(obj,fi.Name); if (str!=null) dic.Add(fi.Name,str); }
				return dic;
			}
			#endregion
			#region ' Call '
			static public T Call<T>(string f, object o, object[] args){
				MethodInfo mo;
				if ((mo = o.GetType().GetMethod(f))==null) throw new Exception("What are you doing?");
				return (T)mo.Invoke(o,args);
			}
			static public T Call<T>(string f, object o, object arg){ return (T)Call<T>(f,o,new object[]{arg}); }
			static public T Call<T>(string f, object o){ return (T)Call<T>(f,o,Type.Missing); }
			#endregion
			#region ' object GetField '
			static public object GetField(object o, string p){
				if (p.IndexOf('.')>=0) return GetDeep(o,p);
				FieldInfo pinfo;
				if (( pinfo = o.GetType().GetField(p))==null)
				{
					throw(new Exception(
						"property.value does not exist!??".Replace(
							"property",o.ToString()
						).Replace("value",p)
					));
					//	return default(T);
				}
				// if this doesn't work try using the o as a first param.
				return pinfo.GetValue(o);
			}
			static public T GetField<T>(object o, string p){ return (T) GetField(o,p); }
			#endregion
			#region ' bool SetField '
			static public bool SetField(object o, string p, object v){
				FieldInfo pnfo;
				if ((pnfo=o.GetType().GetField(p))==null) return false;
				pnfo.SetValue(o,v);
				return true;
			}
			#endregion
			#region ' (object|T) GetProp '
			static public object GetProp(object o, string p){
				if (p.IndexOf('.')>=0) return GetDeep(o,p);
				PropertyInfo pinfo;
				if (( pinfo = o.GetType().GetProperty(p))==null)
				{
					throw(new Exception(
						"property.value does not exist!??".Replace(
							"property",o.ToString()
						).Replace("value",p)
					));
				}
				// if this doesn't work try using the o as a first param.
				return pinfo.GetValue(o,null);
			}
			static public T GetProp<T>(object o, string p){
				return (T) GetProp(o,p);
			}
			#endregion
			#region ' bool SetProp '
			static public bool SetProp(object o, string p, object v){
				PropertyInfo pnfo;
				if ((pnfo=o.GetType().GetProperty(p))==null) {
					return false;
				}
				pnfo.SetValue(o,v,null);
				return true;
			}
			#endregion
			#region ' (object|T) GetDeep '
			static public object GetDeep(object o, string p)
			{
				object returnme = null;
				string[] intern = p.Split('.');
				if (p==null)
				{
					throw new Exception("no sub properties, be sure you're calling the appropriate method");
				}
				else
				{
					returnme = o;
					foreach (string prop in intern)
					{
						try
						{
							returnme = GetProp(returnme,prop);
						}
						catch
						{
							try
							{
								returnme = GetField(returnme,prop);
							}
							catch
							{
								throw new Exception("No Field or Property found");
							}
						}
					}
					//return GetField(o,p);
				}
				return returnme;
			}
			static public T GetDeep<T>(object o, string p) { return (T)GetDeep(o,p); }
			#endregion
			#region ' object GetFieldDeep '
			static public object GetFieldDeep(object o, string p)
			{
				object returnme = null;
				string[] intern = p.Split('.');
				if (p==null)
				{
					throw new Exception("no sub properties, be sure you're calling the appropriate method");
				}
				else
				{
					returnme = o;
					foreach (string prop in intern)
					{
						returnme = GetField(returnme,prop);
					}
					//return GetField(o,p);
				}
				return returnme;
			}
			#endregion
			
		}
	}
}
