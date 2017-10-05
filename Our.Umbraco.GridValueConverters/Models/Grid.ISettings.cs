using System.Collections.Generic;

namespace Our.Umbraco.GridValueConverters.Models
{
	public partial class Grid
	{
		/// <summary>
		/// Represents a grid item with settings (config and styles).
		/// </summary>
		public interface ISettings
		{
			/// <summary>
			/// Gets the config values of this grid item.
			/// </summary>
			/// <value>
			/// The config values of this grid item.
			/// </value>
			IDictionary<string, object> Config { get; }

			/// <summary>
			/// Gets the style values of this grid item.
			/// </summary>
			/// <value>
			/// The style values of this grid item.
			/// </value>
			IDictionary<string, object> Styles { get; }
		}
	}
}