using System;
using System.Collections.Generic;
using System.Linq;

using CryptoTradingSystem.General.Data;
using CryptoTradingSystem.General.Database.Interfaces;
using CryptoTradingSystem.General.Database.Models;

using Microsoft.EntityFrameworkCore;

using Serilog;

namespace CryptoTradingSystem.General.Database
{
    public class MySQLDatabaseHandler : IDatabaseHandler
    {
        private readonly string _connectionString;

        static MySQLDatabaseHandler()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/General.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public MySQLDatabaseHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// return indicator 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="asset"></param>
        /// <param name="timeFrame"></param>
        /// <param name="indicator"></param>
        /// <param name="firstCloseTime"></param>
        /// <param name="lastCloseTime"></param>
        /// <returns></returns>
        public IEnumerable<T> GetIndicators<T>(
            Enums.Assets asset,
            Enums.TimeFrames timeFrame,
            Enums.Indicators indicator,
            DateTime firstCloseTime = new DateTime(),
            DateTime lastCloseTime = new DateTime())
            where T : Indicator
        {
            Log.Debug("passed parameters " +
                "| asset: {asset} " +
                "| timeframe: {timeFrame} " +
                "| indicator: {indicator} " +
                "| firstclosetime: {lastCloseTime} " +
                "| lastclosetime: {lastCloseTime} ", 
                asset.GetStringValue(), 
                timeFrame.GetStringValue(),
                indicator.GetStringValue(),
                firstCloseTime,
                lastCloseTime);

            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            if (firstCloseTime == DateTime.MinValue)
            {
                firstCloseTime = DateTime.MaxValue;
            }

            TimeSpan parsedTimeFrame;

            switch (timeFrame)
            {
                // Translate timeframe here to do date checks later on
                case Enums.TimeFrames.M5:
                case Enums.TimeFrames.M15:
                    parsedTimeFrame = TimeSpan.FromMinutes(Convert.ToDouble(timeFrame.GetStringValue().Trim('m')));
                    break;
                case Enums.TimeFrames.H1:
                case Enums.TimeFrames.H4:
                    parsedTimeFrame = TimeSpan.FromHours(Convert.ToDouble(timeFrame.GetStringValue().Trim('h')));
                    break;
                case Enums.TimeFrames.D1:
                    parsedTimeFrame = TimeSpan.FromDays(Convert.ToDouble(timeFrame.GetStringValue().Trim('d')));
                    break;
                default:
                    Log.Warning(
                        "{asset} | {timeFrame} | {indicator} | {firstClose} | {lastClose} | timeframe could not be translated",
                        asset.GetStringValue(),
                        timeFrame.GetStringValue(),
                        indicator.GetStringValue(),
                        firstCloseTime,
                        lastCloseTime);

                    return Enumerable.Empty<T>();
            }

            try
            {
                using var contextDB = new CryptoTradingSystemContext(_connectionString);

                var property = typeof(CryptoTradingSystemContext).GetProperty($"{indicator.GetStringValue()}s");

                if (property != null)
                {
                    Log.Debug("{propertyName} does match {indicator}", property.Name, indicator.GetStringValue());

                    var dbset = (DbSet<T>)property.GetValue(contextDB);

                    if (dbset != null)
                    {
                        var indicators = dbset
                            .Where(x => x.AssetName == asset.GetStringValue()
                                        && x.Interval == timeFrame.GetStringValue()
                                        && x.CloseTime <= firstCloseTime
                                        && x.CloseTime >= lastCloseTime)
                            .OrderBy(x => x.CloseTime).AsEnumerable();

                        return indicators.ToList();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(
                    e,
                    "{asset} | {timeFrame} | {indicator} | {firstClose} | {lastClose} | could not get candles from Database", 
                    asset.GetStringValue(), 
                    timeFrame.GetStringValue(), 
                    indicator.GetStringValue(),
                    firstCloseTime,
                    lastCloseTime);
                throw;
            }

            return Enumerable.Empty<T>();
        }
    }
}
