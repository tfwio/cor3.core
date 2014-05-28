/* oOo * 11/19/2007 : 8:00 AM */
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.WTF;
using System.Xml.Serialization;

namespace System
{
	public delegate void CallerEvent(object Key,object data);
	
	/// <summary>
	/// This file is an ancient thought, pretty-much kept for no reason.
	/// I believe that this is a part of a little 'NamedObjectFramework'
	/// concept I abandoned years ago.  Since a few interesting tools
	/// were dependent on the presence of this file, it remains.
	/// </summary>
	public class DICTableBase : SerializableClass<DICTableBase>
	{
		[XmlIgnore] public CallerEvent DataChanged;
	
		//.hashtable
		#region \\ Hashtable vals \\
		[Browsable(false),XmlIgnore()] internal Dict _vals = new Dict();
		[XmlIgnore] public CallerEvent DataUpdate;
		[XmlIgnore] public CallerEvent DataRetriever;
		
		public void Caller(object Key,object value)
		{
			if (DataUpdate!=null) DataUpdate(Key,value);
			_vals[Key] = value;
		}
		public object Getter(object Key,object data)
		{
			if (DataRetriever!=null) DataRetriever(Key,data);
			return _vals[Key];
		}
		[Browsable(false),XmlIgnore()] virtual public Dict InnerData { get { return _vals; } set { _vals = value; } }
		#endregion
	
		internal bool Has(object Key) { return _vals.ContainsKey(Key); }
	
		#region \\ object this[object Key] \\
		internal const string ex_argument = "Key[value:\"{0}\"] does not exhist.";
		public string GetStringValue(string Key)
		{
			if (!InnerData.ContainsKey(Key)) throw new ArgumentException(ex_argument);
			return _vals[Key].ToString();
		}
		public object this[object Key]
		{
			get
			{
				if (!Has(Key)) throw new ArgumentException(ex_argument);
				return Getter(Key,_vals[Key]); }
			set
			{
				if (!Has(Key)) throw new ArgumentException(string.Format(ex_argument,Key));
				Caller(Key,value);
			}
		}
		#endregion
	
		//.hashtable.medhods
		#region \\ void Add,AddRange \\
		public virtual void Add(object key, object valu) { InnerData.Add(key, valu); }
		public virtual void AddRange(object[] objkey, params object[] objvalue) {
			for (int i=0;i<objkey.Length;i++)
			{
				if (Has(objkey[i])) this[objkey[i]]=objvalue[i];
				else InnerData.Add(objkey[i],objvalue[i]);
			}
		}
		#endregion
		#region \\ void Remove \\
		public virtual void Remove(object key) { InnerData.Remove(key); }
		#endregion
		#region \\ T Reval<T>(object Key) \\
		public T _T<T>(object Key) {
			return (T)_vals[Key];
		}
		#endregion
		#region \\ protected internal virtual void TryAdd \\
		/// <summary>for this to work, the 'key' value must not yet be present in the Hashtable.</summary>
		protected internal virtual void TryAdd(object key, object value) { if (!InnerData.ContainsKey(key)) Add(key,value); }
		/// <summary>for this to work, the respective 'Keys' value must not yet be present in the Hashtable.</summary>
		protected internal virtual void TryAdd(object[] Keys, object[] Values) { for (int i=0; i<Keys.Length;i++) if (!InnerData.ContainsKey(Keys[i])) Add(Keys[i],Values[i]); }
		#endregion
	
		public Color _c(object Key)
		{
			if (!Has(Key)) return Color.Empty;
			return (Color)this[Key];
		}
		public Color _c(object Key, Color ignorethis)
		{
			if (!Has(Key)) return ignorethis;
			return (((Color)this[Key]).Equals(ignorethis)) ? ignorethis :(Color)this[Key];
		}
	
		public TEnum _enu<TEnum>(string input)
		{
			return (TEnum)Enum.Parse(typeof(TEnum),input);
		}
		
		public string _s(object Key)
		{
			if (!Has(Key)) return null;
			return ((string)this[Key]==string.Empty) ? null:this[Key].ToString();
		}
		/// Converts a value to string: Null for false, "True" for true.
		public string _b(object Key)
		{
			if (!Has(Key)) return null;
			return (((bool)this[Key])==false) ? null : this[Key].ToString();
		}
		/// Converts a value to string: Null for false, "True" for true.
		public bool _boo(object Key)
		{
			if (!Has(Key)) return false;
			return _T<bool>(Key);
		}
		/// sets a parameter to the respective value
		public bool _bs(object Key, string value)
		{
			if (!Has(Key)) return false;
			this[Key] = (value.ToLower()=="true")?true:false;
			return _T<bool>(Key);
		}
		/// gets a parameter to the respective value
		public bool _bs(string value)
		{
			return (value.ToLower()=="true")?true:false;
		}
	
	
	}
}
