using System.Text.Json;
using System.Text.Json.Serialization;

namespace PokeTrackerApi.RunTime.Utils;

public static class JsonSettings
{
    public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = false,
        Converters =
        {
            new JsonStringEnumConverterWithAttributeSupport(JsonNamingPolicy.CamelCase, allowIntegerValues: false)
        }
    };
}
