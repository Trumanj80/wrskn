using Newtonsoft.Json;
using System.Diagnostics;

namespace Warsaken.API
{
    public static class AtomicHub
    {
        public class Root
        {
            [JsonProperty("success")]
            public bool? Success { get; set; }

            [JsonProperty("data")]
            public List<Data>? Data { get; set; }

            [JsonProperty("query_time")]
            public string? QueryTime { get; set; }
        }
        
        public class PackRoot
        {
            [JsonProperty("success")]
            public bool? Success { get; set; }

            [JsonProperty("data")]
            public SinglePack? Data { get; set; }

            [JsonProperty("query_time")]
            public long? QueryTime { get; set; }
        }

        public class SingleAssetRoot
        {
            [JsonProperty("success")]
            public bool? Success { get; set; }

            [JsonProperty("data")]
            public Data? Data { get; set; }

            [JsonProperty("query_time")]
            public long? QueryTime { get; set; }
        }

        public class SinglePack
        {
            [JsonProperty("assets")]
            public string? Assets { get; set; }

            [JsonProperty("burned")]
            public string? Burned { get; set; }
        }

        public class Data
        {
            [JsonProperty("market_contract")]
            public string? MarketContract { get; set; }

            [JsonProperty("assets_contract")]
            public string? AssetsContract { get; set; }

            [JsonProperty("sale_id")]
            public string? SaleId { get; set; }

            [JsonProperty("seller")]
            public string? Seller { get; set; }

            [JsonProperty("buyer")]
            public string? Buyer { get; set; }

            [JsonProperty("offer_id")]
            public string? OfferId { get; set; }

            [JsonProperty("price")]
            public Price? Price { get; set; }

            [JsonProperty("listing_price")]
            public string? ListingPrice { get; set; }

            [JsonProperty("listing_symbol")]
            public string? ListingSymbol { get; set; }

            [JsonProperty("assets")]
            public List<Asset>? Assets { get; set; }

            [JsonProperty("maker_marketplace")]
            public string? MakerMarketplace { get; set; }

            [JsonProperty("taker_marketplace")]
            public string? TakerMarketplace { get; set; }

            [JsonProperty("collection")]
            public Collection? Collection { get; set; }

            [JsonProperty("state")]
            public string? State { get; set; }

            [JsonProperty("updated_at_block")]
            public string? UpdatedAtBlock { get; set; }

            [JsonProperty("updated_at_time")]
            public string? UpdatedAtTime { get; set; }

            [JsonProperty("created_at_block")]
            public string? CreatedAtBlock { get; set; }

            [JsonProperty("created_at_time")]
            public string? CreatedAtTime { get; set; }

            [JsonProperty("img")]
            public string? Img { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("rank")]
            public string? Rank { get; set; }

            [JsonProperty("type")]
            public string? Type { get; set; }

            [JsonProperty("legal")]
            public string? Legal { get; set; }

            [JsonProperty("setid")]
            public string? SetId { get; set; }

            [JsonProperty("cardid")]
            public string? CardId { get; set; }

            [JsonProperty("rarity")]
            public string? Rarity { get; set; }

            [JsonProperty("subset")]
            public string? Subset { get; set; }

            [JsonProperty("backimg")]
            public string? Backimg { get; set; }

            [JsonProperty("cardurl")]
            public string? Cardurl { get; set; }

            [JsonProperty("contract")]
            public string? Contract { get; set; }

            [JsonProperty("asset_id")]
            public string? AssetId { get; set; }

            [JsonProperty("owner")]
            public string? Owner { get; set; }

            [JsonProperty("is_transferable")]
            public bool? IsTransferable { get; set; }

            [JsonProperty("is_burnable")]
            public bool? IsBurnable { get; set; }

            [JsonProperty("schema")]
            public Schema? Schema { get; set; }

            [JsonProperty("template")]
            public Template? Template { get; set; }

            [JsonProperty("mutable_data")]
            public MutableData? MutableData { get; set; }

            [JsonProperty("immutable_data")]
            public ImmutableData? ImmutableData { get; set; }

            [JsonProperty("template_mint")]
            public string? TemplateMint { get; set; }

            [JsonProperty("backed_tokens")]
            public List<BackedToken>? BackedTokens { get; set; }

            [JsonProperty("burned_by_account")]
            public string? BurnedByAccount { get; set; }

            [JsonProperty("burned_at_block")]
            public string? BurnedAtBlock { get; set; }

            [JsonProperty("burned_at_time")]
            public string? BurnedAtTime { get; set; }

            [JsonProperty("transferred_at_block")]
            public string? TransferredAtBlock { get; set; }

            [JsonProperty("transferred_at_time")]
            public string? TransferredAtTime { get; set; }

            [JsonProperty("minted_at_block")]
            public string? MintedAtBlock { get; set; }

            [JsonProperty("minted_at_time")]
            public string? MintedAtTime { get; set; }

            [JsonProperty("data")]
            public Data? AssetData { get; set; }

            [JsonProperty("odds")]
            public string? Odds { get; set; }

            [JsonProperty("video")]
            public string? Video { get; set; }

            [JsonProperty("packid")]
            public string? Packid { get; set; }

            [JsonProperty("setids")]
            public string? Setids { get; set; }

            [JsonProperty("unpack")]
            public string? Unpack { get; set; }

            [JsonProperty("groupid")]
            public string? Groupid { get; set; }

            [JsonProperty("contains")]
            public string? Contains { get; set; }

            [JsonProperty("description")]
            public string? Description { get; set; }

            [JsonProperty("transfer_id")]
            public string? TransferId { get; set; }

            [JsonProperty("sender_name")]
            public string? SenderName { get; set; }

            [JsonProperty("recipient_name")]
            public string? RecipientName { get; set; }

            [JsonProperty("memo")]
            public string? Memo { get; set; }

            [JsonProperty("txid")]
            public string? Txid { get; set; }

        }

        public class Price
        {
            [JsonProperty("amount")]
            public string? Amount { get; set; }

            [JsonProperty("token_precision")]
            public string? TokenPrecision { get; set; }

            [JsonProperty("token_contract")]
            public string? TokenContract { get; set; }

            [JsonProperty("token_symbol")]
            public string? TokenSymbol { get; set; }
        }

        public class Schema
        {
            [JsonProperty("schema_name")]
            public string? SchemaName { get; set; }

            [JsonProperty("format")]
            public List<Format>? Format { get; set; }

            [JsonProperty("created_at_block")]
            public string? CreatedAtBlock { get; set; }

            [JsonProperty("created_at_time")]
            public string? CreatedAtTime { get; set; }
        }

        public class Template
        {
            [JsonProperty("template_id")]
            public string? TemplateId { get; set; }

            [JsonProperty("max_supply")]
            public string? MaxSupply { get; set; }

            [JsonProperty("issued_supply")]
            public string? IssuedSupply { get; set; }

            [JsonProperty("is_transferable")]
            public bool? IsTransferable { get; set; }

            [JsonProperty("is_burnable")]
            public bool? IsBurnable { get; set; }

            [JsonProperty("immutable_data")]
            public ImmutableData? ImmutableData { get; set; }

            [JsonProperty("created_at_time")]
            public string? CreatedAtTime { get; set; }

            [JsonProperty("created_at_block")]
            public string? CreatedAtBlock { get; set; }
        }

        public class Format
        {
            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("type")]
            public string? Type { get; set; }
        }

        public class Collection
        {
            [JsonProperty("collection_name")]
            public string? CollectionName { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("author")]
            public string? Author { get; set; }

            [JsonProperty("img")]
            public string? Img { get; set; }

            [JsonProperty("allow_notify")]
            public bool? AllowNotify { get; set; }

            [JsonProperty("authorized_accounts")]
            public List<string>? AuthorizedAccounts { get; set; }

            [JsonProperty("notify_accounts")]
            public List<string>? NotifyAccounts { get; set; }

            [JsonProperty("market_fee")]
            public string? MarketFee { get; set; }

            [JsonProperty("created_at_block")]
            public string? CreatedAtBlock { get; set; }

            [JsonProperty("created_at_time")]
            public string? CreatedAtTime { get; set; }
        }

        public class BackedToken
        {
            [JsonProperty("token_contract")]
            public string? TokenContract { get; set; }

            [JsonProperty("token_symbol")]
            public string? TokenSymbol { get; set; }

            [JsonProperty("token_precision")]
            public string? TokenPrecision { get; set; }

            [JsonProperty("amount")]
            public string? Amount { get; set; }
        }

        public class Asset
        {
            [JsonProperty("contract")]
            public string? Contract { get; set; }

            [JsonProperty("asset_id")]
            public string? AssetId { get; set; }

            [JsonProperty("owner")]
            public string? Owner { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("is_transferable")]
            public bool? IsTransferable { get; set; }

            [JsonProperty("is_burnable")]
            public bool? IsBurnable { get; set; }

            [JsonProperty("template_mint")]
            public string? TemplateMint { get; set; }

            [JsonProperty("burned_by_account")]
            public string? BurnedByAccount { get; set; }

            [JsonProperty("collection")]
            public Collection? Collection { get; set; }

            [JsonProperty("schema")]
            public Schema? Schema { get; set; }

            [JsonProperty("template")]
            public Template? Template { get; set; }

            [JsonProperty("backed_tokens")]
            public List<BackedToken>? BackedTokens { get; set; }

            [JsonProperty("immutable_data")]
            public ImmutableData? ImmutableData { get; set; }

            [JsonProperty("mutable_data")]
            public MutableData? MutableData { get; set; }

            [JsonProperty("data")]
            public Data? Data { get; set; }

            [JsonProperty("burned_at_block")]
            public string? BurnedAtBlock { get; set; }

            [JsonProperty("burned_at_time")]
            public string? BurnedAtTime { get; set; }

            [JsonProperty("updated_at_block")]
            public string? UpdatedAtBlock { get; set; }

            [JsonProperty("updated_at_time")]
            public string? UpdatedAtTime { get; set; }

            [JsonProperty("transferred_at_block")]
            public string? TransferredAtBlock { get; set; }

            [JsonProperty("transferred_at_time")]
            public string? TransferredAtTime { get; set; }

            [JsonProperty("minted_at_block")]
            public string? MintedAtBlock { get; set; }

            [JsonProperty("minted_at_time")]
            public string? MintedAtTime { get; set; }
        }

        public class ImmutableData
        {
            [JsonProperty("img")]
            public string? Img { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("rank")]
            public string? Rank { get; set; }

            [JsonProperty("type")]
            public string? Type { get; set; }

            [JsonProperty("legal")]
            public string? Legal { get; set; }

            [JsonProperty("setid")]
            public string? Setid { get; set; }

            [JsonProperty("cardid")]
            public string? Cardid { get; set; }

            [JsonProperty("rarity")]
            public string? Rarity { get; set; }

            [JsonProperty("subset")]
            public string? Subset { get; set; }

            [JsonProperty("backimg")]
            public string? Backimg { get; set; }

            [JsonProperty("cardurl")]
            public string? Cardurl { get; set; }

            [JsonProperty("odds")]
            public string? Odds { get; set; }

            [JsonProperty("video")]
            public string? Video { get; set; }

            [JsonProperty("packid")]
            public string? Packid { get; set; }

            [JsonProperty("setids")]
            public string? Setids { get; set; }

            [JsonProperty("unpack")]
            public string? Unpack { get; set; }

            [JsonProperty("groupid")]
            public string? Groupid { get; set; }

            [JsonProperty("contains")]
            public string? Contains { get; set; }

            [JsonProperty("description")]
            public string? Description { get; set; }
        }

        public class MutableData
        {

        }
    }
}
