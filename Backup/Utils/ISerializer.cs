using Newtonsoft.Json;

namespace Backup.Utils
{
    public interface ISerializer
    {
        internal T Deserialize<T>(string json);
    }
    internal class NewtonsoftJsonSerializer : ISerializer
    {
        T ISerializer.Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json)!;
        }
    }
}

