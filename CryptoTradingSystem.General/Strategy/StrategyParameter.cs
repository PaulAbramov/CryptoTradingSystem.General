﻿using System;
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
        public Enums.Assets AssetToBuy { get; private set; }
        public DateTime? TimeFrameStart { get; private set; }
        public DateTime? TimeFrameEnd { get; private set; }

        public StrategyParameter(
            List<Tuple<Enums.TimeFrames, Enums.Assets, Type>> assets, 
            Enums.Assets assetToBuy,
            DateTime? timeFrameStart,
            DateTime? timeFrameEnd)
        {
            Assets = assets;
            AssetToBuy = assetToBuy;
            TimeFrameStart = timeFrameStart;
            TimeFrameEnd = timeFrameEnd;
        }
    }
}
