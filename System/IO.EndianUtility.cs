/**
 * oIo * 2/23/2011 3:15 AM
 **/

namespace System
{
	class bit
	{
		/// <summary>
		/// A Bitwise check on a set of values.
		/// </summary>
		/// <param name="in_var">our reference: if a reference contains this value, true is the result.</param>
		/// <param name="var_ref">(zero—actually) one or more values to check</param>
		/// <returns>Returns true if any of the provided references (<code>params int[] var_ref</code>) contains the value</returns>
		static public bool Check(int in_var, params int[] var_ref)
		{
			foreach (int bitref in var_ref) if ((in_var&bitref)!=bitref) return false;
			return true;
		}
	}
	/// <summary>
	/// An extension on <see cref="System.bit" />.
	/// </summary>
	class EndianUtil : bit
	{
		/// <summary>
		/// ‘<code>BitConverter.IsLittleEndian</code>’: tells how the CPU processes byte data
		/// by default (from the perspective of the BitConverter).
		/// </summary>
		static public Boolean IsLittleEndian { get { return BitConverter.IsLittleEndian; } }
		/// <summary>
		/// Ensures that a range of bytes is converted to the appropriate “Endian-ness”.
		/// </summary>
		/// <param name="array">the input data</param>
		/// <param name="ToLittleEndian">force endian mode.</param>
		static public void EndianSpecific(byte[] array, bool ToLittleEndian)
		{
			if (!IsLittleEndian && ToLittleEndian) Array.Reverse(array);
			else if (IsLittleEndian && !ToLittleEndian) Array.Reverse(array);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static public uint Reverse(uint value)
		{
			byte[] intv = BitConverter.GetBytes(value);
			Array.Reverse(intv);
			return BitConverter.ToUInt32(intv,0);
		}

		/// <summary>
		/// reverses 32 bit integer
		/// </summary>
		/// <param name="value"></param>
		/// <returns>endian swapped integer (32bit)</returns>
		static public int Reverse(int value)
		{
			byte[] intv = BitConverter.GetBytes(value);
			Array.Reverse(intv);
			return BitConverter.ToInt32(intv,0);
		}
		/// <summary>
		/// Reverses a Byte Array
		/// </summary>
		/// <param name="bits">
		/// the result is reversed (for little-endian/big-endian swapping)
		/// </param>
		/// <returns>Reversed array of bytes as unsigned short.</returns>
		static public ushort Reverse(ushort value)
		{
			byte[] intv = BitConverter.GetBytes(value);
			Array.Reverse(intv);
			return BitConverter.ToUInt16(intv,0);
		}

		/// <summary>
		/// Reverses a Byte Array
		/// </summary>
		/// <param name="bits">
		/// the result is reversed (for little-endian/big-endian swapping)
		/// </param>
		/// <returns>Reversed array of bytes as unsigned short.</returns>
		static public byte[] Reverse(params byte[] bits)
		{
			if (IsLittleEndian) Array.Reverse(bits); return bits;
		}
		/// <summary>
		/// Automatically reads 2 bytes from the reader and reverses them.
		/// </summary>
		/// <param name="br">The reader</param>
		/// <returns>Signed 16-bit integer</returns>
		static public short ReadInt16(System.IO.BinaryReader br)
		{
			return System.Convert.ToInt16(Reverse(br.ReadBytes(2)));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="br"></param>
		/// <returns>Un-signed 16-bit integer</returns>
		static public ushort ReadUInt16(System.IO.BinaryReader br)
		{
			return System.Convert.ToUInt16(Reverse(br.ReadBytes(2)));
		}
		/// <summary>
		/// Automatically reads 4 bytes from the reader and reverses them.
		/// </summary>
		/// <param name="br">The reader</param>
		/// <returns>Signed 32-bit integer</returns>
		static public int ReadInt32(System.IO.BinaryReader br)
		{
			return System.Convert.ToInt32(Reverse(br.ReadBytes(4)));
		}
		/// <summary>
		/// Automatically reads 4 bytes from the reader and reverses them.
		/// </summary>
		/// <param name="br">The reader</param>
		/// <returns>Unsigned 32-bit integer</returns>
		static public uint ReadUInt32(System.IO.BinaryReader br)
		{
			return System.Convert.ToUInt32(Reverse(br.ReadBytes(4)));
		}
	}

}
