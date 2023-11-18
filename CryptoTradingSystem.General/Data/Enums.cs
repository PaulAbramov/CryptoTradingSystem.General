namespace CryptoTradingSystem.General.Data;

public static class Enums
{
	public enum Assets
	{
		[StringValue("btcusdt")]
		Btcusdt,
		[StringValue("bnbusdt")]
		Bnbusdt
	}

	public enum TimeFrames
	{
		[StringValue("5m")]
		M5,
		[StringValue("15m")]
		M15,
		[StringValue("1h")]
		H1,
		[StringValue("4h")]
		H4,
		[StringValue("1d")]
		D1
	}

	public enum TradeStatus
	{
		[StringValue("None")]
		None,
		[StringValue("Open")]
		Open,
		[StringValue("Closed")]
		Closed
	}

	public enum TradeType
	{
		[StringValue("None")]
		None,
		[StringValue("Buy")]
		Buy,
		[StringValue("Sell")]
		Sell
	}
}