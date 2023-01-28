using Discord.Webhook;
using Newtonsoft.Json;
using Warsaken.API;

namespace Warsaken
{
    public static class Start
    {
        public static Dictionary<(string?, string?), int> XPValueTable = new();
        public static Dictionary<string, string?> PictureShortcuts = new();
        public static Dictionary<string, (string, string)> Accounts = new();
        public static PackData.Root Loot = new();
        public static PackData.Root Platinum = new();
        public static OpenedPacks.Root Opened = new();

        public static DiscordWebhookClient discord = new DiscordWebhookClient(Constants.Webhooks.WCS);
        public static DiscordWebhookClient discord2 = new DiscordWebhookClient(Constants.Webhooks.WCS2);

        public static async void Begin()
        {
            (XPValueTable, PictureShortcuts) = Utilities.GetJsonFileValues();
            (Loot, Platinum) = Utilities.GetPackValues();
            string text = File.ReadAllText(Constants.FilePaths.Opened);
            Opened = JsonConvert.DeserializeObject<OpenedPacks.Root>(text);
            Accounts = Utilities.GetAddresses();
            Thread thread1 = new(StartAsync);
            Thread thread2 = new(SalesAsync);
            thread1.Start();
            thread2.Start();
        }

        private static void SalesAsync()
        {
            do
            {
                try
                {
                    GetSalesSendMessage();
                    Thread.Sleep(240000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception\n" + ex.StackTrace);
                    Thread.Sleep(30000);
                }
            } while (true);
        }

        private static void StartAsync()
        {
            do
            {
                try
                {
                    GetTransfersSendMessage();
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception\n" + ex.StackTrace);
                    Thread.Sleep(30000);
                }
            } while (true);
        }

        private static async void GetTransfersSendMessage()
        {
            AtomicHub.Root list = new();
            HttpResponseMessage response = await Constants.Clients.Transfer.GetAsync(Constants.Links.Transfer);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Transfers caught " + response.StatusCode);
                list = await response.Content.ReadAsAsync<AtomicHub.Root>();
                await MessageSend.SendWarsakenTransferMessage(list);
            }
            else
            {
                Console.WriteLine("Failed to catch transfers " + response.StatusCode);
            }
        }

        private static async void GetSalesSendMessage()
        {
            AtomicHub.Root root = new AtomicHub.Root();
            HttpResponseMessage response = await Constants.Clients.OnSale.GetAsync(Constants.Links.OnSale);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Sales caught " + response.StatusCode);
                root = await response.Content.ReadAsAsync<AtomicHub.Root>();
                await MessageSend.SendWarsakenSaleMessage(root);
            }
            else
            {
                Console.WriteLine("Failed to catch sales " + response.StatusCode);
            }
        }
    }
}