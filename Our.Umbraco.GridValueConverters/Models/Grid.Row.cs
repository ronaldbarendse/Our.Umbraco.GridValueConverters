using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Our.Umbraco.GridValueConverters.Models
{
	public partial class Grid
	{
		/// <summary>
		/// The grid row.
		/// </summary>
		/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.ISettings" />
		public class Row : ISettings
		{
			/// <summary>
			/// Gets or sets the row name.
			/// </summary>
			/// <value>
			/// The row name.
			/// </value>
			[JsonProperty("name")]
			public string Name { get; set; }

			/// <summary>
			/// Gets or sets the row identifier.
			/// </summary>
			/// <value>
			/// The row identifier.
			/// </value>
			[JsonProperty("id")]
			public Guid Id { get; set; }

			/// <summary>
			/// Gets or sets the config values of this row.
			/// </summary>
			/// <value>
			/// The config values of this row.
			/// </value>
			[JsonProperty("config")]
			public IDictionary<string, object> Config { get; set; }

			/// <summary>
			/// Gets or sets the style values of this row.
			/// </summary>
			/// <value>
			/// The style values of this row.
			/// </value>
			[JsonProperty("styles")]
			public IDictionary<string, object> Styles { get; set; }

			/// <summary>
			/// Gets or sets the areas.
			/// </summary>
			/// <value>
			/// The areas.
			/// </value>
			[JsonProperty("areas")]
			public IList<Area> Areas { get; set; }

			#region UI Properties

			/// <summary>
			/// Gets or sets a value indicating whether this row has config or style options.
			/// </summary>
			/// <value>
			///   <c>true</c> if this row has config or style options; otherwise, <c>false</c>.
			/// </value>
			/// <remarks>
			/// This value is used within the UI view.
			/// </remarks>
			[EditorBrowsable(EditorBrowsableState.Never)]
			[JsonProperty("hasConfig")]
			public bool HasConfig { get; set; }

			/// <summary>
			/// Gets or sets a value indicating whether this <see cref="GridRow" /> is active.
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
			/// Gets or sets a value indicating whether this row has an active area.
			/// </summary>
			/// <value>
			///   <c>true</c> if this row has an active area; otherwise, <c>false</c>.
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