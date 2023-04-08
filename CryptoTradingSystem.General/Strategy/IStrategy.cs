using System.Collections.Generic;
using CryptoTradingSystem.General.Database.Models;

namespace CryptoTradingSystem.General.Strategy
{
    public interface IStrategy
    {
        StrategyParameter SetupStrategyParameter();
        string ExecuteStrategy(List<List<Indicator>> indicators);
    }
}
