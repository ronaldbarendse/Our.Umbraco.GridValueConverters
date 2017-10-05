using System.Collections.Generic;
using Newtonsoft.Json;
using Our.Umbraco.GridValueConverters.Models;

namespace Our.Umbraco.GridValueConverters.Default.Controls
{
	/// <summary>
	/// The macro grid control.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.Control{Our.Umbraco.GridValueConverters.Default.Controls.MacroGridValue}" />
	[GridControl(EditorView = "macro")]
	public class MacroGridControl : Grid.Control<MacroGridValue>
	{ }

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