using System.Linq;
using Our.Umbraco.GridValueConverters.Models;

namespace Our.Umbraco.GridValueConverters.Rendering
{
	/// <summary>
	/// Provides extension methods for the grid.
	/// </summary>
	public static class GridExtensions
	{
		/// <summary>
		/// Determines whether the grid contains any valid sections.
		/// </summary>
		/// <param name="grid">The grid.</param>
		/// <returns>
		///   <c>true</c> if the grid contains any valid sections; otherwise, <c>false</c>.
		/// </returns>
		public static bool Any(this Grid grid)
		{
			return grid.Sections != null && grid.Sections.Any(s => s.Any());
		}

		/// <summary>
		/// Determines whether the section contains any valid rows.
		/// </summary>
		/// <param name="section">The section.</param>
		/// <returns>
		///   <c>true</c> if the section contains any valid rows; otherwise, <c>false</c>.
		/// </returns>
		public static bool Any(this Grid.Section section)
		{
			return section.Rows != null && section.Rows.Any(r => r.Any());
		}

		/// <summary>
		/// Determines whether the row contains any valid areas.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <returns>
		///   <c>true</c> if the row contains any valid areas; otherwise, <c>false</c>.
		/// </returns>
		public static bool Any(this Grid.Row row)
		{
			return row.Areas != null && row.Areas.Any(a => a.Any());
		}

		/// <summary>
		/// Determines whether the area contains any controls.
		/// </summary>
		/// <param name="area">The area.</param>
		/// <returns>
		///   <c>true</c> if the area contains any controls; otherwise, <c>false</c>.
		/// </returns>
		public static bool Any(this Grid.Area area)
		{
			return area.Controls != null && area.Controls.Any();
		}
	}
}
