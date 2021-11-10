using System.Text.Json.Serialization;
namespace api.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        knight,
        mage,
        cleric
    }
}