using System.Collections.Generic;
using CryptoTradingSystem.General.Data;

namespace CryptoTradingSystem.General.Strategy;

public class StrategyReturnParameter
{
    public Enums.TradeType TradeType { get; set; }
    public Enums.TradeStatus TradeStatus { get; set; }
    public double? StopLossPercentage { get; set; }
    public double? TakeProfitPercentage { get; set; }
    public double? TrailingStopLossPercentage { get; set; }
    public double? TrailingTakeProfitPercentage { get; set; }
}