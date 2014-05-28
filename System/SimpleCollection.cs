/*
 * Created by SharpDevelop.
 * User: tfw
 * Date: 9/19/2008
 * Time: 3:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Collections;
using System.Collections.Generic;

/* oOo * 11/14/2007 : 10:50 PM */
namespace System
{
	namespace Cor3
	{
		/* oOo * 11/19/2007 : 8:00 AM */
		public class SimpleCollection<T> : CollectionBase
		{
			public SimpleCollection() : base() {}
			public SimpleCollection(int capacity) : base(capacity) {}
			public SimpleCollection(T[] input) { AddRange(input); }
			public SimpleCollection(List<T> input) { AddRange(input); }
		//		~SimpleCollection()
		//		{
		//			InnerList.Clear();
		//		}
			virtual public T this[int i] { get { return (T)List[i]; } set { List[i] = (T)value;} }
			virtual public int AddObject(object item) { return List.Add(item); }
			virtual public void ReoveObject(object item) { List.Remove(item); }
			virtual public object GetObject(object item) { return List[GetIndex(item)]; }
			virtual public int GetIndex(object item) { return List.IndexOf(item); }
			virtual public int Add(T item) { return List.Add(item); }
			virtual public void AddRange(T[] array) { InnerList.AddRange(array); }
			virtual public void AddRange(List<T> array) { InnerList.AddRange(array); }
			virtual public void Remove(T item) { List.Remove(item); }
			virtual public T[] ToArray() { return (T[])InnerList.ToArray(typeof(T)); }
			virtual public bool Contains(T value)
			{
				return InnerList.Contains(value);
			}
		}
	}
}
