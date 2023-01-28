using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsaken
{
    public static class Constants
    {
        public static class Links
        {
            public const string? Transfer = "https://wax.api.atomicassets.io/atomicassets/v1/transfers?sender=pre.warsaken&collection_name=warsaken&page=1&limit=5&order=desc&sort=created";
            public const string? SingleAsset = "https://wax.api.atomicassets.io/atomicassets/v1/assets/";
            public const string? OnSale = "https://wax.api.atomicassets.io/atomicmarket/v2/sales?state=1&collection_name=warsaken&schema_name=cards&page=1&limit=20&order=desc&sort=created";
            public const string? Sold = "https://wax.api.atomicassets.io/atomicmarket/v2/sales?state=3&collection_name=warsaken&schema_name=cards&page=1&limit=20&order=desc&sort=created";
            public const string? Template = "https://wax.api.atomicassets.io/atomicassets/v1/templates/warsaken/";
        }

        public static class Webhooks
        {
            public const string? WCS = "";
            public const string? WCS2 = "";
        }

        public static class Clients
        {
            public static HttpClient Transfer = new();
            public static HttpClient SingleAsset = new();
            public static HttpClient OnSale = new();
        }

        public static class Subsets
        {
            public const string? Thermal_Ash = "Thermal Ash";
            public const string? Unique = "Unique";
            public const string? Gold = "24K Gold";
            public const string? Hacked = "Hacked";
            public const string? Black_Camo = "Black Camo";
            public const string? Smoke_Noir = "Smoke Noir";
            public const string? Full_Art_Medal = "Full Art Medal";
            public const string? Full_Art = "Full Art";
            public const string? Full_Art_Resource = "Full Art (Resource)";
            public const string? Base = "Base";
            public const string? Loot = "Loot";
            public const string? Gold_Loot = "24K Gold (Loot)";
            public const string? Blood_Foil_Infinite = "Blood Foil (Infinite)";
            public const string? Blood_Foil_G_Force = "Blood Foil (G-Force)";
            public const string? Blood_Foil = "Blood Foil";
            public const string? Decadent = "Decadent";
            public const string? Owner = "Owner";
            public const string? Tenant = "Tenant";
            public const string? Upgrade = "Upgrade";
        }

        public static class Dimensions
        {
            public const int Width = 630;
            public const int Height = 880;
        }

        public static class CardTypes
        {
            public const string? Leader = "Leader";
            public const string? G_Force = "G Force";
            public const string? Infinite = "Infinite";
        }

        public static class SchemaNames
        {
            public const string? Pack = "packs";
            public const string? Card = "cards";
        }

        public static class Ranks
        {
            public const string? Rank4 = "Rank 4";
        }

        public static class FilePaths
        {
            public static string? Transfer = Path.GetFullPath(@"..\..\..\..\") + "Warsaken\\Data\\Transfer.txt";
            public static string? OnSale = Path.GetFullPath(@"..\..\..\..\") + "Warsaken\\Data\\OnSale.txt";
            public static string? AddressesFile = Path.GetFullPath(@"..\..\..\..\") + "Warsaken\\Data\\Addresses.json";
            public static string? XPTable = Path.GetFullPath(@"..\..\..\..\") + "Warsaken\\Data\\XPTable.json";
            public static string? Loot = Path.GetFullPath(@"..\..\..\..\") + "Warsaken\\Data\\Loot.json";
            public static string? Platinum = Path.GetFullPath(@"..\..\..\..\") + "Warsaken\\Data\\Plat.json";
            public static string? Opened = Path.GetFullPath(@"..\..\..\..\") + "Warsaken\\Data\\OpenedPacks.json";
            public static string? Images = Path.GetFullPath(@"..\..\..\..\") + "Warsaken\\Images\\";
        }

        public static class Roles
        {
            public static string? BloodFoil = "<@&1038762994164834394>";
            public static string? Owner = "<@&1038763171600670750>";
            public static string? Decadent = "<@&1038778628726599700>";
            public static string? Tenant = "<@&1038763215712165918>";
            public static string? ThermalAsh = "<@&1038763320716562471>";
            public static string? SmokeNoir = "<@&1038763327381315674>";
            public static string? Hacked = "<@&1038778494605344868>";
            public static string? Gold = "<@&1038780370029641748>";
            public static string? Loot = "<@&1039088315594457088>";
        }
    }
}
