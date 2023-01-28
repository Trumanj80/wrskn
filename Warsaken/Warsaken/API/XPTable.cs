using Newtonsoft.Json;

namespace Warsaken.API
{
    public static class XPTable
    {
        public class Root
        {
            [JsonProperty("type")]
            public List<Type>? Types { get; set; }
        }

        public class Type
        {
            [JsonProperty("variation")]
            public string? Variation { get; set; }

            [JsonProperty("rank")]
            public string? Rank { get; set; }

            [JsonProperty("xpValue")]
            public int XpValue { get; set; }

            [JsonProperty("picShortcut")]
            public string? Shortcut { get; set; }
        }
    }
}
