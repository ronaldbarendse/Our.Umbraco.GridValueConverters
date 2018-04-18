using System.Web;
using Newtonsoft.Json;
using Our.Umbraco.GridValueConverters.JsonConverters;

namespace Our.Umbraco.GridValueConverters.Values
{
	/// <summary>
	/// The HTML grid value.
	/// </summary>
	/// <seealso cref="System.Web.IHtmlString" />
	[JsonConverter(typeof(HtmlGridValueConverter))]
	public class HtmlGridValue : IHtmlString
	{
		/// <summary>
		/// The HTML value.
		/// </summary>
		private string htmlValue;

		/// <summary>
		/// Gets the HTML.
		/// </summary>
		/// <value>
		/// The HTML.
		/// </value>
		public string Html { get; internal set; }

		/// <summary>
		/// Gets the HTML value.
		/// </summary>
		/// <returns>
		/// The HTML value.
		/// </returns>
		/// <remarks>
		/// This method is only invoked once when the HTML value is needed and can be used to process the value.
		/// </remarks>
		protected virtual string GetHtmlValue()
		{
			return this.Html;
		}

		/// <summary>
		/// Returns an HTML-encoded string.
		/// </summary>
		/// <returns>
		/// An HTML-encoded string.
		/// </returns>
		public string ToHtmlString()
		{
			return this.htmlValue ?? (this.htmlValue = this.GetHtmlValue());
		}
	}
}
