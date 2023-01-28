using Newtonsoft.Json;

namespace Warsaken.API
{
    public static class PackData
    {
        public class Root
        {
            [JsonProperty("TemplateID")]
            public int? TemplateID { get; set; }

            [JsonProperty("Blood Foil Infinite")]
            public int? BloodFoilInfinite { get; set; }

            [JsonProperty("Blood Foil G-Force")]
            public int? BloodFoilGForce { get; set; }

            [JsonProperty("Owner")]
            public int? Owner { get; set; }

            [JsonProperty("Tenant")]
            public int? Tenant { get; set; }

            [JsonProperty("Blood Foil Leader")]
            public int? BloodFoilLeader { get; set; }

            [JsonProperty("Decadent Leader")]
            public int? DecadentLeader { get; set; }

            [JsonProperty("24K Gold Loot Leader")]
            public int? _24KGoldLootLeader { get; set; }

            [JsonProperty("2M Loot")]
            public int? _2MLoot { get; set; }
        }
    }
}
