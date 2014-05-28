/* User: oIo * Date: 8/18/2010 * Time: 4:27 AM */

namespace System
{
	using System.Collections.Generic;
	/// <summary>
	/// This is a common syntax driven class and can conform to several specified
	/// tactics by initializing with a supported <see cref="System.SVarFormat" />.
	/// <para>
	/// note that this class is generally used to represent a Field within a (SQL) Query-Expression.
	/// </para>
	/// </summary>
	/// <see cref="System.query_field" />
	public class SVar : query_field
	{
		/// <summary>
		/// the (protected) default SVarFormat Type (<code>SVarFormat.VarDollar</code>).
		/// </summary>
		protected SVarFormat formatType = SVarFormat.VarDollar;
		/// <summary>
		/// the (public) default SVarFormat Type (<code>SVarFormat.VarDollar</code>).
		/// </summary>
		virtual public SVarFormat FormatType { get { return formatType; } }

//		static protected SVar instance = new SVar(true);
//		public static SVar Instance { get { return instance; } }

		#region Const Filters
		internal protected const string fmt_crly = "{{0}}";
		internal protected const string fmt_aquod = @"""{0}""";
		internal protected const string fmt_aquot = "'{0}'";
		internal protected const string fmt_quod = "“{0}”";
		internal protected const string fmt_quot = "‘{0}’";
		internal protected const string fmt_doll = "$({0})";
		internal protected const string fmt_hash = "#({0})";
		internal protected const string fmt_amp = "@{0}";
		#endregion
		#region Expression Formats
		/// <summary>
		/// Curly: <code>{Value}</code>
		/// </summary>
		protected string _curl { get { return  string.Format(fmt_crly,_value); } }
		/// <summary>
		/// Ansi Double Quotation: <code>"Value"</code>
		/// </summary>
		protected string _ansQuoteDbl { get { return  string.Format(fmt_quod,_value); } }
		/// <summary>
		/// Ansi Quotation: <code>'Value'</code>
		/// </summary>
		protected string _ansQuote { get { return  string.Format(fmt_aquot,_value); } }
		/// <summary>
		/// Double Quotation: <code>“Value”</code>
		/// </summary>
		protected string _QuoteDbl { get { return  string.Format(fmt_quod,_value); } }
		/// <summary>
		/// Quotation: <code>‘Value’</code>
		/// </summary>
		protected string _Quote { get { return  string.Format(fmt_quot,_value); } }
		/// <summary>
		/// Dollar Symbol: <code>$(Value)</code>
		/// </summary>
		protected string _VarDlr { get { return  string.Format(fmt_doll,_value); } }
		/// <summary>
		/// Hash/Pound Symbol: <code>#(Value)</code>
		/// </summary>
		protected string _VarHash { get { return  string.Format(fmt_hash,_value); } }
		/// <summary>
		/// Ampersand: <code>@Value</code>
		/// </summary>
		protected string _VarAmp { get { return  string.Format(fmt_amp,_value); } }
		#endregion
		
		public string this[SVarFormat fmt]
		{
			get {
				if (Formats.ContainsKey(fmt)) throw new ArgumentException("Key not found");
				return Formats[fmt];
			}
		}
		public IDictionary<SVarFormat,string> Formats = new Dictionary<SVarFormat,string>();

		public SVar (string input, SVarFormat type) : this(true) { _value = input; formatType = type; }
		public SVar (string input) : this(input,SVarFormat.VarSQL) { }
		public SVar (SVarFormat type) : this(true) { formatType = type; }
		public SVar (bool initDictionary) : base() { if (initDictionary) InitializeDictionary(); }

		protected void InitializeDictionary()
		{
			if (IsInitialized) return;
			Formats.Add(SVarFormat.VarSQL,fmt_hash);
			Formats.Add(SVarFormat.VarHash,fmt_hash);
			Formats.Add(SVarFormat.VarDollar,fmt_doll);
			Formats.Add(SVarFormat.VarCurly,fmt_crly);
			Formats.Add(SVarFormat.VarAmpersand,fmt_amp);
			Formats.Add(SVarFormat.Quoted,fmt_quot);
			Formats.Add(SVarFormat.QuotedAns,fmt_aquot);
			Formats.Add(SVarFormat.DoubleQuoted,fmt_quod);
			Formats.Add(SVarFormat.DoubleQuotedAns,fmt_aquod);
			IsInitialized = true;
		}
		protected bool IsInitialized = false;

		public override string ToString() { return this.Value; }
		
		public string Format(string expression, params object[] value) {
			return Reformat(expression, FormatType, Value);
		}
		public string Reformat(string expression, SVarFormat fmt, params object[] str) {
			return string.Format( expression, string.Format(Formats[fmt],str) );
		}
		
		static public implicit operator SVar(string s){ return new SVar(string.Copy(s)); }
		static public implicit operator string(SVar s){ return string.Copy(s.Value); }
	}

}
