using System.Drawing;
using Our.Umbraco.GridValueConverters.Default.Configs;
using Our.Umbraco.GridValueConverters.Default.Values;
using Our.Umbraco.GridValueConverters.Models;
using Umbraco.Web.Models;

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
		/// Gets the crop data set (with the focal point and size).
		/// </summary>
		/// <returns>
		/// The crop data set.
		/// </returns>
		public ImageCropDataSet GetCropDataSet()
		{
			Size? size = this.Editor?.Config?.Size;

			return this.Value?.GetCropDataSet(size);
		}
	}
}