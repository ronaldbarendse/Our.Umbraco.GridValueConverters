using Newtonsoft.Json;

namespace Our.Umbraco.GridValueConverters.Default.Values
{
	/// <summary>
	/// The media grid value.
	/// </summary>
	public class MediaGridValue : ImageGridValue
	{
		/// <summary>
		/// Gets or sets the media caption.
		/// </summary>
		/// <value>
		/// The media caption.
		/// </value>
		/// <remarks>
		/// This should not be in the built-in media grid value (in my option).
		/// </remarks>
		[JsonProperty("caption")]
		public string Caption { get; set; }
	}
}
