using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Keikobar.Utility;

public static class SessionExtensions
{
    //Method Serialisation
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }
    //Method Deserialisation
    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}