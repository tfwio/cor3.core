#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */

using System;
using System.Runtime.InteropServices;

namespace System
{

	unsafe public class Mart : IDisposable
	{
		public void* data;
		public int size = -1;
		public bool AutoUnload = false;
		
		public bool HaveSize { get { return size!=-1; } }
		public bool IsNull { get { return data==null; } }
		/// <summary>
		/// this only checks the last three bits in an intpointer to see that
		/// they are zero.  Errors may happen in the case the first bit is also zero.
		/// </summary>
		public bool IsIntResource
		{
			get
			{
				byte[] b = PointerBits;
				bool bvalue = true;
				for (int i=1;i<b.Length;i++) if (b[i]!=0) bvalue = false;
				Array.Clear(b,0,b.Length);
				return bvalue;
			}
		}
		
		static public implicit operator IntPtr (Mart mem){ return (IntPtr)mem.data; }
		static public implicit operator String (Mart mem){ return mem.GetStringAuto(); }
		static public implicit operator Int32 (Mart mem){ return mem.GetInt(); }
		static public implicit operator UInt32 (Mart mem){ return mem.GetUInt(); }
		static public implicit operator UInt16 (Mart mem){ return mem.GetWord(); }
		static public implicit operator Int16 (Mart mem){ return mem.GetInt16(); }
		static public implicit operator byte[] (Mart mem){ return mem.GetByteData(mem.size); }
		static public implicit operator Mart (IntPtr mem){ return new Mart(mem); }
		static public implicit operator Mart (String mem){ return new Mart(mem); }
		static public implicit operator Mart (byte[] mem){ return new Mart(mem); }
		
		public string GetStringAnsi() { return Marshal.PtrToStringAnsi(this); }
		public string GetStringAuto() { return Marshal.PtrToStringAuto(this); }
		public UInt16 GetWord() { return BitConverter.ToUInt16(GetByteData(2),0); }
		public Int16 GetInt16() { return BitConverter.ToInt16(GetByteData(2),0); }
		public Int32 GetInt() { return BitConverter.ToInt32(GetByteData(4),0); }
		public UInt32 GetUInt() { return BitConverter.ToUInt32(GetByteData(4),0); }
		
		public byte[] PointerBits // may need to be mended for 64Bit Systems
			// in consideration that this is IS_INTRESOURCE
			// helper...
		{
			get
			{
				return (IntPtr.Size == 4) ?
					BitConverter.GetBytes(((IntPtr)this).ToInt32()):
					BitConverter.GetBytes(((IntPtr)this).ToInt64());
			}
		}
		
		public string LittleBits { get { return BID(PointerBits); } }
		
		// to be moved to a utility class
		public string BID(params byte[] bytes)
		{
			string returnvalue = string.Empty;
			foreach (byte bit in bytes)
				returnvalue += string.Format("{0}.",bit);
			return returnvalue = returnvalue.TrimEnd('.');
		}
		
		public byte[] GetByteData(int length)
		{
			byte[] rdata = new byte[length];
			Marshal.Copy((IntPtr)data,rdata,0,length);
			return rdata;
		}
		
		public void Free()
		{
			try
			{
				Global.cstat(ConsoleColor.Yellow,"Mart: Freeing Memory");
				Marshal.FreeCoTaskMem(this);
				//				Global.cstat(ConsoleColor.Yellow,"Mart: Releasing Memory");
				//				Marshal.Release(this);
			}
			catch { Global.cstat(ConsoleColor.Red,"an error was encountered trying to free the data."); }
			finally { Global.cstat(ConsoleColor.Green,"mem-free complete"); }
		}
		
		public Mart(void* data_in) { data = data_in; }
		
		public Mart(string data_in) : this(data_in,false) {}
		public Mart(string data_in, bool ignore_unload)
		{
			AutoUnload = ignore_unload;
			size = data_in.Length;
			data = (void*)Marshal.StringToCoTaskMemAuto(data_in);
		}
		public Mart(string data_in, bool ignore_unload, bool IsAnsi)
		{
			AutoUnload = ignore_unload;
			size = data_in.Length;
			data = (void*)Marshal.StringToCoTaskMemAnsi(data_in);
		}
		
		public Mart(IntPtr data_in) : this(data_in,true) { }
		public Mart(IntPtr data_in, bool ignore_unload)
		{
			data = (void*) data_in;
			AutoUnload = ignore_unload;
		}
		
		public Mart(params byte[] data_in)
		{
			size = data_in.Length;
			data = (void*)Marshal.AllocCoTaskMem(data_in.Length);
			Marshal.Copy(data_in,0,(IntPtr)data,data_in.Length);
		}
		
		~Mart() { Dispose(); }
		public void Dispose() { if (AutoUnload) Free(); }
	}

}
