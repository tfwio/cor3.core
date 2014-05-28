/* oOo * 11/14/2007 : 10:22 PM */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace System.IO
{
	/// <summary>
	/// just implement what you will.
	/// </summary>
	[Serializable]
	[Obsolete]
	public class SerializableList<T>
	{
		[XmlIgnore] protected internal List<T> innerList = new List<T>();
		[XmlElement("item")]
		public T[] Items { get { return innerList.ToArray(); } set { innerList = new List<T>(value); } }
		virtual public void Add(T item) { innerList.Add(item); }
		public void Clear() { innerList.Clear(); }
		/// Empty Initialization (for serialization)
		public SerializableList() {}
	}

}
