using Our.Umbraco.GridValueConverters.Default.Configs;
using Our.Umbraco.GridValueConverters.Default.Values;
using Our.Umbraco.GridValueConverters.Models;

namespace Our.Umbraco.GridValueConverters.Default.Controls
{
	/// <summary>
	/// The media grid control.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.Control{Our.Umbraco.GridValueConverters.Default.Values.MediaGridValue, Our.Umbraco.GridValueConverters.Default.Configs.MediaGridConfig}" />
	[GridControl(EditorView = "media")]
	public class MediaGridControl : Grid.Control<MediaGridValue, MediaGridConfig>
	{
		/// <summary>
		/// Gets the image crop URL.
		/// </summary>
		/// <param name="width">The width of the output image.</param>
		/// <param name="height">The height of the output image.</param>
		/// <param name="quality">Quality percentage of the output image.</param>
		/// <param name="cacheBusterValue">Add a serialised date of the last edit of the item to ensure client cache refresh when updated.</param>
		/// <param name="furtherOptions">These are any query string parameters (formatted as query strings) that ImageProcessor supports. For example:
		/// <example><![CDATA[
		/// furtherOptions: "&bgcolor=fff"
		/// ]]></example></param>
		/// <param name="upScale">If the image should be upscaled to requested dimensions.</param>
		/// <returns>
		/// The image crop URL.
		/// </returns>
		public string GetCropUrl(int? width = null, int? height = null, int? quality = null, string cacheBusterValue = null, string furtherOptions = null, bool upScale = true)
		{
			var value = this.Value;
			if (value == null) return null;

			if (!width.HasValue || !height.HasValue)
			{
				var size = this.Editor?.Config?.Size;
				width = width ?? size?.Width;
				height = height ?? size?.Height;
			}

			return value.GetCropUrl(width, height, quality, cacheBusterValue, furtherOptions, upScale);
		}

		/// <summary>
		/// Gets the image crop URL, width and height.
		/// </summary>
		/// <param name="width">The width of the output image.</param>
		/// <param name="height">The height of the output image.</param>
		/// <param name="quality">Quality percentage of the output image.</param>
		/// <param name="cacheBusterValue">Add a serialised date of the last edit of the item to ensure client cache refresh when updated.</param>
		/// <param name="furtherOptions">These are any query string parameters (formatted as query strings) that ImageProcessor supports. For example:
		/// <example><![CDATA[
		/// furtherOptions: "&bgcolor=fff"
		/// ]]></example></param>
		/// <param name="upScale">If the image should be upscaled to requested dimensions.</param>
		/// <returns>
		/// The image crop URL, width and height.
		/// </returns>
		public (string Url, int? Width, int? Height) GetCrop(int? width = null, int? height = null, int? quality = null, string cacheBusterValue = null, string furtherOptions = null, bool upScale = true)
		{
			if (!width.HasValue || !height.HasValue)
			{
				var size = this.Editor?.Config?.Size;
				width = width ?? size?.Width;
				height = height ?? size?.Height;
			}

			string url = this.GetCropUrl(width, height, quality, cacheBusterValue, furtherOptions, upScale);

			return (url, width, height);
		}
	}
}