using System;
using System.Web;
using Our.Umbraco.GridValueConverters.Controls;
using Umbraco.Web;
using Umbraco.Web.Templates;

namespace Our.Umbraco.GridValueConverters.Default.Controls
{
	/// <summary>
	/// The rich text grid control.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Controls.HtmlGridControl" />
	[GridControl(EditorView = "rte")]
	public class RteGridControl : HtmlGridControl
	{
		/// <summary>
		/// Gets the Umbraco context.
		/// </summary>
		/// <value>
		/// The Umbraco context.
		/// </value>
		protected UmbracoContext UmbracoContext { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="RteGridControl"/> class.
		/// </summary>
		public RteGridControl()
			: this(UmbracoContext.Current)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="RteGridControl" /> class.
		/// </summary>
		/// <param name="umbracoContext">The Umbraco context.</param>
		/// <exception cref="ArgumentNullException">umbracoContext</exception>
		public RteGridControl(UmbracoContext umbracoContext)
		{
			this.UmbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
		}

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
			string value = this.Value;
			if (!String.IsNullOrEmpty(value))
			{
				value = TemplateUtilities.ParseInternalLinks(value, this.UmbracoContext.UrlProvider);
				value = TemplateUtilities.ResolveUrlsFromTextString(value);
			}

			return new HtmlString(value);
		}
	}
}