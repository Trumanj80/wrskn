using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Diagnostics;
using Warsaken;
using Warsaken.API;
using static Warsaken.API.AtomicHub;

namespace Bot.Commands
{
    public class PackChances : BaseCommandModule
    {
        public static Stopwatch Stopwatch = new();
        [Command("chances")]
        public async Task GetChances(CommandContext ctx)
        {
            if (Stopwatch.IsRunning == false)
            {
                Stopwatch.Start();
            }
            if (Stopwatch.ElapsedMilliseconds > 10000)
            {
                await ctx.RespondAsync(WritePackData().Result);
                Stopwatch.Reset();
                Stopwatch.Start();
            }
            else
            {
                await ctx.RespondAsync("Please wait a bit before running this command again");
            }
        }

        private static async Task<string> WritePackData()
        {
            string final = string.Empty;
            string generic = "The API has issues try again later";
            PackData.Root loot;
            PackData.Root plat;
            HttpClient client = new();
            HttpResponseMessage response;
            (loot, plat) = Utilities.GetPackValues();
            double totalCardsLoot = 5000;
            double totalCardsPlat = 3500;
            string lootLink = Constants.Links.Template + loot.TemplateID + "/stats";
            string platLink = Constants.Links.Template + plat.TemplateID + "/stats";
            AtomicHub.PackRoot rootLoot;
            AtomicHub.PackRoot rootPlat;

            response = await client.GetAsync(lootLink);
            if (response.IsSuccessStatusCode)
            {
                rootLoot = await response.Content.ReadAsAsync<AtomicHub.PackRoot>();
            }
            else
            {
                return generic;
            }

            response = await client.GetAsync(platLink);
            if (response.IsSuccessStatusCode)
            {
                rootPlat = await response.Content.ReadAsAsync<PackRoot>();
            }
            else
            {
                return generic;
            }

            totalCardsLoot -= int.Parse(rootLoot.Data.Burned);
            totalCardsPlat -= int.Parse(rootPlat.Data.Burned);

            final += "Left in loot packs\n";
            final += "Opened " + rootLoot.Data.Burned + "\n";
            final += "Purchased " + rootLoot.Data.Assets + "\n";
            final += CheckIfTheyContainSame(loot, totalCardsLoot);
            final += "\n\n";

            final += "Left in platinum loot packs\n";
            final += "Opened " + rootPlat.Data.Burned + "\n";
            final += "Purchased " + rootPlat.Data.Assets + "\n";
            final += CheckIfTheyContainSame(plat, totalCardsPlat);

            return final;
        }

        private static string CheckIfTheyContainSame(PackData.Root left, double total)
        {
            total /= 100;
            string final = string.Empty;
                final += left.BloodFoilInfinite + " — Blood Foil Infinite — " + Math.Round((double)left.BloodFoilInfinite / total, 4) + "%\n";
                final += left.BloodFoilGForce + " — Blood Foil G-Force — " + Math.Round((double)left.BloodFoilGForce / total, 4) + "%\n";
                final += left.BloodFoilLeader + " — Blood Foil Leader — " + Math.Round((double)left.BloodFoilLeader / total, 4) + "%\n";
                final += left.Owner + " — Owner — " + Math.Round((double)left.Owner / total, 4) + "%\n";
                final += left.Tenant + " — Tenant — " + Math.Round((double)left.Tenant / total, 4) + "%\n";
                final += left.DecadentLeader + " — Decadent Leader — " + Math.Round((double)left.DecadentLeader / total, 4) + "%\n";
                final += left._24KGoldLootLeader + " — Gold Loot Leader — " + Math.Round((double)left._24KGoldLootLeader / total, 4) + "%\n";
                final += left._2MLoot + " — 2,000,000 Loot — " + Math.Round((double)left._2MLoot / total, 4) + "%\n";
            return final;
        }
    }
}