using System;
using System.Collections.Generic;
using CryptoTradingSystem.General.Data;
using CryptoTradingSystem.General.Database.Models;

namespace CryptoTradingSystem.General.Database.Interfaces
{
    public interface IDatabaseHandler
    {
        List<T> GetIndicators<T>(Enums.Assets _asset, Enums.TimeFrames _timeFrame, Enums.Indicators _indicator, DateTime _lastCloseTime = new DateTime(), int _amount = 500) where T : Indicator;
    }
}
