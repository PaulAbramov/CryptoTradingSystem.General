using System.Collections.Generic;
using CryptoTradingSystem.General.Data;
using CryptoTradingSystem.General.Database.Models;

namespace CryptoTradingSystem.General.Strategy
{
    public interface IStrategy
    {
        StrategyStatistics Statistics { get; set; }
        
        StrategyParameter SetupStrategyParameter();
        StrategyReturnParameter ExecuteStrategy(List<Indicator> indicators, decimal price);

        void CalculateStatistics(decimal candleClose, Enums.TradeType tradeType);
    }
}
