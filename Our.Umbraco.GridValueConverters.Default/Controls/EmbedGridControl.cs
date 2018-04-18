using Our.Umbraco.GridValueConverters.Models;
using Our.Umbraco.GridValueConverters.Values;

namespace Our.Umbraco.GridValueConverters.Default.Controls
{
	/// <summary>
	/// The grid control for the built-in embed.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.Control{Our.Umbraco.GridValueConverters.Values.HtmlGridValue}" />
	[GridControl(EditorView = "embed")]
	public class EmbedGridControl : Grid.Control<HtmlGridValue>
	{ }
}