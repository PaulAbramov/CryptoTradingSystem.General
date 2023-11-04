namespace CryptoTradingSystem.General.Database.Models
{
    public class AssetAdditionalInformation : AssetBase
    {
        public decimal? ReturnToLastCandle { get; set; }
        public decimal? ReturnToLastCandleInPercentage { get; set; }
    }
}
