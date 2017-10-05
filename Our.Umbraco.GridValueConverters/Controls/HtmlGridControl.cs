using System.Web;
using Newtonsoft.Json;
using Our.Umbraco.GridValueConverters.JsonConverters;
using static Our.Umbraco.GridValueConverters.Models.Grid;

namespace Our.Umbraco.GridValueConverters.Controls
{
	/// <summary>
	/// The grid control with an HTML value.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.Control{System.String}" />
	public class HtmlGridControl : Control<string>
	{
		/// <summary>
		/// The lazily initialized HTML value.
		/// </summary>
		private IHtmlString htmlValue;

		/// <summary>
		/// Gets or sets the HTML value.
		/// </summary>
		/// <value>
		/// The HTML value.
		/// </value>
		[JsonIgnore]
		public IHtmlString HtmlValue => this.htmlValue ?? (this.htmlValue = this.GetHtmlValue());

		/// <summary>
		/// Gets the HTML value.
		/// </summary>
		/// <returns>
		/// The HTML value.
		/// </returns>
		/// <remarks>
		/// This method is only invoked once when the HTML value is needed and can be used to process the value.
		/// </remarks>
		protected virtual IHtmlString GetHtmlValue()
		{
			return new HtmlString(this.Value);
		}
	}

	/// <summary>
	/// The grid control with an HTML value and editor config value.
	/// </summary>
	/// <typeparam name="TEditorConfig">The type of the editor configuration.</typeparam>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.Control{System.String}" />
	public class HtmlGridControl<TEditorConfig> : HtmlGridControl, IControl<string, TEditorConfig>
	{
		/// <summary>
		/// Gets or sets the grid editor.
		/// </summary>
		/// <value>
		/// The grid editor.
		/// </value>
		[JsonConverter(typeof(GridEditorConverter))]
		[JsonProperty("editor")]
		public new IEditor<TEditorConfig> Editor
		{
			get
			{
				return (IEditor<TEditorConfig>)base.Editor;
			}
			set
			{
				base.Editor = value;
			}
		}
	}
}
