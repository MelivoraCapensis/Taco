using System;
using Newtonsoft.Json;
using TacoChallenge.Models;


namespace TacoChallenge.Data
{
    //Custom serializer to Json.Net to tell it how to handle the deserealization for Generic Interface
    public class EntityJsonConverter : JsonConverter
    {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(IEntity);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return serializer.Deserialize(reader, typeof(Resturant));
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
    }
}