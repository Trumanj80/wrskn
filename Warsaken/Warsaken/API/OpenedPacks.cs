using Newtonsoft.Json;

namespace Warsaken.API
{
    public static class OpenedPacks
    {
        public class Root
        {
            [JsonProperty("users")]
            public List<User>? Users { get; set; }
        }

        public class User
        {
            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("loot")]
            public int? Loot { get; set; }

            [JsonProperty("plat")]
            public int? Plat { get; set; }

            public User(string name)
            {
                this.Name = name;
                Loot = 0;
                Plat = 0;
            }

            
        }


    }
}
