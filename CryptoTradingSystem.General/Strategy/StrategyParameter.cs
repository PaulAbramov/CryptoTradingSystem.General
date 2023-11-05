using CryptoTradingSystem.General.Data;
using System;
using System.Collections.Generic;

namespace CryptoTradingSystem.General.Strategy;

/// <summary>
///   Every Strategy has to fill this.
///   Will be returned to the Backtester to get all Data
/// </summary>
public struct StrategyParameter
{
	public List<Tuple<Enums.TimeFrames, Enums.Assets, Type>> Assets { get; private set; }
	public Enums.Assets AssetToBuy { get; private set; }
	public DateTime? TimeFrameStart { get; set; }
	public DateTime? TimeFrameEnd { get; set; }
	public StrategyApprovementStatistics StrategyApprovementStatistics { get; set; }

	public StrategyParameter(
		List<Tuple<Enums.TimeFrames, Enums.Assets, Type>> assets,
		Enums.Assets assetToBuy,
		DateTime? timeFrameStart,
		DateTime? timeFrameEnd,
		StrategyApprovementStatistics strategyApprovementStatistics)
	{
		Assets = assets;
		AssetToBuy = assetToBuy;
		TimeFrameStart = timeFrameStart;
		TimeFrameEnd = timeFrameEnd;
		StrategyApprovementStatistics = strategyApprovementStatistics;
	}
}