using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Warsaken.API;

namespace Bot.Commands
{
    public class Flex : BaseCommandModule
    {
        [Command("flex")]
        public async Task CheckLoot(CommandContext ctx, string wallet)
        {
            if (String.IsNullOrEmpty(wallet))
                await ctx.RespondAsync("Not a valid command");

            HttpClient client = new();


            Balances balance = new();
            HttpResponseMessage response = await client.GetAsync("https://lightapi.eosamsterdam.net/api/balances/wax/" + wallet);

            if (response.IsSuccessStatusCode)
            {
                balance = await response.Content.ReadAsAsync<Balances>();
            }

            foreach (var item in balance.balances)
            {
                if (item.Currency == "LOOT")
                {
                    await ctx.RespondAsync(item.Amount + " loot");
                }
            }
        }
    }
}
