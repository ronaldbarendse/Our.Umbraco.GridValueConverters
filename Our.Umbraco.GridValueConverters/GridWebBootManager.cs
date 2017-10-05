using Our.Umbraco.GridValueConverters.Models;
using Umbraco.Core;
using Umbraco.Web;

namespace Our.Umbraco.GridValueConverters
{
	/// <summary>
	/// A bootstrapper for the Umbraco application which initializes the grid controls resolver.
	/// </summary>
	/// <remarks>
	/// This custom bootstrapper can be used instead of the <see cref="RegisterGridControlsResolverEventHander"/>.
	/// </remarks>
	/// <seealso cref="Umbraco.Web.WebBootManager" />
	public class GridWebBootManager : WebBootManager
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GridWebBootManager" /> class.
		/// </summary>
		/// <param name="umbracoApplication">The umbraco application.</param>
		public GridWebBootManager(UmbracoApplicationBase umbracoApplication)
			: base(umbracoApplication)
		{ }

		/// <summary>
		/// Create the resolvers
		/// </summary>
		protected override void InitializeResolvers()
		{
			base.InitializeResolvers();

			GridControlsResolver.Current = new GridControlsResolver(this.ServiceProvider, this.ProfilingLogger.Logger, this.PluginManager.ResolveTypes<Grid.IControl>());
		}
	}
}
