using Our.Umbraco.GridValueConverters.Controls;

namespace Our.Umbraco.GridValueConverters.Default.Controls
{
	/// <summary>
	/// The grid control for the built-in embed.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Controls.HtmlGridControl" />
	[GridControl(EditorView = "embed")]
	public class EmbedGridControl : HtmlGridControl
	{ }
}