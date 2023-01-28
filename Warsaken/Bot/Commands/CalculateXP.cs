using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Diagnostics;
using Warsaken;
using Warsaken.API;
using static Warsaken.API.AtomicHub;

namespace Bot.Commands
{
    public class CalculateXP : BaseCommandModule
    {
        public static Stopwatch timer = new();
        [Command("calcxp")]
        public async Task CheckXP(CommandContext ctx, string wallet)
        {
            if (timer.IsRunning == false)
            {
                timer.Start();
            }
            if (wallet.Equals("pre.warsaken"))
            {
                await ctx.RespondAsync("I won't do it. You can't force me");
                return;
            }
            if (timer.ElapsedMilliseconds > 60000)
            {
                if (timer.IsRunning)
                {
                    timer.Stop();
                    timer.Reset();
                }
                AtomicHub.Root main = new();
                HttpClient client = new();
                HttpResponseMessage response;
                int numberOfCards = 0;
                long xp = 0;
                int counter = 0;
                int count = 0;
                do
                {
                    counter++;
                    main = new();
                    response = await client.GetAsync("https://wax.api.atomicassets.io/atomicassets/v1/assets?collection_name=warsaken&owner=" + wallet + "&page=" + counter + "&limit=1000&order=desc&sort=asset_id");
                    if (response.IsSuccessStatusCode)
                    {
                        main = await response.Content.ReadAsAsync<AtomicHub.Root>();
                        count = main.Data.Count;
                        if (count != 0)
                        {
                            xp += Calculate(main.Data);
                            numberOfCards += count;
                        }
                    }
                    else
                    {
                        count = 1;
                        counter--;
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        Thread.Sleep(10000);
                    }
                    Console.WriteLine(response.StatusCode);
                    Thread.Sleep(100);
                } while (count != 0);

                await ctx.RespondAsync("You have " + xp + " xp from " + numberOfCards + " cards");
                timer.Start();
            }
            else
            {
                await ctx.RespondAsync("You have to wait a bit until this command can be used again");
            }
        }
            public static int Calculate(List<Data>? list)
            {
                int xp = 0;
                foreach (Data asset in list)
                {
                    if (asset.Schema.SchemaName != Constants.SchemaNames.Pack)
                    {
                        string? subset = asset?.Template?.ImmutableData?.Subset;
                        string? rank = asset?.Template?.ImmutableData?.Rank;
                        int id = int.Parse(asset.Template.ImmutableData.Cardid);
                        string? type = asset.Template?.ImmutableData?.Type;
                        if (rank == Constants.Ranks.Rank4 && Utilities.NonSeparableSubsets(subset) &&
                            ((id > 300 && type == Constants.CardTypes.Leader) || type == Constants.CardTypes.G_Force))
                        {
                            if (type == Constants.CardTypes.G_Force)
                            {
                                xp += Setup.XPTable[(subset, "G-Force")];
                            }
                            else
                            {
                                xp += Setup.XPTable[(subset, Constants.CardTypes.Infinite)];
                            }
                        }
                        else
                        {
                            xp += Setup.XPTable[(subset, rank)];
                        }
                    }
                }
                return xp;
            }
    }
}
