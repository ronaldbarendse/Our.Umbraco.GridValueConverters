using umbraco.interfaces;

namespace Our.Umbraco.GridValueConverters.Models
{
	public partial class Grid
	{
		/// <summary>
		/// Represents a grid control.
		/// </summary>
		/// <seealso cref="umbraco.interfaces.IDiscoverable" />
		public interface IControl : IDiscoverable
		{
			/// <summary>
			/// Gets the control value.
			/// </summary>
			/// <value>
			/// The control value.
			/// </value>
			object Value { get; }

			/// <summary>
			/// Gets the editor.
			/// </summary>
			/// <value>
			/// The editor.
			/// </value>
			IEditor Editor { get; }

			#region UI Properties

			/// <summary>
			/// Gets a value indicating whether this <see cref="IControl" /> is active.
			/// </summary>
			/// <value>
			///   <c>true</c> if active; otherwise, <c>false</c>.
			/// </value>
			bool Active { get; }

			#endregion

			#region Methods

			/// <summary>
			/// Determines whether the specified <see cref="Grid.IEditor" /> is for this control.
			/// </summary>
			/// <param name="editor">The editor.</param>
			/// <returns>
			///   <c>true</c> if the editor is for this control; otherwise, <c>false</c>.
			/// </returns>
			bool IsControlEditor(IEditor editor);

			#endregion
		}

		/// <summary>
		/// Represents a grid control with a typed value.
		/// </summary>
		/// <typeparam name="TValue">The type of the control value.</typeparam>
		/// <seealso cref="umbraco.interfaces.IDiscoverable" />
		public interface IControl<out TValue> : IControl
		{
			/// <summary>
			/// Gets the control value.
			/// </summary>
			/// <value>
			/// The control value.
			/// </value>
			new TValue Value { get; }
		}

		/// <summary>
		/// Represents a grid control with a typed value and editor config value.
		/// </summary>
		/// <typeparam name="TValue">The type of the control value.</typeparam>
		/// <typeparam name="TEditorConfig">The type of the editor config value.</typeparam>
		/// <seealso cref="umbraco.interfaces.IDiscoverable" />
		public interface IControl<out TValue, out TEditorConfig> : IControl<TValue>
		{
			/// <summary>
			/// Gets the editor.
			/// </summary>
			/// <value>
			/// The editor.
			/// </value>
			new IEditor<TEditorConfig> Editor { get; }
		}
	}
}