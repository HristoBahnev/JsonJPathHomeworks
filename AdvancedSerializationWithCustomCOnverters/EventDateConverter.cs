using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;


namespace AdvancedSerializationWithCustomCOnverters
{
    public class EventDateConverter : JsonConverter
    {
        private const string DateFormat = "yyyy-MM-dd";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is List<Event> events)
            {
                JArray array = new JArray();
                foreach (var evt in events)
                {
                    JObject obj = new JObject
                    {
                        ["Date"] = evt.Date.ToString(DateFormat),
                        ["Name"] = evt.Name
                    };
                    array.Add(obj);
                }
                array.WriteTo(writer);
            }
            else if (value is Event evt)
            {
                JObject obj = new JObject
                {
                    ["Date"] = evt.Date.ToString(DateFormat),
                    ["Name"] = evt.Name
                };
                obj.WriteTo(writer);
            }
            else
            {
                throw new InvalidOperationException("Unsupported type for EventDateConverter");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                JArray array = JArray.Load(reader);
                var events = new List<Event>();
                foreach (var token in array)
                {
                    events.Add(new Event
                    {
                        Date = DateTime.ParseExact(token["Date"]?.ToString() ?? "", DateFormat, null),
                        Name = token["Name"]?.ToString()
                    });
                }
                return events;
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                JObject obj = JObject.Load(reader);
                return new Event
                {
                    Date = DateTime.ParseExact(obj["Date"]?.ToString() ?? "", DateFormat, null),
                    Name = obj["Name"]?.ToString()
                };
            }
            else
            {
                throw new InvalidOperationException("Invalid JSON for EventDateConverter");
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Event) || objectType == typeof(List<Event>);
        }
    }
}
