using System;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Our.Umbraco.GridValueConverters.JsonConverters;
using Umbraco.Core;

namespace Our.Umbraco.GridValueConverters.Models
{
	public partial class Grid
	{
		/// <summary>
		/// The grid control.
		/// </summary>
		/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.IControl" />
		public class Control : IControl
		{
			/// <summary>
			/// Gets or sets the control value.
			/// </summary>
			/// <value>
			/// The control value.
			/// </value>
			[JsonConverter(typeof(HtmlGridValueConverter))]
			[JsonProperty("value")]
			public object Value { get; set; }

			/// <summary>
			/// Gets or sets the grid editor.
			/// </summary>
			/// <value>
			/// The grid editor.
			/// </value>
			[JsonConverter(typeof(GridEditorConverter))]
			[JsonProperty("editor")]
			public IEditor Editor { get; set; }

			#region UI Properties

			/// <summary>
			/// Gets or sets a value indicating whether this <see cref="GridControl" /> is active.
			/// </summary>
			/// <value>
			///   <c>true</c> if active; otherwise, <c>false</c>.
			/// </value>
			/// <remarks>
			/// This value is used within the UI view.
			/// </remarks>
			[JsonProperty("active")]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public bool Active { get; set; }

			#endregion

			#region Methods

			/// <summary>
			/// Determines whether the specified <see cref="Grid.IEditor" /> is for this control.
			/// </summary>
			/// <param name="editor">The editor.</param>
			/// <returns>
			///   <c>true</c> if the editor is for this control; otherwise, <c>false</c>.
			/// </returns>
			/// <exception cref="ArgumentNullException">editor</exception>
			/// <remarks>
			/// This method can be overridden to allow for custom matching logic.
			/// </remarks>
			public virtual bool IsControlEditor(IEditor editor)
			{
				if (editor == null) throw new ArgumentNullException(nameof(editor));

				GridControlAttribute gridControlAttribute = this.GetType().GetCustomAttribute<GridControlAttribute>(false);
				if (gridControlAttribute != null)
				{
					return gridControlAttribute.IsControlEditor(editor);
				}

				return false;
			}

			#endregion

			#region Backwards Compatibility

			/// <summary>
			/// Gets the value.
			/// </summary>
			/// <value>
			/// The value.
			/// </value>
			/// <remarks>
			/// The grid control value can be accessed in views that still use dynamics (eg. plugin views). To keep this from breaking, we expose a lowercase <see cref="value" /> property that returns a <see cref="JToken" />.
			/// </remarks>
			[EditorBrowsable(EditorBrowsableState.Never)]
			[JsonIgnore]
#pragma warning disable IDE1006 // Naming Styles
			public JToken value => JToken.FromObject(this.Value);
#pragma warning restore IDE1006 // Naming Styles

			/// <summary>
			/// Gets the editor.
			/// </summary>
			/// <value>
			/// The editor.
			/// </value>
			/// <remarks>
			/// The grid editor can be accessed in views that still use dynamics (eg. plugin views). To keep this from breaking, we expose a lowercase <see cref="editor" /> property that returns a <see cref="JToken" />.
			/// </remarks>
			[EditorBrowsable(EditorBrowsableState.Never)]
			[JsonIgnore]
#pragma warning disable IDE1006 // Naming Styles
			public JToken editor => JToken.FromObject(this.Editor);
#pragma warning restore IDE1006 // Naming Styles

			#endregion
		}

		/// <summary>
		/// The grid control with a typed value.
		/// </summary>
		/// <typeparam name="TValue">The type of the control value.</typeparam>
		/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.IControl" />
		public class Control<TValue> : Control, IControl<TValue>
		{
			/// <summary>
			/// Gets or sets the control value.
			/// </summary>
			/// <value>
			/// The control value.
			/// </value>
			[JsonConverter(typeof(HtmlGridValueConverter))]
			[JsonProperty("value")]
			public virtual new TValue Value
			{
				get
				{
					return (TValue)base.Value;
				}
				set
				{
					base.Value = value;
				}
			}
		}

		/// <summary>
		/// The grid control with a typed value and editor config value.
		/// </summary>
		/// <typeparam name="TValue">The type of the control value.</typeparam>
		/// <typeparam name="TEditorConfig">The type of the editor config value.</typeparam>
		/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.IControl" />
		public class Control<TValue, TEditorConfig> : Control<TValue>, IControl<TValue, TEditorConfig>
		{
			/// <summary>
			/// Gets or sets the grid editor.
			/// </summary>
			/// <value>
			/// The grid editor.
			/// </value>
			[JsonConverter(typeof(GridEditorConverter))]
			[JsonProperty("editor")]
			public new IEditor<TEditorConfig> Editor
			{
				get
				{
					return (IEditor<TEditorConfig>)base.Editor;
				}
				set
				{
					base.Editor = value;
				}
			}
		}
	}
}