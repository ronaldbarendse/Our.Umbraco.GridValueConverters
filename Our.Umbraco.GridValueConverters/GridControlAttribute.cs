using System;
using Our.Umbraco.GridValueConverters.Models;
using Umbraco.Core;

namespace Our.Umbraco.GridValueConverters
{
	/// <summary>
	/// Apply this attribute on a <see cref="Models.Grid.IControl" /> to map the class to a grid editor alias or view (as specified in the gridEditors config).
	/// </summary>
	/// <seealso cref="System.Attribute" />
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class GridControlAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets the editor alias.
		/// </summary>
		/// <value>
		/// The editor alias.
		/// </value>
		public string EditorAlias { get; set; }

		/// <summary>
		/// Gets or sets the editor view.
		/// </summary>
		/// <value>
		/// The editor view.
		/// </value>
		public string EditorView { get; set; }

		/// <summary>
		/// Determines whether the specified <see cref="Grid.IEditor" /> is for the attributed control.
		/// </summary>
		/// <param name="editor">The editor.</param>
		/// <returns>
		///   <c>true</c> if the editor is for the attributed control; otherwise, <c>false</c>.
		/// </returns>
		/// <exception cref="ArgumentNullException">editor</exception>
		/// <remarks>
		/// This method can be overridden to allow for custom matching logic.
		/// </remarks>
		public virtual bool IsControlEditor(Grid.IEditor editor)
		{
			if (editor == null) throw new ArgumentNullException(nameof(editor));

			// Match on alias (case insensitive) or view (case insensitive and with or without ~/)
			if ((!String.IsNullOrEmpty(this.EditorAlias) && this.EditorAlias.InvariantEquals(editor.Alias)) ||
				(!String.IsNullOrEmpty(this.EditorView) && this.EditorView.EnsureStartsWith("~/").InvariantEquals(editor.View?.EnsureStartsWith("~/"))))
			{
				return true;
			}

			return false;
		}
	}
}