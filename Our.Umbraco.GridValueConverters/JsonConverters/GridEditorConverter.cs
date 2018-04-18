using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Core;
using static Our.Umbraco.GridValueConverters.Models.Grid;

namespace Our.Umbraco.GridValueConverters.JsonConverters
{
	/// <summary>
	/// A <see cref="JsonConverter" /> to read <see cref="IEditor" /> and <see cref="IEditor{T}" /> objects.
	/// </summary>
	/// <seealso cref="Newtonsoft.Json.JsonConverter" />
	public class GridEditorConverter : JsonConverter
	{
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON.
		/// </summary>
		/// <value>
		///   <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON; otherwise, <c>false</c>.
		/// </value>
		public override bool CanWrite => false;

		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		///   <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		public override bool CanConvert(Type objectType)
		{
			return objectType.IsAbstract && objectType.Inherits<IEditor>();
		}

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>
		/// The object value.
		/// </returns>
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var obj = JObject.Load(reader);
			var editor = obj.ToObject<Editor>(serializer);

			// Resolve to specific editor type
			var editorType = GridControlsResolver.Current.ResolveEditorType(editor);
			if (editorType == null && objectType.IsGenericType)
			{
				// Try to make non-abstract generic editor type
				if (objectType.TryGetGenericArguments(typeof(IEditor<>), out Type[] genericArguments))
				{
					editorType = typeof(Editor<>).MakeGenericType(genericArguments);
				}
			}

			// Return editor
			if (editorType != null && editorType != editor.GetType())
			{
				return obj.ToObject(editorType, serializer);
			}

			return editor;
		}

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <exception cref="NotImplementedException">This converter does not support writing JSON.</exception>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException("This converter does not support writing JSON.");
		}
	}
}