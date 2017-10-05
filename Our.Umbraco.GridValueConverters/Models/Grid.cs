using System.Collections.Generic;
using Newtonsoft.Json;

namespace Our.Umbraco.GridValueConverters.Models
{
	/// <summary>
	/// A model representing the grid layout.
	/// </summary>
	public partial class Grid
	{
		/// <summary>
		/// Gets or sets the name of the selected layout.
		/// </summary>
		/// <value>
		/// The name of the selected layout.
		/// </value>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the sections within the layout.
		/// </summary>
		/// <value>
		/// The sections within the layout.
		/// </value>
		[JsonProperty("sections")]
		public IList<Section> Sections { get; set; }
	}
}