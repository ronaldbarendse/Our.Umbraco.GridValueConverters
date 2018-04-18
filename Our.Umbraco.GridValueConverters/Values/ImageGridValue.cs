using System;
using Newtonsoft.Json;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Our.Umbraco.GridValueConverters.Values
{
	/// <summary>
	/// The image grid value.
	/// </summary>
	public class ImageGridValue
	{
		#region Properties

		/// <summary>
		/// Gets the Umbraco context.
		/// </summary>
		/// <value>
		/// The Umbraco context.
		/// </value>
		protected UmbracoContext UmbracoContext { get; }

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
		/// Gets the image as <see cref="IPublishedContent"/> (from the media cache).
		/// </summary>
		/// <value>
		/// The image content.
		/// </value>
		[JsonIgnore]
		public IPublishedContent Content
		{
			get
			{
				return this.UmbracoContext.MediaCache.GetById(this.Id);
			}
		}

		/// <summary>
		/// Gets or sets the media alternate text.
		/// </summary>
		/// <value>
		/// The media alternate text.
		/// </value>
		[JsonProperty("altText")]
		public string AlternateText { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageGridValue" /> class.
		/// </summary>
		public ImageGridValue()
			: this(UmbracoContext.Current)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageGridValue" /> class.
		/// </summary>
		/// <param name="umbracoContext">The Umbraco context.</param>
		/// <exception cref="ArgumentNullException">umbracoContext</exception>
		public ImageGridValue(UmbracoContext umbracoContext)
		{
			this.UmbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
		}

		#endregion

		#region Methods

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
			var src = this.Image;
			if (String.IsNullOrEmpty(src))
			{
				return null;
			}

			var cropDataSet = new ImageCropDataSet()
			{
				Src = src,
				FocalPoint = this.FocalPoint
			};

			return src.GetCropUrl(cropDataSet, width, height, quality: quality, cacheBusterValue: cacheBusterValue, furtherOptions: furtherOptions, upScale: upScale);
		}

		#endregion
	}
}
