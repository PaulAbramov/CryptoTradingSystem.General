using System;
using System.Collections.Generic;

using CryptoTradingSystem.General.Data;

namespace CryptoTradingSystem.General.Strategy
{
    /// <summary>
    /// Every Strategy has to fill this.
    /// Will be returned to the Backtester to get all Data
    /// </summary>
    public struct StrategyParameter
    {
        public List<Tuple<Enums.TimeFrames, Enums.Assets, Type>> Assets { get; private set; }

        public DateTime? TimeFrameStart { get; private set; }
        public DateTime? TimeFrameEnd { get; private set; }

        public StrategyParameter(
            List<Tuple<Enums.TimeFrames, Enums.Assets, Type>> assets, 
            DateTime? timeFrameStart,
            DateTime? timeFrameEnd)
        {
            Assets = assets;
            TimeFrameStart = timeFrameStart;
            TimeFrameEnd = timeFrameEnd;
        }
    }
}
