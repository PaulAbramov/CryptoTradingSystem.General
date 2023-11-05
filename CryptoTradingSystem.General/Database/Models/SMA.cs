namespace CryptoTradingSystem.General.Database.Models;

// ReSharper disable once InconsistentNaming
public class SMA : Indicator
{
	public decimal? SMA5 { get; set; }
	public decimal? SMA9 { get; set; }
	public decimal? SMA12 { get; set; }
	public decimal? SMA20 { get; set; }
	public decimal? SMA26 { get; set; }
	public decimal? SMA50 { get; set; }
	public decimal? SMA75 { get; set; }
	public decimal? SMA200 { get; set; }
}