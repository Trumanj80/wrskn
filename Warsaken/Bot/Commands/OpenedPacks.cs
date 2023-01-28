using DSharpPlus.CommandsNext;
using Newtonsoft.Json;
using Warsaken;
using DSharpPlus.CommandsNext.Attributes;
using Discord;
using Warsaken.API;

namespace Bot.Commands
{
    public class OpenedPacks : BaseCommandModule
    {
        [Command("open")]
        public async Task CheckOpen(CommandContext ctx, string wallet)
        {
            Warsaken.API.OpenedPacks.Root opened = new();
            string text = File.ReadAllText(Constants.FilePaths.Opened);
            opened = JsonConvert.DeserializeObject<Warsaken.API.OpenedPacks.Root>(text);
            bool didSend = false;
            if (!wallet.Equals("loot") && !wallet.Equals("plat"))
            {
                foreach (var user in opened.Users)
                {
                    if (user.Name.Equals(wallet))
                    {
                        string final = string.Empty;
                        final += "User " + wallet + " has opened " + user.Loot + " loot packs and " + user.Plat + " platinum packs.";
                        await ctx.RespondAsync(final);
                        didSend = true;
                    }
                }

                if (didSend == false)
                {
                    await ctx.RespondAsync("User " + wallet + " did not open any packs");
                }
            }
            else
            {
                if (wallet.Equals("loot"))
                {
                    opened.Users.Sort(CompareToLoot);
                    string final = string.Empty;
                    for (int i = 0; i<opened.Users.Count || i<25;i++)
                    {
                        var user = opened.Users[i];
                        if (user.Loot != 0)
                        {
                            final += i + 1 + ". " + user.Name + " - " + user.Loot;
                        }
                    }
                }
                else if (wallet.Equals("plat"))
                {
                    opened.Users.Sort(CompareToPlat);
                    string final = string.Empty;
                    for (int i = 0; i < opened.Users.Count || i < 25; i++)
                    {
                        var user = opened.Users[i];
                        if (user.Plat != 0)
                        {
                            final += i + 1 + ". " + user.Name + " - " + user.Plat;
                        }
                    }
                }
            }
        }

        private int CompareToLoot(Warsaken.API.OpenedPacks.User x, Warsaken.API.OpenedPacks.User y)
        {
            if (x.Loot > y.Loot)
                return 1;
            else if (x.Loot == y.Loot)
                return 0;
            else
                return -1;
        }

        private int CompareToPlat(Warsaken.API.OpenedPacks.User x, Warsaken.API.OpenedPacks.User y)
        {
            if (x.Plat > y.Plat)
                return 1;
            else if (x.Plat == y.Plat)
                return 0;
            else
                return -1;
        }
    }
}
