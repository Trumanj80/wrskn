using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;

namespace Warsaken.API
{
    public partial class Balances
    {
        [JsonProperty("chain")]
        public Chain? Chain { get; set; }

        [JsonProperty("account_name")]
        public string? AccountName { get; set; }

        [JsonProperty("balances")]
        public List<BalanceElement>? balances { get; set; }
    }

    public partial class BalanceElement
    {
        [JsonProperty("contract")]
        public string? Contract { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }

        [JsonProperty("currency")]
        public string? Currency { get; set; }

        [JsonProperty("decimals")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Decimals { get; set; }
    }

    public partial class Chain
    {
        [JsonProperty("decimals")]
        public long? Decimals { get; set; }

        [JsonProperty("network")]
        public string? Network { get; set; }

        [JsonProperty("block_num")]
        public long? BlockNum { get; set; }

        [JsonProperty("systoken")]
        public string? Systoken { get; set; }

        [JsonProperty("sync")]
        public long? Sync { get; set; }

        [JsonProperty("chainid")]
        public string? Chainid { get; set; }

        [JsonProperty("block_time")]
        public DateTimeOffset? BlockTime { get; set; }

        [JsonProperty("rex_enabled")]
        public long? RexEnabled { get; set; }

        [JsonProperty("production")]
        public long? Production { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }
    }

    public partial class Balances
    {
        public static Balances FromJson(string json) => JsonConvert.DeserializeObject<Balances>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Balances self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object? ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
