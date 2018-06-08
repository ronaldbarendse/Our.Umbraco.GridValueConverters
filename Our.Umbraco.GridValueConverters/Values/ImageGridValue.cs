using System;
using System.Drawing;
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
		#region Fields

		/// <summary>
		/// The content.
		/// </summary>
		private readonly Lazy<IPublishedContent> content;

		#endregion

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
		public IPublishedContent Content => this.content.Value;

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
			this.content = new Lazy<IPublishedContent>(() =>
			{
				return this.UmbracoContext.MediaCache.GetById(this.Id);
			});
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the crop data set (with the focal point and size).
		/// </summary>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns>
		/// The crop data set.
		/// </returns>
		public ImageCropDataSet GetCropDataSet(int? width = null, int? height = null)
		{
			Size? size = null;
			if (width.HasValue && height.HasValue)
			{
				size = new Size(width.Value, height.Value);
			}

			return this.GetCropDataSet(size);
		}

		/// <summary>
		/// Gets the crop data set (with the focal point and size).
		/// </summary>
		/// <param name="size">The size (used as crop dimentions).</param>
		/// <returns>
		/// The crop data set.
		/// </returns>
		public ImageCropDataSet GetCropDataSet(Size? size = null)
		{
			var src = this.Image;
			if (String.IsNullOrEmpty(src))
			{
				return null;
			}

			var cropDataSet = new ImageCropDataSet
			{
				Src = src,
				FocalPoint = this.FocalPoint
			};

			if (size.HasValue)
			{
				cropDataSet.Crops = new []
				{
					new ImageCropData
					{
						Width = size.Value.Width,
						Height = size.Value.Height
					}
				};
			}

			return cropDataSet;
		}

		#endregion
	}
}
