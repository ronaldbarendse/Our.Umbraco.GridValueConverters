using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Our.Umbraco.GridValueConverters.JsonConverters;

namespace Our.Umbraco.GridValueConverters.Models
{
	public partial class Grid
	{
		/// <summary>
		/// The grid area/cell.
		/// </summary>
		/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.ISettings" />
		public class Area : ISettings
		{
			/// <summary>
			/// Gets or sets the amount of columns within the grid area.
			/// </summary>
			/// <value>
			/// The amount of columns within the grid area.
			/// </value>
			[JsonProperty("grid")]
			public int Grid { get; set; }

			/// <summary>
			/// Gets or sets the config values of this area.
			/// </summary>
			/// <value>
			/// The config values of this area.
			/// </value>
			[JsonProperty("config")]
			public IDictionary<string, object> Config { get; set; }

			/// <summary>
			/// Gets or sets the style values of this area.
			/// </summary>
			/// <value>
			/// The style values of this area.
			/// </value>
			[JsonProperty("styles")]
			public IDictionary<string, object> Styles { get; set; }

			/// <summary>
			/// Gets or sets the controls.
			/// </summary>
			/// <value>
			/// The controls.
			/// </value>
			[JsonProperty("controls", ItemConverterType = typeof(GridControlConverter))]
			public IList<IControl> Controls { get; set; }

			#region UI Properties

			/// <summary>
			/// Gets or sets a value indicating whether all controls are allowed.
			/// </summary>
			/// <value>
			///   <c>true</c> if all controls are allowed; otherwise, <c>false</c>.
			/// </value>
			/// <remarks>
			/// This value is used within the UI view.
			/// </remarks>
			[EditorBrowsable(EditorBrowsableState.Never)]
			[JsonProperty("allowAll")]
			public bool AllowAll { get; set; }

			/// <summary>
			/// Gets or sets the allowed control aliasses.
			/// </summary>
			/// <value>
			/// The allowed control aliasses.
			/// </value>
			/// <remarks>
			/// This value is used within the UI view.
			/// </remarks>
			[EditorBrowsable(EditorBrowsableState.Never)]
			[JsonProperty("allowed")]
			public IList<string> Allowed { get; set; }

			/// <summary>
			/// Gets or sets a value indicating whether dropping a control to this area is not allowed.
			/// </summary>
			/// <value>
			///   <c>true</c> if dropping a control to this area is not allowed; otherwise, <c>false</c>.
			/// </value>
			/// <remarks>
			/// This value is used within the UI view.
			/// </remarks>
			[EditorBrowsable(EditorBrowsableState.Never)]
			[JsonProperty("dropNotAllowed")]
			public bool DropNotAllowed { get; set; }

			/// <summary>
			/// Gets or sets a value indicating whether a control is dropped to an empty area.
			/// </summary>
			/// <value>
			///   <c>true</c> if a control is dropped to an empty area; otherwise, <c>false</c>.
			/// </value>
			/// <remarks>
			/// This value is used within the UI view.
			/// </remarks>
			[EditorBrowsable(EditorBrowsableState.Never)]
			[JsonProperty("dropOnEmpty")]
			public bool DropOnEmpty { get; set; }

			/// <summary>
			/// Gets or sets a value indicating whether this area has config or style options.
			/// </summary>
			/// <value>
			///   <c>true</c> if this area has config or style options; otherwise, <c>false</c>.
			/// </value>
			/// <remarks>
			/// This value is used within the UI view.
			/// </remarks>
			[EditorBrowsable(EditorBrowsableState.Never)]
			[JsonProperty("hasConfig")]
			public bool HasConfig { get; set; }

			/// <summary>
			/// Gets or sets a value indicating whether this <see cref="GridArea" /> is active.
			/// </summary>
			/// <value>
			///   <c>true</c> if active; otherwise, <c>false</c>.
			/// </value>
			/// <remarks>
			/// This value is used within the UI view.
			/// </remarks>
			[EditorBrowsable(EditorBrowsableState.Never)]
			[JsonProperty("active")]
			public bool Active { get; set; }

			/// <summary>
			/// Gets or sets a value indicating whether this area has an active control.
			/// </summary>
			/// <value>
			///   <c>true</c> if this area has an active control; otherwise, <c>false</c>.
			/// </value>
			/// <remarks>
			/// This value is used within the UI view.
			/// </remarks>
			[EditorBrowsable(EditorBrowsableState.Never)]
			[JsonProperty("hasActiveChild")]
			public bool HasActiveChild { get; set; }

			#endregion
		}
	}
}