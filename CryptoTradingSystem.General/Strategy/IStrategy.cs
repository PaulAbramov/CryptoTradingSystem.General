namespace CryptoTradingSystem.General.Strategy
{
    public interface IStrategy
    {
        StrategyParameter SetupStrategyParameter();
        string ExecuteStrategy();
    }
}
