using System.ComponentModel;
using Newtonsoft.Json;
using Umbraco.Core;

namespace Our.Umbraco.GridValueConverters.Models
{
	public partial class Grid
	{
		/// <summary>
		/// The grid editor.
		/// </summary>
		/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.IEditor" />
		public class Editor : IEditor
		{
			/// <summary>
			/// Gets or sets the editor name.
			/// </summary>
			/// <value>
			/// The editor name.
			/// </value>
			[JsonProperty("name")]
			public string Name { get; set; }

			/// <summary>
			/// Gets or sets the editor alias.
			/// </summary>
			/// <value>
			/// The editor alias.
			/// </value>
			[JsonProperty("alias")]
			public string Alias { get; set; }

			/// <summary>
			/// Gets or sets the editor view.
			/// </summary>
			/// <value>
			/// The editor view.
			/// </value>
			[JsonProperty("view")]
			public string View { get; set; }

			/// <summary>
			/// Gets or sets the editor render (partial) view.
			/// </summary>
			/// <value>
			/// The editor render (partial) view.
			/// </value>
			[JsonProperty("render")]
			public string Render { get; set; }

			/// <summary>
			/// Gets or sets the editor configuration.
			/// </summary>
			/// <value>
			/// The editor configuration.
			/// </value>
			[JsonProperty("config")]
			public object Config { get; set; }

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
			[EditorBrowsable(EditorBrowsableState.Never)]
			[JsonProperty("icon")]
			public string Icon { get; set; }

			#endregion
		}

		/// <summary>
		/// The grid editor with a typed config value.
		/// </summary>
		/// <typeparam name="TConfig">The type of the config value.</typeparam>
		/// <seealso cref="Our.Umbraco.GridValueConverters.Models.Grid.IEditor" />
		public class Editor<TConfig> : Editor, IEditor<TConfig>
		{
			/// <summary>
			/// Gets or sets the editor configuration.
			/// </summary>
			/// <value>
			/// The editor configuration.
			/// </value>
			[JsonProperty("config")]
			public new TConfig Config
			{
				get
				{
					return (TConfig)base.Config;
				}
				set
				{
					base.Config = value;
				}
			}
		}
	}
}