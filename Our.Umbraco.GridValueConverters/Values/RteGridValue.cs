using System;
using Umbraco.Web;
using Umbraco.Web.Templates;

namespace Our.Umbraco.GridValueConverters.Values
{
	/// <summary>
	/// The rich text grid value.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Values.HtmlGridValue" />
	public class RteGridValue : HtmlGridValue
	{
		/// <summary>
		/// Gets the Umbraco context.
		/// </summary>
		/// <value>
		/// The Umbraco context.
		/// </value>
		protected UmbracoContext UmbracoContext { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="RteGridValue" /> class.
		/// </summary>
		public RteGridValue()
			: this(UmbracoContext.Current)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="RteGridValue" /> class.
		/// </summary>
		/// <param name="umbracoContext">The Umbraco context.</param>
		/// <exception cref="ArgumentNullException">umbracoContext</exception>
		public RteGridValue(UmbracoContext umbracoContext)
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
		protected override string GetHtmlValue()
		{
			string value = this.Html;
			if (!String.IsNullOrEmpty(value))
			{
				value = TemplateUtilities.ParseInternalLinks(value, this.UmbracoContext.UrlProvider);
				value = TemplateUtilities.ResolveUrlsFromTextString(value);
			}

			return value;
		}
	}
}
