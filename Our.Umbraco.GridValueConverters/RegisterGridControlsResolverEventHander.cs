using System;
using Our.Umbraco.GridValueConverters.Models;
using Umbraco.Core;

namespace Our.Umbraco.GridValueConverters
{
	/// <summary>
	/// Event handler to register the grid controls resolver.
	/// </summary>
	/// <remarks>
	/// I couldn't find another way to register the resolver, other than having all projects use a custom IBootManager (<see cref="GridWebBootManager"/>) or duplicating the <see cref="IServiceProvider"/>. Probably because the <see cref="ManyObjectsResolverBase"/> is part of Umbraco core and not exposed for plugins?
	/// - https://our.umbraco.org/documentation/reference/plugins/creating-resolvers
	/// - https://our.umbraco.org/documentation/reference/plugins/initializing-resolvers
	/// </remarks>
	/// <seealso cref="Umbraco.Core.ApplicationEventHandler" />
	public class RegisterGridControlsResolverEventHander : ApplicationEventHandler
	{
		/// <summary>
		/// Overridable method to execute when All resolvers have been initialized but resolution is not frozen so they can be modified in this method
		/// </summary>
		/// <param name="umbracoApplication"></param>
		/// <param name="applicationContext"></param>
		protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			if (!GridControlsResolver.HasCurrent)
			{
				// Resolver not already set in the boot manager
				GridControlsResolver.Current = new GridControlsResolver(new ActivatorServiceProvider(), applicationContext.ProfilingLogger.Logger, PluginManager.Current.ResolveTypes<Grid.IControl>());
			}
		}

		/// <summary>
		/// Copy of internal <see cref="Umbraco.Core.ActivatorServiceProvider" />.
		/// </summary>
		/// <seealso cref="System.IServiceProvider" />
		internal class ActivatorServiceProvider : IServiceProvider
		{
			/// <summary>
			/// Gets the service object of the specified type.
			/// </summary>
			/// <param name="serviceType">An object that specifies the type of service object to get.</param>
			/// <returns>
			/// A service object of type <paramref name="serviceType" />.-or- null if there is no service object of type <paramref name="serviceType" />.
			/// </returns>
			public object GetService(Type serviceType)
			{
				return Activator.CreateInstance(serviceType);
			}
		}
	}
}
