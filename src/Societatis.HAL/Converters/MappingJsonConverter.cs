namespace Societatis.HAL.Converters
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Societatis.Misc;

    public class MappingJsonConverter : JsonConverter
    {
        private IDictionary<Type, Type> mappings;

        public MappingJsonConverter()
        {
            this.mappings = new Dictionary<Type, Type>();
        }
        public IDictionary<Type, Type> Mappings 
        { 
            get => this.mappings;
            set
            {
                value.ThrowIfNull(nameof(value));
                this.mappings = value;
            } 
        }

        public override bool CanRead => true;
        public override bool CanWrite => false;
        public override bool CanConvert(Type objectType)
        {
            return objectType != null 
                && this.Mappings.ContainsKey(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, this.Mappings[objectType]);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}