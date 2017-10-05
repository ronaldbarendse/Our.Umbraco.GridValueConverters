namespace Our.Umbraco.GridValueConverters.Models
{
	public partial class Grid
	{
		/// <summary>
		/// Represents a grid editor.
		/// </summary>
		public interface IEditor
		{
			/// <summary>
			/// Gets the editor name.
			/// </summary>
			/// <value>
			/// The editor name.
			/// </value>
			string Name { get; }

			/// <summary>
			/// Gets the editor alias.
			/// </summary>
			/// <value>
			/// The editor alias.
			/// </value>
			string Alias { get; }

			/// <summary>
			/// Gets the editor view.
			/// </summary>
			/// <value>
			/// The editor view.
			/// </value>
			string View { get; }

			/// <summary>
			/// Gets the editor render (partial) view.
			/// </summary>
			/// <value>
			/// The editor render (partial) view.
			/// </value>
			string Render { get; }

			/// <summary>
			/// Gets the editor configuration.
			/// </summary>
			/// <value>
			/// The editor configuration.
			/// </value>
			object Config { get; }

			#region UI Properties

			/// <summary>
			/// Gets or sets the editor icon.
			/// </summary>
			/// <value>
			/// The editor icon.
			/// </value>
			/// <remarks>
			/// This value is used within the UI view.
			/// </remarks>
			string Icon { get; }

			#endregion
		}

		/// <summary>
		/// Represents a grid editor with a typed config value.
		/// </summary>
		/// <typeparam name="TConfig">The type of the config value.</typeparam>
		public interface IEditor<out TConfig> : IEditor
		{
			/// <summary>
			/// Gets the editor configuration.
			/// </summary>
			/// <value>
			/// The editor configuration.
			/// </value>
			new TConfig Config { get; }
		}
	}
}