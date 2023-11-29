using System;

namespace CryptoTradingSystem.General.Strategy;

public class StrategyApprovementStatistics : Statistics
{
	public TimeSpan MinimalValidationDuration { get; set; }
	public decimal MaxAllowedDrawdown { get; set; }
}