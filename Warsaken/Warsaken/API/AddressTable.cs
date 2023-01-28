using Newtonsoft.Json;

namespace Warsaken.API
{
    public static class AddressTable
    {
        public class Root
        {
            [JsonProperty("user")]
            public List<User>? Users { get; set; }
        }

        public class User
        {
            [JsonProperty("discordID")]
            public string? DiscordID { get; set; }

            [JsonProperty("username")]
            public string? Username { get; set; }

            [JsonProperty("wax")]
            public string? Wax { get; set; }
        }
    }
}
