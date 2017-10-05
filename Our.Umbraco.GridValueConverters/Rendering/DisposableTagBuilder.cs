using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Our.Umbraco.GridValueConverters.Rendering
{
	/// <summary>
	/// Represents a class for creating HTML elements with seperate start and end tags.
	/// </summary>
	/// <remarks>
	/// This class should be used with an <see cref="HtmlHelper"/> extension method that creates a new instance, builds the tag and returns an <see cref="IDisposable" />.
	/// 
	/// The builder action or the subclassed <see cref="Build(TagBuilder)"/> method can provide additional logic to set attributes.
	/// </remarks>
	/// <seealso cref="System.IDisposable" />
	/// <example>
	/// public static IDisposable BeginDiv(this HtmlHelper htmlHelper)
	/// {
	///		return new DisposableTagBuilder(htmlHelper.ViewContext, "div").Build();
	/// }
	/// 
	/// @using (Html.BeginDiv()) { }
	/// </example>
	public class DisposableTagBuilder : IDisposable
	{
		#region Fields

		/// <summary>
		/// The tag builder.
		/// </summary>
		private readonly TagBuilder tagBuilder;

		/// <summary>
		/// The builder action.
		/// </summary>
		private readonly Action<TagBuilder> builder;

		/// <summary>
		/// Indicates whether this instance is disposed.
		/// </summary>
		private bool disposed;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the view context.
		/// </summary>
		/// <value>
		/// The view context.
		/// </value>
		public ViewContext ViewContext { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DisposableTagBuilder" /> class.
		/// </summary>
		/// <param name="viewContext">The view context.</param>
		/// <param name="tagName">Name of the tag.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <param name="builder">The builder action, invoked when the <see cref="Build" /> method is called.</param>
		/// <exception cref="ArgumentNullException">viewContext</exception>
		public DisposableTagBuilder(ViewContext viewContext, string tagName, object htmlAttributes = null, Action<TagBuilder> builder = null)
		{
			this.ViewContext = viewContext ?? throw new ArgumentNullException(nameof(viewContext));
			this.tagBuilder = new TagBuilder(tagName);
			this.tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
			this.builder = builder;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Builds and writes the start tag.
		/// </summary>
		/// <returns>
		/// An <see cref="IDisposable" /> that writes the end tag when disposed.
		/// </returns>
		/// <exception cref="ObjectDisposedException"></exception>
		public IDisposable Build()
		{
			if (this.disposed) throw new ObjectDisposedException(this.GetType().FullName);

			this.Build(this.tagBuilder);
			this.ViewContext.Writer.Write(this.tagBuilder.ToString(TagRenderMode.StartTag));

			return this;
		}

		/// <summary>
		/// Builds the specified tag builder (before writing the start tag).
		/// </summary>
		/// <param name="tagBuilder">The tag builder.</param>
		protected virtual void Build(TagBuilder tagBuilder)
		{
			this.builder?.Invoke(tagBuilder);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.ViewContext.Writer.Write(this.tagBuilder.ToString(TagRenderMode.EndTag));
				this.ViewContext.Writer.Flush();
			}
		}

		#endregion
	}
}