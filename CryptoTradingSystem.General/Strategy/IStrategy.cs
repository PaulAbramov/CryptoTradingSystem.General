using CryptoTradingSystem.General.Data;
using CryptoTradingSystem.General.Database.Models;
using System.Collections.Generic;

namespace CryptoTradingSystem.General.Strategy;

public interface IStrategy
{
	StrategyParameter SetupStrategyParameter();
	StrategyReturnParameter ExecuteStrategy(List<Indicator> indicators, decimal price);
}