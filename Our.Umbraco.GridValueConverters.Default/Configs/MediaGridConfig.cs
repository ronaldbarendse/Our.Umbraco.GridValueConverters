using System.Drawing;
using Newtonsoft.Json;
using Our.Umbraco.GridValueConverters.JsonConverters;

namespace Our.Umbraco.GridValueConverters.Default.Configs
{
	/// <summary>
	/// The media grid editor config.
	/// </summary>
	public class MediaGridConfig
	{
		/// <summary>
		/// Gets an object describing the desired size of the media.
		/// </summary>
		/// <value>
		/// The desired size of the media.
		/// </value>
		[JsonProperty("size")]
		[JsonConverter(typeof(SizeJsonConverter))]
		public Size? Size { get; set; }
	}
}
