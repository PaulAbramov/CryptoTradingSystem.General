using System;
using System.Collections.Generic;
using CryptoTradingSystem.General.Data;
using CryptoTradingSystem.General.Database.Models;

namespace CryptoTradingSystem.General.Database.Interfaces
{
    public interface IDatabaseHandler
    {
        IEnumerable<T> GetIndicators<T>(
            Enums.Assets asset, 
            Enums.TimeFrames timeFrame, 
            Enums.Indicators indicator, 
            DateTime firstCloseTime = new DateTime(), 
            DateTime lastCloseTime = new DateTime()) 
            where T : Indicator;
    }
}
