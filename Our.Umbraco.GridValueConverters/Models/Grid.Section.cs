using System.Collections.Generic;
using Newtonsoft.Json;

namespace Our.Umbraco.GridValueConverters.Models
{
	public partial class Grid
	{
		/// <summary>
		/// The grid section.
		/// </summary>
		public class Section
		{
			/// <summary>
			/// Gets or sets the amount of columns within the grid section.
			/// </summary>
			/// <value>
			/// The amount of columns within the grid section.
			/// </value>
			[JsonProperty("grid")]
			public int Grid { get; set; }

			/// <summary>
			/// Gets or sets the rows.
			/// </summary>
			/// <value>
			/// The rows.
			/// </value>
			[JsonProperty("rows")]
			public IList<Row> Rows { get; set; }
		}
	}
}