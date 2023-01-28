using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warsaken.API;
using static Warsaken.API.AtomicHub;

namespace Warsaken
{
    public static class MessageSend
    {
        public async static Task SendWarsakenTransferMessage(Root list)
        {
            long currentMax;
            StreamReader reader = new(Constants.FilePaths.Transfer);
            currentMax = long.Parse(reader.ReadLine());
            reader.Close();
            for (int j = list.Data.Count - 1; j >= 0; j--)
            {
                long max = long.Parse(list.Data[j].TransferId);
                if (currentMax < max)
                {
                    Console.WriteLine(DateTime.Now.TimeOfDay + "Sending new message");
                    if (list.Data[j].Memo.Length > 10)
                    {
                        EmbedBuilder embed = new();
                        string id = list.Data[j].Memo.Remove(0, 10);
                        SingleAssetRoot packAssetInfo = new();
                        HttpResponseMessage response = await Constants.Clients.SingleAsset.GetAsync(Constants.Links.SingleAsset + id);
                        do
                        {
                            packAssetInfo = await response.Content.ReadAsAsync<SingleAssetRoot>();
                        } while (response.IsSuccessStatusCode == false);
                        embed.Title = "Opening\n" + packAssetInfo?.Data?.Name + "\nMint #" + packAssetInfo?.Data?.TemplateMint;
                        bool isCurrentPack = false;
                        if (packAssetInfo.Data.Template.TemplateId.Equals(Start.Loot.TemplateID.ToString()) || packAssetInfo.Data.Template.TemplateId.Equals(Start.Platinum.TemplateID.ToString()))
                        {
                            isCurrentPack = true;
                        }

                        int xp = Utilities.CalculateXP(list.Data[j].Assets);
                        EmbedFooterBuilder footer = new()
                        {
                            Text = xp + " xp"
                        };

                        if (isCurrentPack)
                        {
                            Utilities.UpdateOpenedPacks(list.Data[j].RecipientName, packAssetInfo.Data.Name);
                        }
                        embed.Footer = footer;
                        bool is2MLoot = false;
                        string str = string.Empty;
                        foreach (Asset asset in list.Data[j].Assets)
                        {
                            str += asset.Template?.ImmutableData?.Name + " | ";
                            str += asset.Template?.ImmutableData?.Subset + " | ";
                            str += asset.Template?.ImmutableData?.Rank + " | ";
                            str += "Mint " + asset.TemplateMint + "\n";
                            if (asset.Template.TemplateId.Equals("442850"))
                                is2MLoot = true;
                        }
                        EmbedFieldBuilder field = new()
                        {
                            Name = "Cards",
                            Value = str
                        };
                        str = string.Empty;
                        EmbedFieldBuilder ownerField = Utilities.BuildUser(list.Data[j].RecipientName);
                        ownerField.Name = "Opened by:";
                        embed.AddField(ownerField);
                        embed.AddField(field);
                        string image = Environment.TickCount + ".png";
                        List<Asset> cards = Utilities.FilterPremium(list.Data[j].Assets);
                        Utilities.GetImagePath(cards, image);
                        embed.ImageUrl = $"attachment://{image}";
                        HashSet<Embed> embeds = new();
                        Embed emb = embed.Build();
                        embeds.Add(emb);
                        Utilities.SendAdequateServer(emb, image, cards, is2MLoot, isCurrentPack);
                        Thread.Sleep(5000);

                        File.Delete(image);
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.TimeOfDay + "It is a redeem");
                    }
                }
                else
                {
                    Console.WriteLine(DateTime.Now.TimeOfDay + "Old transfer");
                }
            }
            StreamWriter writer = new StreamWriter(Constants.FilePaths.Transfer);
            writer.Write(list.Data[0].TransferId);
            writer.Close();
        }

        public static async Task SendWarsakenSaleMessage(Root list)
        {
            long currentMax;
            StreamReader reader = new(Constants.FilePaths.OnSale);
            currentMax = long.Parse(reader.ReadLine());
            reader.Close();
            for (int j = list.Data.Count - 1; j >= 0; j--)
            {
                try
                {
                    long max = long.Parse(list.Data[j].SaleId);
                    if (currentMax < max)
                    {
                        Console.WriteLine(DateTime.Now.TimeOfDay + "Sending new message");
                        EmbedBuilder embed = new()
                        {
                            Title = "Sale\n" + "#" + list.Data[j].SaleId,
                            Url = "https://wax.atomichub.io/market/sale/" + list.Data[j].SaleId
                        };

                        int xp = Utilities.CalculateXP(list.Data[j].Assets);
                        EmbedFooterBuilder footer = new()
                        {
                            Text = xp + " xp"
                        };
                        embed.Footer = footer;
                        string str = string.Empty;
                        foreach (Asset asset in list.Data[j].Assets)
                        {
                            str += asset.Template?.ImmutableData?.Name + " | ";
                            str += asset.Template?.ImmutableData?.Subset + " | ";
                            str += asset.Template?.ImmutableData?.Rank + " | ";
                            str += "Mint " + asset.TemplateMint + "\n";
                        }
                        EmbedFieldBuilder field = new()
                        {
                            Name = "Cards",
                            Value = str
                        };
                        str = string.Empty;
                        EmbedFieldBuilder priceField = new()
                        {
                            Name = "Price",
                            Value = Utilities.CalculateWax(list.Data[j].Price.Amount) + " Wax"
                        };
                        EmbedFieldBuilder ownerField = Utilities.BuildUser(list.Data[j].Seller);
                        ownerField.Name = "Seller:";
                        embed.AddField(ownerField);
                        embed.AddField(priceField);
                        embed.AddField(field);
                        string newImage = Environment.TickCount + "1" + ".png";
                        string image = Constants.FilePaths.Images + Start.PictureShortcuts[list.Data[j].Assets[0].Template.ImmutableData.Subset] + list.Data[j].Assets[0].Template.ImmutableData.Cardid + ".png";
                        Utilities.CreateImage(new string[] { image }, 1, 1, newImage);
                        embed.ImageUrl = $"attachment://{newImage}";
                        HashSet<Embed> embeds = new();
                        Embed emb = embed.Build();
                        embeds.Add(emb);
                        await Start.discord2.SendFileAsync(newImage, Utilities.RolePing(list.Data[j].Assets, false), embeds: new List<Embed> { emb }, threadId: 1046416047466627093);
                        Thread.Sleep(2500);
                        File.Delete(newImage);
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.TimeOfDay + "Old listing");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Exception" + ex.Message);
                }
            }
            StreamWriter writer = new(Constants.FilePaths.OnSale);
            writer.Write(list.Data[0].SaleId);
            writer.Close();
        }
    }
}
