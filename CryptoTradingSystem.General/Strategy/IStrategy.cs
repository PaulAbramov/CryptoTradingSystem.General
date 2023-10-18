using System.Collections.Generic;
using CryptoTradingSystem.General.Data;
using CryptoTradingSystem.General.Database.Models;

namespace CryptoTradingSystem.General.Strategy
{
    public interface IStrategy
    {
        StrategyParameter SetupStrategyParameter();
        StrategyReturnParameter ExecuteStrategy(List<Indicator> indicators, Enums.TradeStatus status);
    }
}
