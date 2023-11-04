using System;

namespace CryptoTradingSystem.General.Strategy;

public class StrategyApprovementStatistics
{
    public DateTime MinimalValidationDuration { get; set; }
    public decimal? ProfitLoss { get; set; }
    public decimal? Winrate { get; set; }
    public decimal? SharpeRatio { get; set; }
    public decimal? SortinoRatio { get; set; }
    public decimal? CalmarRatio { get; set; }
    public decimal? MaxAllowedDrawdown { get; set; }
    public decimal? Volatility { get; set; }
    public double? StopLossPercentage { get; set; }
    public double? TakeProfitPercentage { get; set; }
    // !Value which moves with the price into the favorable direction and activated instantly
    public double? TrailingStopLossPercentage { get; set; }
    // !Value which moves with the price into the favorable direction and activated after a certain Take-Profit was reached
    // !Makes sure to lock in atleast the desired Take-Profit
    public double? TrailingTakeProfitPercentage { get; set; }
    // TODO position size and risk management
    
    public double? ReturnOnInvestment { get; set; }
}