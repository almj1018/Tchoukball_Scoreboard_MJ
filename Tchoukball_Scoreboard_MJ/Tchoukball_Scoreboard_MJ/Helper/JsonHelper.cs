using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tchoukball_Scoreboard_MJ.Helper
{
    public class JsonHelper : JsonConverter<Key>
    {
        public override Key ReadJson(JsonReader reader, Type objectType, Key existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            return (Key)int.Parse(s);
        }

        public override void WriteJson(JsonWriter writer, Key value, JsonSerializer serializer)
        {
            writer.WriteValue((int)value);
        }
    }

    public class JsonTimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("m\\:ss"));
        }
    }
}
