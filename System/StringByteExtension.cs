/* User: oIo * Date: 8/18/2010 * Time: 4:27 AM */

namespace System
{
	static class StringByteExtension
	{
		static public string StringifyAscii(this byte[] bytes) { return bytes.Stringify(System.Text.Encoding.ASCII); }
		static public string StringifyUTF8(this byte[] bytes) { return bytes.Stringify(System.Text.Encoding.UTF8); }
		static public string StringifyOs(this byte[] bytes) { return bytes.Stringify(System.Text.Encoding.Default); }
		static public string Stringify(this byte[] bytes, System.Text.Encoding encoder) { return encoder.GetString(bytes); }
		
		// FROM MIDI SMF Project
		/// <summary>converts a byte array to a string</summary>
		/// <remarks>The method is particularly used to print HEX
		/// Strings out in a human readable form.</remarks>
		static public string StringifyHex(this byte[] inb)
		{
			if (inb==null) return string.Empty;
			string bish = "";
			foreach (byte c in inb) { bish += c.ToString("X2").PadLeft(2,'0')+" "; }
			return bish.TrimEnd();
		}
	}
}
