using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTradingSystem.General.Database.Models
{
    public class Asset : AssetBase
    {
        public decimal CandleOpen { get; set; }
        public decimal CandleHigh { get; set; }
        public decimal CandleLow { get; set; }
        public decimal CandleClose { get; set; }
        public decimal Volume { get; set; }
        public decimal QuoteAssetVolume { get; set; }
        public long Trades { get; set; }
        public decimal TakerBuyBaseAssetVolume { get; set; }
        public decimal TakerBuyQuoteAssetVolume { get; set; }
        [NotMapped]
        public EMA? Ema { get; set; }
        [NotMapped]
        public SMA? Sma { get; set; }
        [NotMapped]
        public ATR? Atr { get; set; }
        [NotMapped]
        public AssetAdditionalInformation? AdditionalInformation { get; set; }
    }
}
