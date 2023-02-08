using System;
using System.Collections.Generic;
using CryptoTradingSystem.General.Data;
using CryptoTradingSystem.General.Database.Models;

namespace CryptoTradingSystem.General.Database.Interfaces
{
    public interface IDatabaseHandler
    {
        IEnumerable<T> GetIndicators<T>(
            Enums.Assets _asset, 
            Enums.TimeFrames _timeFrame, 
            Enums.Indicators _indicator, 
            DateTime _firstCloseTime = new DateTime(), 
            DateTime _lastCloseTime = new DateTime()) 
            where T : Indicator;
    }
}
