using Discord;
using Newtonsoft.Json;
using System.Drawing;
using Warsaken.API;
using static Warsaken.API.AtomicHub;

namespace Warsaken
{
    public static class Utilities
    {
        public static string CalculateWax(string str)
        {
            double price = double.Parse(str);
            price /= 100000000;
            return price.ToString();
        }

        public static (Dictionary<(string?, string?), int>, Dictionary<string, string?>) GetJsonFileValues()
        {
            Dictionary<(string?, string?), int> xp = new();
            Dictionary<string, string?> pic = new();
            string text = File.ReadAllText(Constants.FilePaths.XPTable);
            XPTable.Root list = JsonConvert.DeserializeObject<XPTable.Root>(text);
            for (int i = 0; i < list.Types.Count; i++)
            {
                xp.TryAdd((list.Types[i].Variation, list.Types[i].Rank), list.Types[i].XpValue);
                pic.TryAdd(list.Types[i].Variation, list.Types[i].Shortcut);
            }
            return (xp, pic);
        }

        public static Dictionary<string, (string, string)> GetAddresses()
        {
            Dictionary<string, (string, string)> address = new();
            string text = File.ReadAllText(Constants.FilePaths.AddressesFile);
            AddressTable.Root list = JsonConvert.DeserializeObject<AddressTable.Root>(text);
            for (int i = 0; i < list.Users.Count; i++)
            {
                address.TryAdd(list.Users[i].Wax, (list.Users[i].Username, list.Users[i].DiscordID));
            }
            return address;
        }

        public static void UpdateOpenedPacks(string wam, string pack)
        {
            bool didChange = false;
            foreach(API.OpenedPacks.User user in Start.Opened.Users)
            {
                if (user.Name.Equals(wam))
                {
                    didChange = true;
                    if (pack.Contains("Platinum"))
                    {
                        user.Plat++;
                    }
                    else
                    {
                        user.Loot++;
                    }
                }
            }

            if (!didChange)
            {
                OpenedPacks.User user = new(wam);
                if (pack.Contains("Platinum"))
                {
                    user.Plat++;
                }
                else
                {
                    user.Loot++;
                }
                Start.Opened.Users.Add(user);
            }

            string text = JsonConvert.SerializeObject(Start.Opened);
            File.WriteAllText(Constants.FilePaths.Opened, text);
        }

        public static (PackData.Root, PackData.Root) GetPackValues()
        {
            string text = File.ReadAllText(Constants.FilePaths.Loot);
            PackData.Root loot = JsonConvert.DeserializeObject<PackData.Root>(text);
            text = File.ReadAllText(Constants.FilePaths.Platinum);
            PackData.Root plat = JsonConvert.DeserializeObject<PackData.Root>(text);
            return (loot, plat);
        }

        public static string RolePing(List<Asset> cards, bool has2MLoot)
        {
            string final = string.Empty;
            foreach (Asset asset in cards)
            {
                string subset = asset.Template.ImmutableData.Subset;
                if (subset.Equals(Constants.Subsets.Smoke_Noir))
                {
                    final += Constants.Roles.SmokeNoir + "\n";
                }
                else if (subset.Equals(Constants.Subsets.Gold) || subset.Equals(Constants.Subsets.Gold_Loot))
                {
                    final += Constants.Roles.Gold + "\n";
                }
                else if (subset.Equals(Constants.Subsets.Blood_Foil_Infinite) || subset.Equals(Constants.Subsets.Blood_Foil) || subset.Equals(Constants.Subsets.Blood_Foil_G_Force))
                {
                    final += Constants.Roles.BloodFoil + "\n";
                }
                else if (subset.Equals(Constants.Subsets.Hacked))
                {
                    final += Constants.Roles.Hacked + "\n";
                }
                else if (subset.Equals(Constants.Subsets.Owner))
                {
                    final += Constants.Roles.Owner + "\n";
                }
                else if (subset.Equals(Constants.Subsets.Tenant))
                {
                    final += Constants.Roles.Tenant + "\n";
                }
                else if (subset.Equals(Constants.Subsets.Decadent))
                {
                    final += Constants.Roles.Decadent + "\n";
                }
                else if (subset.Equals(Constants.Subsets.Thermal_Ash))
                {
                    final += Constants.Roles.ThermalAsh + "\n";
                }
            }

            if (has2MLoot)
            {
                final += Constants.Roles.Loot;
            }
            return final;
        }


        public static int CalculateXP(List<Asset>? list)
        {
            int xp = 0;
            foreach (Asset asset in list)
            {
                if (asset.Schema.SchemaName != Constants.SchemaNames.Pack)
                {
                    string? subset = asset?.Template?.ImmutableData?.Subset;
                    string? rank = asset?.Template?.ImmutableData?.Rank;
                    int id = int.Parse(asset.Template.ImmutableData.Cardid);
                    string? type = asset.Template?.ImmutableData?.Type;
                    if (rank == Constants.Ranks.Rank4 && Utilities.NonSeparableSubsets(subset) && ((id > 300 && type == Constants.CardTypes.Leader) || type == Constants.CardTypes.G_Force))
                    {
                        if (type == Constants.CardTypes.G_Force)
                        {
                            xp += Start.XPValueTable[(subset, "G-Force")];
                        }
                        else
                        {
                            xp += Start.XPValueTable[(subset, Constants.CardTypes.Infinite)];
                        }
                    }
                    else
                    {
                        xp += Start.XPValueTable[(subset, rank)];
                    }
                }
            }
            return xp;
        }

        public static bool NonSeparableSubsets(string? subset)
        {
            if (subset == Constants.Subsets.Thermal_Ash || subset == Constants.Subsets.Unique || subset == Constants.Subsets.Gold
                || subset == Constants.Subsets.Hacked || subset == Constants.Subsets.Black_Camo || subset == Constants.Subsets.Smoke_Noir || subset == Constants.Subsets.Full_Art_Medal)
            {
                return true;
            }
            return false;
        }

        public static void GetImagePath(List<Asset> list, string imagePath)
        {
            int width = (int)Math.Ceiling(Math.Sqrt(list.Count));
            int height = (int)Math.Ceiling((double)list.Count / width);
            string[] paths = GetPaths(list);
            CreateImage(paths, width, height, imagePath);
        }

        private static string[] GetPaths(List<Asset> list)
        {
            string[] paths = new string[list.Count];
            for (int index = 0; index < list.Count; index++)
            {
                string? cardID = list[index].Template?.ImmutableData?.Cardid;
                string? var = list[index].Template?.ImmutableData?.Subset;
                if (cardID != null && var != null)
                {
                    paths[index] = Constants.FilePaths.Images + Start.PictureShortcuts[var] + cardID + ".png";
                }
            }
            paths = paths.Where(c => c != null).ToArray();
            return paths;
        }

        public static void CreateImage(string[] paths, int width, int height, string imagePath)
        {
            Bitmap destination = new(width * Constants.Dimensions.Width, height * Constants.Dimensions.Height);
            destination.MakeTransparent();
            List<Bitmap> streams = new();
            foreach (string path in paths)
            {
                streams.Add(new Bitmap(path));
            }
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    for (int i = 0; i < Constants.Dimensions.Height; i++)
                    {
                        for (int j = 0; j < Constants.Dimensions.Width; j++)
                        {
                            if (x * width + y < streams.Count)
                                destination.SetPixel(Constants.Dimensions.Width * y + j, Constants.Dimensions.Height * x + i, streams.ElementAt(x * width + y).GetPixel(j, i));
                        }
                    }
                }
            }
            destination.Save(imagePath);
            destination.Dispose();
        }

        public static List<Asset> FilterPremium(List<Asset> list)
        {
            List<Asset> assets = new();
            foreach (Asset asset in list)
            {
                string? subset = asset.Template.ImmutableData.Subset;
                if (subset != Constants.Subsets.Full_Art && subset != Constants.Subsets.Full_Art_Resource &&
                    subset != Constants.Subsets.Base && subset != Constants.Subsets.Loot && subset != Constants.Subsets.Upgrade)
                    assets.Add(asset);
            }
            return assets;
        }

        public static void SendAdequateServer(Embed embed, string image, List<Asset> cards, bool has2MLoot, bool isCurrentPack)
        {
            if (embed.Title.Contains("Booster"))
            {
                Start.discord.SendFileAsync(image, RolePing(cards, has2MLoot), false, new List<Embed> { embed }, threadId: 1038762022466244619);
            }
            else if (embed.Title.Contains("Elite"))
            {
                Start.discord.SendFileAsync(image, RolePing(cards, has2MLoot), false, new List<Embed> { embed }, threadId: 1038762104984965170);
            }
            else if (embed.Title.Contains("Premium"))
            {
                Start.discord.SendFileAsync(image, RolePing(cards, has2MLoot), false, new List<Embed> { embed }, threadId: 1038762506002378752);
            }
            else if (embed.Title.Contains("Platinum"))
            {
                Utilities.CheckIfDesiredPull(has2MLoot, cards, "Platinum", isCurrentPack);
                Start.discord.SendFileAsync(image, RolePing(cards, has2MLoot), false, new List<Embed> { embed }, threadId: 1038762713863688222);
            }
            else if (embed.Title.Contains("Loot"))
            {
                Utilities.CheckIfDesiredPull(has2MLoot, cards, "Loot", isCurrentPack);
                Start.discord.SendFileAsync(image, RolePing(cards, has2MLoot), false, new List<Embed> { embed }, threadId: 1038762603117289503);
            }
        }

        public static EmbedFieldBuilder BuildUser(string user)
        {
            EmbedFieldBuilder userField = new();
            string field = "**" + user + "**";
            if (Start.Accounts.ContainsKey(user))
            {
                field += "\n" + Start.Accounts[user].Item1 + "\n";
                if (Start.Accounts[user].Item2 != "")
                {
                    field += "<@" + Start.Accounts[user].Item2 + ">";
                }
            }
            userField.Value = field;
            return userField;
        }

        public static void CheckIfDesiredPull(bool has2MLoot, List<Asset> cards, string type, bool isNew)
        {
            bool isDesired = false;

            if (isNew)
            {
                if (type.Equals("Loot"))
                {
                    isDesired = TryDeductValues(Start.Loot, cards, has2MLoot);
                }
                else
                {
                    isDesired = TryDeductValues(Start.Platinum, cards, has2MLoot);
                }
            }

            if (isDesired)
            {
                if (type.Equals("Loot"))
                {
                    MakePackEmbed(Start.Loot, "Loot", Start.Loot.TemplateID);
                    string output = JsonConvert.SerializeObject(Start.Loot);
                    File.WriteAllText(Constants.FilePaths.Loot, output);
                }
                else
                {
                    MakePackEmbed(Start.Platinum, "Platinum", Start.Platinum.TemplateID);
                    string output = JsonConvert.SerializeObject(Start.Platinum);
                    File.WriteAllText(Constants.FilePaths.Platinum, output);
                }
            }
        }

        public static bool TryDeductValues(PackData.Root root, List<Asset> cards, bool has2MLoot)
        {
            bool deducted = false;
            if (has2MLoot)
            {
                root._2MLoot--;
                deducted = true;
            }
            foreach (Asset card in cards)
            {
                string subset = card.Template.ImmutableData.Subset;
                string type = card.Template.ImmutableData.Type;
                if (subset.Equals(Constants.Subsets.Decadent))
                {
                    root.DecadentLeader--;
                    deducted = true;
                }
                else if (subset.Equals(Constants.Subsets.Gold_Loot))
                {
                    root._24KGoldLootLeader--;
                    deducted = true;
                }
                else if (subset.Equals(Constants.Subsets.Blood_Foil_Infinite))
                {
                    root.BloodFoilInfinite--;
                    deducted = true;
                }
                else if (subset.Equals(Constants.Subsets.Blood_Foil_G_Force))
                {
                    root.BloodFoilGForce--;
                    deducted = true;
                }
                else if (subset.Equals(Constants.Subsets.Owner))
                {
                    root.Owner--;
                    deducted = true;
                }
                else if (subset.Equals(Constants.Subsets.Tenant))
                {
                    root.Tenant--;
                    deducted = true;
                }
                else if (subset.Equals(Constants.Subsets.Blood_Foil) && type.Equals(Constants.CardTypes.Leader))
                {
                    root.BloodFoilLeader--;
                    deducted = true;
                }
            }
            return deducted;
        }


        public static async void MakePackEmbed(PackData.Root pack, string type, int? templateID)
        {

            HttpClient client = new();
            PackRoot packData = new();
            HttpResponseMessage response = await client.GetAsync(Constants.Links.Template + templateID + "/stats");
            if (response.IsSuccessStatusCode)
            {
                packData = await response.Content.ReadAsAsync<PackRoot>();
            }

            EmbedBuilder embed = new EmbedBuilder
            {
                Title = "Current Standings of " + type + " packs"
            };
            EmbedFieldBuilder currentStatsField = new()
            {
                Name = "Current Stats",
                Value = "Purchased: " + packData.Data.Assets + "\n" + "Opened: " + packData.Data.Burned + "\n"
            };
            embed.AddField(currentStatsField);
            EmbedFieldBuilder field = new()
            {
                Name = "Blood Foil Infinite",
                Value = pack.BloodFoilInfinite
            };
            embed.AddField(field);
            field = new()
            {
                Name = "Blood Foil G-Force",
                Value = pack.BloodFoilGForce
            };
            embed.AddField(field);
            field = new()
            {
                Name = "Owner",
                Value = pack.Owner
            };
            embed.AddField(field);
            field = new()
            {
                Name = "Tenant",
                Value = pack.Tenant
            };
            embed.AddField(field);
            field = new()
            {
                Name = "Blood Foil Leader",
                Value = pack.BloodFoilLeader
            };
            embed.AddField(field);
            field = new()
            {
                Name = "Decadent Leaders",
                Value = pack.DecadentLeader
            };
            embed.AddField(field);
            field = new()
            {
                Name = "24K Gold Loot Leaders",
                Value = pack._24KGoldLootLeader
            };
            embed.AddField(field);
            field = new()
            {
                Name = "2 Million Loot Card",
                Value = pack._2MLoot
            };
            embed.AddField(field);
            Embed emb = embed.Build();
            Start.discord.SendMessageAsync("", false, new List<Embed> { emb });
        }
    }
}
