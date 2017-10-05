using System;
using System.Drawing;
using Newtonsoft.Json;
using Our.Umbraco.GridValueConverters.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Our.Umbraco.GridValueConverters.Default.Controls
{
	/// <summary>
	/// The media grid control.
	/// </summary>
	/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.Control{Our.Umbraco.GridValueConverters.Default.Controls.MediaGridValue, Our.Umbraco.GridValueConverters.Default.Controls.MediaGridConfig}" />
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
			if (String.IsNullOrEmpty(value?.Image))
			{
				return null;
			}

			var src = value.Image;
			var cropDataSet = new ImageCropDataSet()
			{
				Src = src,
				FocalPoint = value.FocalPoint
			};
			if (!width.HasValue || !height.HasValue)
			{
				var size = this.Editor?.Config?.Size;
				width = width ?? size?.Width;
				height = height ?? size?.Height;
			}

			return src.GetCropUrl(cropDataSet, width, height, quality: quality, cacheBusterValue: cacheBusterValue, furtherOptions: furtherOptions, upScale: upScale);
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

			string url = GetCropUrl(width, height, quality, cacheBusterValue, furtherOptions, upScale);

			return (url, width, height);
		}
	}

	/// <summary>
	/// The media grid value.
	/// </summary>
	public class MediaGridValue
	{
		/// <summary>
		/// Gets or sets the focal point.
		/// </summary>
		/// <value>
		/// The focal point.
		/// </value>
		[JsonProperty("focalPoint")]
		public ImageCropFocalPoint FocalPoint { get; set; }

		/// <summary>
		/// Gets or sets the media identifier.
		/// </summary>
		/// <value>
		/// The media identifier.
		/// </value>
		[JsonProperty("id")]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the media image URL.
		/// </summary>
		/// <value>
		/// The media image URL.
		/// </value>
		[JsonProperty("image")]
		public string Image { get; set; }

		/// <summary>
		/// Gets or sets the media alternate text.
		/// </summary>
		/// <value>
		/// The media alternate text.
		/// </value>
		[JsonProperty("altText")]
		public string AlternateText { get; set; }

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
		public Size? Size { get; set; }
	}
}