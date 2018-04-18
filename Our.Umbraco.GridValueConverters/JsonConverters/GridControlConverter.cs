using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Core;
using static Our.Umbraco.GridValueConverters.Models.Grid;

namespace Our.Umbraco.GridValueConverters.JsonConverters
{
	/// <summary>
	/// A <see cref="JsonConverter" /> to read <see cref="IControl" /> objects.
	/// </summary>
	/// <seealso cref="Newtonsoft.Json.JsonConverter" />
	public class GridControlConverter : JsonConverter
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
			return objectType.IsAbstract && objectType.Inherits<IControl>();
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
			var control = obj.ToObject<Control>(serializer);

			// Resolve to specific control type
			var controlType = GridControlsResolver.Current.ResolveControlType(control.Editor);
			if (controlType == null && objectType.IsGenericType)
			{
				// Try to make non-abstract generic control type
				if (objectType.TryGetGenericArguments(typeof(IControl<>), out Type[] genericArguments))
				{
					controlType = typeof(Control<>).MakeGenericType(genericArguments);
				}
				else if (objectType.TryGetGenericArguments(typeof(IControl<,>), out genericArguments))
				{
					controlType = typeof(Control<,>).MakeGenericType(genericArguments);
				}
			}
			
			// Return control
			if (controlType != null && controlType != control.GetType())
			{
				return obj.ToObject(controlType, serializer);
			}

			return control;
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