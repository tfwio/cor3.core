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
namespace System.Cor3 {

	[Flags] public enum TSpec
	{
		ATTRIBUTES_DEEP,
		ATTRIBUTES,
		CONSTRUCTORS,
		EVENTS,
		FIELDS,
		MEMBERS,
		METHODS,
		NESTED_TYPES,
		PROPERTIES,
	}
	public class introvert : IDisposable
	{
		const TSpec ALLSPEC =
			TSpec.CONSTRUCTORS|
			TSpec.ATTRIBUTES|
			TSpec.ATTRIBUTES_DEEP|
			TSpec.EVENTS|
			TSpec.FIELDS|
			TSpec.MEMBERS|
			TSpec.METHODS|
			TSpec.NESTED_TYPES|
			TSpec.PROPERTIES;
		/// <summary>
		/// <para>I say that this is not working for XmlEnumAttribute.</para>
		/// <para>…And also that I'm not sure if this works at all considering how long it's been since this was
		/// either elaborated or tested.</para>
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		static public bool Contains<T>(object obj)
		{
			//			Global.stat("att-len:{0}",obj.GetType().GetCustomAttributes(true).Length);
			foreach (PropertyInfo p in obj.GetType().GetProperties())
			{
				foreach (Attribute sat in p.GetCustomAttributes(true))
				{
					if (sat.GetType().Equals(typeof(T)))
					{
						Global.cstat(ConsoleColor.Green,"{0}:{1}",p.Name,sat);
						return true;
					}
				}
			}
			return false;
			//			FieldInfo[] fi = typeof(obj).
		}
		
		public object pov;
		public object Pov { get { if (pov==null) return null; return pov; } set { if (value==null) throw new ArgumentException("Value can not be null."); pov = value; obj = pov.GetType(); } }
		public Type obj;
		
		public bool IsNull { get { return Pov==null; } }
		
		public ConstructorInfo[] Constructors { get { return obj.GetConstructors(); } set {} }
		public object[] AttributesDeep { get { return (IsNull) ? null : obj.GetCustomAttributes(true); } }
		public object[] Attributes { get { return (IsNull) ? null : obj.GetCustomAttributes(false); } }
		public EventInfo[] Events { get { return (IsNull) ? null : obj.GetEvents(); } set {} }
		public FieldInfo[] Fields { get { return (IsNull) ? null : obj.GetFields(); } set {} }
		public MemberInfo[] Members { get { return (IsNull) ? null : obj.GetMembers(); } set {} }
		public MethodInfo[] Methods { get { return (IsNull) ? null : obj.GetMethods(); } set {} }
		public Type[] NesT { get { return (IsNull) ? null : obj.GetNestedTypes(); } set {} }
		public PropertyInfo[] Properties { get { return (IsNull) ? null : obj.GetProperties(); } set {} }
		
		public introvert(object o) {
			Pov = o;
		}
		
		/// <summary>
		/// this call has not been completed
		/// </summary>
		/// <returns></returns>
		public Hashtable ToHashtable(/*TSpec types_to_get*/)
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
		public Dictionary<string,string> ToDictionary(object o)
		{
			if (o==null) throw new ArgumentException();
			pov = o; obj = pov.GetType();
			Dictionary<string,string> dic = new Dictionary<string, string>();
			foreach(FieldInfo fi in Fields) { string str = GetDeep<string>(obj,fi.Name); if (str!=null) dic.Add(fi.Name,str); }
			return dic;
		}
		
		static public T Call<T>(string f, object o, object[] args){
			MethodInfo mo;
			if ((mo = o.GetType().GetMethod(f))==null) throw new Exception("What are you doing?");
			return (T)mo.Invoke(o,args);
		}
		static public T Call<T>(string f, object o, object arg){ return (T)Call<T>(f,o,new object[]{arg}); }
		static public T Call<T>(string f, object o){ return (T)Call<T>(f,o,Type.Missing); }
		
		static public object GetField(object o, string p)
		{
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
		
		static public bool SetField(object o, string p, object v){
			FieldInfo pnfo;
			if ((pnfo=o.GetType().GetField(p))==null) return false;
			pnfo.SetValue(o,v);
			return true;
		}
		
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
		
		static public bool SetProp(object o, string p, object v){
			PropertyInfo pnfo;
			if ((pnfo=o.GetType().GetProperty(p))==null) {
				return false;
			}
			pnfo.SetValue(o,v,null);
			return true;
		}
		
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
		
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
