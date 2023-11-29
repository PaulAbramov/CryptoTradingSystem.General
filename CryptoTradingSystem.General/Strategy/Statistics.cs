namespace CryptoTradingSystem.General.Strategy;

public class Statistics
{
	public decimal ProfitLoss { get; set; }
	public decimal ReturnOnInvestment { get; set; }
	public decimal TradesAmount { get; set; }
	public decimal Winrate { get; set; }
	public decimal SharpeRatio { get; set; }
	public decimal SortinoRatio { get; set; }
	public decimal CalmarRatio { get; set; }
	
	// TODO position size and risk management
}