using Our.Umbraco.GridValueConverters.Default.Values;
using Our.Umbraco.GridValueConverters.Models;

namespace Our.Umbraco.GridValueConverters.Default.Controls
{
	/// <summary>
	/// The macro grid control.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.Control{Our.Umbraco.GridValueConverters.Default.Values.MacroGridValue}" />
	[GridControl(EditorView = "macro")]
	public class MacroGridControl : Grid.Control<MacroGridValue>
	{ }
}