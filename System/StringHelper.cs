/**
 * oIo * 2/23/2011 3:51 PM
 **/

namespace System
{
	/// <para>
	/// quick helpers for string data conversions.
	/// This has been written particularly to replace a ‘mop’ function
	/// from the ‘STR’ class (my old string lib)
	/// </para>
	public class StringHelper
	{
		static public string GetAnsiChars(params char[] chars)
		{
			byte[] copy = System.Text.Encoding.ASCII.GetBytes(chars);
			string returnValue = System.Text.Encoding.ASCII.GetString(copy);
			Array.Clear(copy,0,copy.Length);
			return returnValue;
		}

		/// <summary>reverse a bit array</summary>
		/// <param name="bits">the result is reversed (for little-endian/big-endian swapping)</param>
		/// <returns>a reversed array of bits.</returns>
		static public byte[] convb(byte[] bits)
		{
			if (BitConverter.IsLittleEndian)
				Array.Reverse(bits);
			return bits;
		}

		#region GetBit
		/// <summary>string to byte[] conversion</summary>
		/// <remarks>uses System.Text.Encoding.Default</remarks>
		static public byte[] getBit(string inpoo)
		{
			return System.Text.Encoding.Default.GetBytes(inpoo);
		}
		/// <summary>byte[] to string conversion</summary>
		static public byte[] getBit(string inpoo, System.Text.Encoding Enc)
		{
			return Enc.GetBytes(inpoo);
		}
		#endregion
	}
}
