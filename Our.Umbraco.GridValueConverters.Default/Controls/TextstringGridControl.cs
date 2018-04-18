using Our.Umbraco.GridValueConverters.Default.Configs;
using Our.Umbraco.GridValueConverters.Default.Values;
using Our.Umbraco.GridValueConverters.Models;

namespace Our.Umbraco.GridValueConverters.Default.Controls
{
	/// <summary>
	/// The textstring grid control.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.Control{Our.Umbraco.GridValueConverters.Default.Values.TextstringGridValue, Our.Umbraco.GridValueConverters.Default.Configs.TextstringGridConfig}" />
	[GridControl(EditorView = "textstring")]
	public class TextstringGridControl : Grid.Control<TextstringGridValue, TextstringGridConfig>
	{
		/// <summary>
		/// Gets or sets the control value.
		/// </summary>
		/// <value>
		/// The control value.
		/// </value>
		public override TextstringGridValue Value
		{
			get
			{
				var value = base.Value;
				if (value != null)
				{
					// Inject config to value
					value.Config = this.Editor?.Config;
				}

				return value;
			}
			set => base.Value = value;
		}
	}
}