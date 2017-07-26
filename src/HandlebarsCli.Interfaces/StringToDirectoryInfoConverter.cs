using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HandlebarsCli.HandlebarsTemplateProcessor
{
    public class FileSystemInfoConverter<TFileSystemInfo> : JsonConverter where TFileSystemInfo : FileSystemInfo
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, ((DirectoryInfo) value)?.FullName);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

           var val =  serializer.Deserialize<string>(reader);
            return new DirectoryInfo(val);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }


    }

    //public class ConcreteTypeConverter<TConcrete> : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        //assume we can convert to anything for now
    //        return true;
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        //explicitly specify the concrete type we want to create
    //        return serializer.Deserialize<TConcrete>(reader);
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        //use the default serialization - it works fine
    //        serializer.Serialize(writer, value);
    //    }
    //}
}
