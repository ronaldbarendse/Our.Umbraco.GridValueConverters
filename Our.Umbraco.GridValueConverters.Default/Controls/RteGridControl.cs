using Our.Umbraco.GridValueConverters.Models;
using Our.Umbraco.GridValueConverters.Values;

namespace Our.Umbraco.GridValueConverters.Default.Controls
{
	/// <summary>
	/// The rich text grid control.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Controls.HtmlGridControl" />
	[GridControl(EditorView = "rte")]
	public class RteGridControl : Grid.Control<RteGridValue>
	{ }
}