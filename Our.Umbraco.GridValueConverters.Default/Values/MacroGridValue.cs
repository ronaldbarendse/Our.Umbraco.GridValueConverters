using System.Collections.Generic;
using Newtonsoft.Json;

namespace Our.Umbraco.GridValueConverters.Default.Values
{
	/// <summary>
	/// The macro grid value.
	/// </summary>
	public class MacroGridValue
	{
		/// <summary>
		/// Gets or sets the macro alias.
		/// </summary>
		/// <value>
		/// The macro alias.
		/// </value>
		[JsonProperty("macroAlias")]
		public string MacroAlias { get; set; }

		/// <summary>
		/// Gets or sets the macro parameters.
		/// </summary>
		/// <value>
		/// The macro parameters.
		/// </value>
		[JsonProperty("macroParamsDictionary")]
		public Dictionary<string, object> MacroParameters { get; set; }
	}
}
