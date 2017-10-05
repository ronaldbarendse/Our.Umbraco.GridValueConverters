using Newtonsoft.Json.Linq;
using Our.Umbraco.GridValueConverters.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Our.Umbraco.GridValueConverters
{
	/// <summary>
	/// Value converter for grid value.
	/// </summary>
	/// <remarks>
	/// The object is cached at the content cache level, because it could store references to other content (eg. parsed HTML with links).
	/// </remarks>
	/// <seealso cref="Umbraco.Core.PropertyEditors.ValueConverters.GridValueConverter" />
	[PropertyValueType(typeof(Grid))]
	[PropertyValueCache(PropertyCacheValue.Source, PropertyCacheLevel.Content)]
	[PropertyValueCache(PropertyCacheValue.Object, PropertyCacheLevel.ContentCache)]
	[PropertyValueCache(PropertyCacheValue.XPath, PropertyCacheLevel.Content)]
	public class GridValueConverter : global::Umbraco.Core.PropertyEditors.ValueConverters.GridValueConverter
	{
		/// <summary>
		/// Converts the source to object.
		/// </summary>
		/// <param name="propertyType">Type of the property.</param>
		/// <param name="source">The source.</param>
		/// <param name="preview">If set to <c>true</c> indicates the content is previewed.</param>
		/// <returns>
		/// The source converted to the object.
		/// </returns>
		public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
		{
			if (source is JObject obj)
			{
				// Core logic happens within the attributed JsonConverters
				return obj.ToObject<Grid>();
			}

			return null;
		}
	}
}