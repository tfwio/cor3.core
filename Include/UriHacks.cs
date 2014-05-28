// include in your project
using System.Reflection;
namespace System
{
	
	static class UriHacks // thanks to stackoverflow
	{
		private const int CompressPath = 0x800000;
		private const int UnEscapeDotsAndSlashes = 0x2000000;
		public static void LeaveDotsAndSlashesEscaped(this Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			
			FieldInfo fieldInfo = uri.GetType().GetField("m_Syntax", BindingFlags.Instance | BindingFlags.NonPublic);
			if (fieldInfo == null)
			{
				throw new MissingFieldException("'m_Syntax' field not found");
			}
			object uriParser = fieldInfo.GetValue(uri);
			
			fieldInfo = typeof(UriParser).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
			if (fieldInfo == null)
			{
				throw new MissingFieldException("'m_Flags' field not found");
			}
			object uriSyntaxFlags = fieldInfo.GetValue(uriParser);
			
			// Clear the flag that we don't want
			uriSyntaxFlags = (int)uriSyntaxFlags & ~UnEscapeDotsAndSlashes;
			
			fieldInfo.SetValue(uriParser, uriSyntaxFlags);
		}
		public static void LeaveMultipleSlashesAsIs(this Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			
			FieldInfo fieldInfo = uri.GetType().GetField("m_Syntax", BindingFlags.Instance | BindingFlags.NonPublic);
			if (fieldInfo == null)
			{
				throw new MissingFieldException("'m_Syntax' field not found");
			}
			object uriParser = fieldInfo.GetValue(uri);
			
			fieldInfo = typeof(UriParser).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
			if (fieldInfo == null)
			{
				throw new MissingFieldException("'m_Flags' field not found");
			}
			object uriSyntaxFlags = fieldInfo.GetValue(uriParser);
			
			// Clear the flag that we don't want
			uriSyntaxFlags = (int)uriSyntaxFlags & ~CompressPath;
			
			fieldInfo.SetValue(uriParser, uriSyntaxFlags);
		}
		
		public static void EnableHacks(this Uri uri)
		{
			uri.LeaveDotsAndSlashesEscaped();
			uri.LeaveMultipleSlashesAsIs();
		}
	}
		/// <summary>
	/// The general idea of this enumeration would be decoding.
	/// </summary>
	public enum EntityFilterType
	{
		/// <summary>
		/// '%56%2F' to '%2F' or '\/' to '/' where 56 and 2F are hex.
		/// This is usually paired with DecodeUniChar
		/// </summary>
		Dec562F,
		EncHtmlEntity,
		DecHtmlEntity,
//			EncodeUni,
		DecUnicodeChars,
		EncEscape,
		DecEscape,
//			Encode,
		DecUriChars,
	}

}
