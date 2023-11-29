namespace CryptoTradingSystem.General.Strategy;

public class StrategyStatistics : Statistics
{
	public decimal InitialInvestment { get; set; }
	public decimal AmountOfWonTrades { get; set; }
	public decimal AmountOfLostTrades { get; set; }
	public decimal LostTradesPercentage { get; set; }
	public decimal RiskRewardRatio { get; set; }
	
	public double StopLossPercentage { get; set; }

	public double TakeProfitPercentage { get; set; }

	// !Value which moves with the price into the favorable direction and activated instantly
	public double TrailingStopLossPercentage { get; set; }

	// !Value which moves with the price into the favorable direction and activated after a certain Take-Profit was reached
	// !Makes sure to lock in atleast the desired Take-Profit
	public double TrailingTakeProfitPercentage { get; set; }
	
	public decimal Volatility { get; set; }
}