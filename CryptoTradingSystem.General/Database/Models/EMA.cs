namespace CryptoTradingSystem.General.Database.Models;

public class EMA : Indicator
{
	public decimal? EMA5 { get; set; }
	public decimal? EMA9 { get; set; }
	public decimal? EMA12 { get; set; }
	public decimal? EMA20 { get; set; }
	public decimal? EMA26 { get; set; }
	public decimal? EMA50 { get; set; }
	public decimal? EMA75 { get; set; }
	public decimal? EMA200 { get; set; }
}