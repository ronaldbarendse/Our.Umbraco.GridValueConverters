using System;
using Newtonsoft.Json;
using Our.Umbraco.GridValueConverters.Values;
using Umbraco.Core;

namespace Our.Umbraco.GridValueConverters.JsonConverters
{
	/// <summary>
	/// A <see cref="JsonConverter" /> to read and write <see cref="HtmlGridValue" /> objects.
	/// </summary>
	/// <seealso cref="Newtonsoft.Json.JsonConverter" />
	public class HtmlGridValueConverter : JsonConverter
	{
		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		/// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		public override bool CanConvert(Type objectType)
		{
			return objectType.Inherits<HtmlGridValue>();
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
			if (Activator.CreateInstance(objectType) is HtmlGridValue htmlGridValue)
			{
				htmlGridValue.Html = reader.Value as string;

				return htmlGridValue;
			}

			return null;
		}

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is HtmlGridValue htmlGridValue)
			{
				writer.WriteValue(htmlGridValue.Html);
			}
		}
	}
}
