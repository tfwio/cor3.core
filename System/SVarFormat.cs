/* User: oIo * Date: 8/18/2010 * Time: 4:27 AM */

namespace System
{
	using System.Collections.Generic;
	public enum SVarFormat
	{
		Undefined,
		Quoted,DoubleQuoted,
		/// <summary>"ansi-double-quotation-example"</summary>
		QuotedAns,
		/// <summary>
		/// <para>“double-quoted-string-example”</para>
		/// <para>• Uses encoding specific Begin and End Quotations (two different quote characters)</para>
		/// <para>• Note that this is rarely used, so probably isn't implemented anywhere (here either)</para>
		/// </summary>
		DoubleQuotedAns,
		/// <summary>{curly-field-example}</summary>
		VarCurly,
		///<summary>@ampsersand-field-example</summary>
		VarAmpersand,
		///<summary>#hash-field-example</summary>
		VarHash,
		///<summary>$(x-script-field-example)</summary>
		VarDollar,
		///<summary>`my-sql-data-field-example`</summary>
		VarSQL,
		///<summary>[sqlserver-field-example]</summary>
		VarSqlServer
	}

}
