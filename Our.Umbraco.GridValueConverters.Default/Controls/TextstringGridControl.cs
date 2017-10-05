﻿using System;
using System.Web;
using Newtonsoft.Json;
using Our.Umbraco.GridValueConverters.Controls;
using Umbraco.Web;

namespace Our.Umbraco.GridValueConverters.Default.Controls
{
	/// <summary>
	/// The textstring grid control.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Controls.HtmlGridControl{Our.Umbraco.GridValueConverters.Default.Controls.TextstringGridConfig}" />
	[GridControl(EditorView = "textstring")]
	public class TextstringGridControl : HtmlGridControl<TextstringGridConfig>
	{
		/// <summary>
		/// Gets the HTML value.
		/// </summary>
		/// <returns>
		/// The HTML value.
		/// </returns>
		/// <remarks>
		/// This method is only invoked once when the HTML value is needed and can be used to process the value.
		/// </remarks>
		protected override IHtmlString GetHtmlValue()
		{
			// Be sure to HTML encode the value!
			var value = HttpUtility.HtmlEncode(this.Value);

			var config = this.Editor?.Config;
			if (config != null)
			{
				string markup = config.Markup, style = config.Style;
				if (!String.IsNullOrEmpty(markup))
				{
					// Just create a new instance of HtmlStringUtilities, since this method is only called once and we don't need UmbracoHelper
					markup = markup.Replace("#value#", new HtmlStringUtilities().ReplaceLineBreaksForHtml(value));
					markup = markup.Replace("#style#", style);

					return new HtmlString(markup);
				}
				else if (!String.IsNullOrEmpty(style))
				{
					markup = $"<div style=\"{style}\">{value}</div>";

					return new HtmlString(markup);
				}
			}

			return new HtmlString(value);
		}
	}

	/// <summary>
	/// The textstring grid editor config.
	/// </summary>
	public class TextstringGridConfig
	{
		/// <summary>
		/// Gets or sets the style properties for the text.
		/// </summary>
		/// <value>
		/// The style properties for the text.
		/// </value>
		[JsonProperty("style")]
		public string Style { get; set; }

		/// <summary>
		/// Gets or sets the markup for the text.
		/// </summary>
		/// <value>
		/// The markup for the text.
		/// </value>
		[JsonProperty("markup")]
		public string Markup { get; set; }
	}
}