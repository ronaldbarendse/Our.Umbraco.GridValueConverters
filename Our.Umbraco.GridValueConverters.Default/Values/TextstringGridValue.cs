using System;
using System.Web;
using Our.Umbraco.GridValueConverters.Default.Configs;
using Our.Umbraco.GridValueConverters.Values;
using Umbraco.Web;

namespace Our.Umbraco.GridValueConverters.Default.Values
{
	/// <summary>
	/// The textstring grid value.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Values.HtmlGridValue" />
	public class TextstringGridValue : HtmlGridValue
	{
		/// <summary>
		/// Gets or sets the editor configuration.
		/// </summary>
		/// <value>
		/// The editor configuration.
		/// </value>
		internal TextstringGridConfig Config { get; set; }

		/// <summary>
		/// Gets the HTML value.
		/// </summary>
		/// <returns>
		/// The HTML value.
		/// </returns>
		/// <remarks>
		/// This method is only invoked once when the HTML value is needed and can be used to process the value.
		/// </remarks>
		protected override string GetHtmlValue()
		{
			// Be sure to HTML encode the value!
			var value = HttpUtility.HtmlEncode(this.Html);
			if (!String.IsNullOrEmpty(value))
			{
				var config = this.Config;
				if (config != null)
				{
					string markup = config.Markup, style = config.Style;
					if (!String.IsNullOrEmpty(markup))
					{
						// Just create a new instance of HtmlStringUtilities, since this method is only called once and we don't need UmbracoHelper
						markup = markup.Replace("#value#", new HtmlStringUtilities().ReplaceLineBreaksForHtml(value));
						markup = markup.Replace("#style#", style);

						return markup;
					}
					else if (!String.IsNullOrEmpty(style))
					{
						markup = $"<div style=\"{style}\">{value}</div>";

						return markup;
					}
				}
			}

			return value;
		}
	}
}
