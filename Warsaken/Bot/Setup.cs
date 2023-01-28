using static Warsaken.Utilities;
using Warsaken.API;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Bot.Commands;

namespace Bot
{
    public class Setup
    {
        public static Dictionary<(string, string), int> XPTable = new();
        public static PackData.Root Loot = new();
        public static PackData.Root Platinum = new();
        private string Token = "";
        private string Prefix = "!";
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public async Task RunAsync()
        {
            XPTable = GetJsonFileValues().Item1;
            (Loot, Platinum) = GetPackValues();

            DiscordConfiguration config = new()
            {
                Token = Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug

            };

            Client = new(config);


            CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { Prefix },
                EnableMentionPrefix = true,
                EnableDms = false,
                DmHelp = true,
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<Flex>();
            Commands.RegisterCommands<CalculateXP>();
            Commands.RegisterCommands<PackChances>();
            //Commands.RegisterCommands<Commands.OpenedPacks>();

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}
