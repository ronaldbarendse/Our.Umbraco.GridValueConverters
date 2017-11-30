using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Our.Umbraco.GridValueConverters.Models;

namespace Our.Umbraco.GridValueConverters.Rendering
{
	/// <summary>
	/// Extension methods for <see cref="HtmlHelper" />.
	/// </summary>
	public static class HtmlHelperExtensions
	{
		/// <summary>
		/// The default grid framework.
		/// </summary>
		private const string defaultGridFramework = "bootstrap3";

		/// <summary>
		/// Gets the grid HTML.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="grid">The grid.</param>
		/// <param name="framework">The framework.</param>
		/// <returns></returns>
		public static MvcHtmlString GetGridHtml(this HtmlHelper htmlHelper, Grid grid, string framework = defaultGridFramework)
		{
			var view = "Grid/" + framework;

			return htmlHelper.Partial(view, grid);
		}

		/// <summary>
		/// Begins the tag.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="tagName">Name of the tag.</param>
		/// <param name="condition">If set to <c>true</c> renders the tag.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns>
		/// The tag.
		/// </returns>
		/// <exception cref="ArgumentNullException">htmlHelper</exception>
		public static IDisposable BeginTag(this HtmlHelper htmlHelper, string tagName, bool condition = true, object htmlAttributes = null)
		{
			if (htmlHelper == null) throw new ArgumentNullException(nameof(htmlHelper));

			return condition ? new DisposableTagBuilder(htmlHelper.ViewContext, tagName, htmlAttributes).Build() : null;
		}

		/// <summary>
		/// Begins the grid tag.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="gridSettings">The grid settings.</param>
		/// <param name="tagName">Name of the tag.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns>
		/// The grid tag.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// htmlHelper
		/// or
		/// gridSettings
		/// </exception>
		public static IDisposable BeginGridTag(this HtmlHelper htmlHelper, Grid.ISettings gridSettings, string tagName = "div", object htmlAttributes = null)
		{
			if (htmlHelper == null) throw new ArgumentNullException(nameof(htmlHelper));
			if (gridSettings == null) throw new ArgumentNullException(nameof(gridSettings));

			return new DisposableTagBuilder(htmlHelper.ViewContext, tagName, htmlAttributes, (tagBuilder) =>
			{
				// Add configuration/attributes
				if (gridSettings.Config?.Count > 0)
				{
					foreach (var item in gridSettings.Config)
					{
						var value = item.Value?.ToString();
						if (String.Equals(item.Key, "class", StringComparison.OrdinalIgnoreCase))
						{
							// Combine classes
							tagBuilder.AddCssClass(value);
						}
						else
						{
							// Add/replace other attributes
							tagBuilder.MergeAttribute(item.Key, value, true);
						}
					}
				}

				// Add styles
				if (gridSettings.Styles?.Count > 0)
				{
					var styles = gridSettings.Styles.Select(s => s.Key + ": " + s.Value?.ToString() + ";");

					// Add/replace style attribute
					tagBuilder.MergeAttribute("style", String.Join(" ", styles), true);
				}
			}).Build();
		}

		/// <summary>
		/// Renders the grid control.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="gridControl">The grid control.</param>
		/// <param name="partialViewName">The name of the partial view to render. If <c>null</c>, uses the editors <see cref="Grid.IEditor.Render" />, <see cref="Grid.IEditor.View" /> or <see cref="Grid.IEditor.Alias" />.</param>
		/// <returns>
		/// The rendered grid control.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// htmlHelper
		/// or
		/// gridControl
		/// </exception>
		public static MvcHtmlString GridControl(this HtmlHelper htmlHelper, Grid.IControl gridControl, string partialViewName = null)
		{
			if (htmlHelper == null) throw new ArgumentNullException(nameof(htmlHelper));
			if (gridControl == null) throw new ArgumentNullException(nameof(gridControl));

			string view = htmlHelper.GetPartialViewName(gridControl, partialViewName);

			return htmlHelper.Partial(view, gridControl);
		}

		/// <summary>
		/// Renders the grid control.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="gridControl">The grid control.</param>
		/// <param name="partialViewName">The name of the partial view to render. If <c>null</c>, uses the editors <see cref="Grid.IEditor.Render" />, <see cref="Grid.IEditor.View" /> or <see cref="Grid.IEditor.Alias" />.</param>
		/// <exception cref="ArgumentNullException">
		/// htmlHelper
		/// or
		/// gridControl
		/// </exception>
		public static void RenderGridControl(this HtmlHelper htmlHelper, Grid.IControl gridControl, string partialViewName = null)
		{
			if (htmlHelper == null) throw new ArgumentNullException(nameof(htmlHelper));
			if (gridControl == null) throw new ArgumentNullException(nameof(gridControl));

			string view = htmlHelper.GetPartialViewName(gridControl, partialViewName);

			htmlHelper.RenderPartial(view, gridControl);
		}

		/// <summary>
		/// Gets the partial view name for the <see cref="Grid.IControl" />.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="gridControl">The grid control.</param>
		/// <param name="partialViewName">The name of the partial view to render. If <c>null</c>, uses the editors <see cref="Grid.IEditor.Render" />, <see cref="Grid.IEditor.View" /> or <see cref="Grid.IEditor.Alias" />.</param>
		/// <returns>
		/// The partial view name.
		/// </returns>
		private static string GetPartialViewName(this HtmlHelper htmlHelper, Grid.IControl gridControl, string partialViewName)
		{
			string view = partialViewName ?? gridControl.Editor.Render ?? gridControl.Editor.View.ToLowerInvariant().Replace(".html", null);
			if (!view.Contains("/"))
			{
				view = "Grid/Editors/" + view;
			}

			if (ViewEngines.Engines.FindPartialView(htmlHelper.ViewContext, view)?.View == null)
			{
				view = "Grid/Editors/" + gridControl.Editor.Alias;
			}

			return view;
		}
	}
}