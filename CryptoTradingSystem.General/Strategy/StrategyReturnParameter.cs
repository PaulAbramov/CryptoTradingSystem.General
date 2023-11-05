using CryptoTradingSystem.General.Data;

namespace CryptoTradingSystem.General.Strategy;

public struct StrategyReturnParameter
{
	public Enums.TradeType TradeType { get; set; }
	public Enums.TradeStatus TradeStatus { get; set; }
	public Enums.StrategyState StrategyState { get; set; }
	public double? StopLossPercentage { get; set; }
	public double? TakeProfitPercentage { get; set; }

	// !Value which moves with the price into the favorable direction and activated instantly
	public double? TrailingStopLossPercentage { get; set; }

	// !Value which moves with the price into the favorable direction and activated after a certain Take-Profit was reached
	// !Makes sure to lock in atleast the desired Take-Profit
	public double? TrailingTakeProfitPercentage { get; set; }

	public StrategyReturnParameter(
		Enums.TradeType tradeType,
		Enums.TradeStatus tradeStatus,
		double? stopLossPercentage,
		double? takeProfitPercentage,
		double? trailingStopLossPercentage,
		double? trailingTakeProfitPercentage)
	{
		TradeType = tradeType;
		TradeStatus = tradeStatus;
		StopLossPercentage = stopLossPercentage;
		TakeProfitPercentage = takeProfitPercentage;
		TrailingStopLossPercentage = trailingStopLossPercentage;
		TrailingTakeProfitPercentage = trailingTakeProfitPercentage;
	}
}