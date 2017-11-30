using Newtonsoft.Json;

namespace Our.Umbraco.GridValueConverters.Default.Configs
{
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
