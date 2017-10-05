using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Our.Umbraco.GridValueConverters.Models;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.ObjectResolution;

namespace Our.Umbraco.GridValueConverters
{
	/// <summary>
	/// Resolves the <see cref="Grid.IControl" /> objects.
	/// </summary>
	/// <seealso cref="Umbraco.Core.ObjectResolution.ManyObjectsResolverBase{Our.Umbraco.GridValueConverters.GridControlsResolver, Our.Umbraco.GridValueConverters.Models.Grid.IControl}" />
	public sealed class GridControlsResolver : ManyObjectsResolverBase<GridControlsResolver, Grid.IControl>
	{
		/// <summary>
		/// Gets the controls.
		/// </summary>
		/// <value>
		/// The controls.
		/// </value>
		public IEnumerable<Grid.IControl> Controls => this.Values;

		/// <summary>
		/// Initializes a new instance of the <see cref="GridControlsResolver" /> class.
		/// </summary>
		/// <param name="serviceProvider">The service provider.</param>
		/// <param name="logger">The logger.</param>
		/// <param name="controls">The controls.</param>
		internal GridControlsResolver(IServiceProvider serviceProvider, ILogger logger, IEnumerable<Type> controls)
			: base(serviceProvider, logger, controls?.Where(t => !t.IsGenericType))
		{ }

		/// <summary>
		/// Resolves the control type.
		/// </summary>
		/// <param name="editor">The editor.</param>
		/// <returns>
		/// The type of the resolved control or <c>null</c>.
		/// </returns>
		/// <exception cref="ArgumentNullException">editor</exception>
		/// <exception cref="InvalidOperationException"></exception>
		internal Type ResolveControlType(Grid.IEditor editor)
		{
			if (editor == null) throw new ArgumentNullException(nameof(editor));

			// Resolve to specific control type
			var controls = this.Controls.Where(c => c.IsControlEditor(editor)).ToList();
			if (controls.Count == 1)
			{
				var controlType = controls[0].GetType();

				this.Logger.Debug<GridControlsResolver>($"Resolved control type for grid editor alias '{editor.Alias}' to {controlType}.");

				return controlType;
			}
			else if (controls.Count > 1) throw new InvalidOperationException($"Type '{controls[1].GetType().FullName}' cannot be a Grid.IControl for grid editor with alias '{editor.Alias}', because type '{controls[0].GetType().FullName}' has already been detected as a control for that editor, and only one control can exist for a grid editor.");

			return null;
		}

		/// <summary>
		/// Resolves the editor type.
		/// </summary>
		/// <param name="editor">The editor.</param>
		/// <returns>
		/// The type of the resolved editor or <c>null</c>.
		/// </returns>
		/// <exception cref="ArgumentNullException">editor</exception>
		internal Type ResolveEditorType(Grid.IEditor editor)
		{
			if (editor == null) throw new ArgumentNullException(nameof(editor));

			var controlType = this.ResolveControlType(editor);
			if (controlType != null)
			{
				// Get editor type from declared property (because the property can be hidden using the new modifier and we don't want an AmbiguousMatchException)
				foreach (var baseType in controlType.GetBaseTypes(true))
				{
					var editorType = baseType.GetProperty(nameof(Grid.IControl.Editor), BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)?.PropertyType;
					if (editorType != null)
					{
						if (editorType.IsInterface)
						{
							// Construct type from interface
							if (editorType.TryGetGenericArguments(typeof(Grid.IEditor<>), out Type[] genericArguments))
							{
								editorType = typeof(Grid.Editor<>).MakeGenericType(genericArguments[0]);
							}
							else
							{
								editorType = typeof(Grid.Editor);
							}
						}

						this.Logger.Debug<GridControlsResolver>($"Resolved editor type for grid editor alias '{editor.Alias}' to {editorType} (using control type {controlType}).");

						return editorType;
					}
				}
			}

			return null;
		}
	}
}
