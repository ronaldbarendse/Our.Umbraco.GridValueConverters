using System;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Our.Umbraco.GridValueConverters.Default.JsonConverters
{
	/// <summary>
	/// A <see cref="JsonConverter" /> to read and write <see cref="Size" /> objects.
	/// </summary>
	/// <seealso cref="Newtonsoft.Json.JsonConverter" />
	public class SizeJsonConverter : JsonConverter
	{
		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		///   <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Size);
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

			int width = obj.Value<int?>("width").GetValueOrDefault(),
				height = obj.Value<int?>("height").GetValueOrDefault();

			return new Size(width, height);
		}

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var obj = new JObject();

			var size = (Size)value;
			if (size.Width != default(int))
			{
				obj.Add("width", size.Width);
			}
			if (size.Height != default(int))
			{
				obj.Add("height", size.Height);
			}

			obj.WriteTo(writer);
		}
	}
}
