namespace CryptoTradingSystem.General.Strategy
{
    public interface IStrategy
    {
        string ExecuteStrategy(string _connectionString, string _logFile);
    }
}
