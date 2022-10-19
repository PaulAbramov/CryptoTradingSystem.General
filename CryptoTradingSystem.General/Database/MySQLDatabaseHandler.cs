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
        private string connectionString;

        static MySQLDatabaseHandler()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/General.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public MySQLDatabaseHandler(string _connectionString)
        {
            connectionString = _connectionString;
        }

        /// <summary>
        /// return indicator 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_asset"></param>
        /// <param name="_timeFrame"></param>
        /// <param name="_indicator"></param>
        /// <param name="_lastCloseTime"></param>
        /// <param name="_amount"></param>
        /// <returns></returns>
        public List<T> GetIndicators<T>(
            Enums.Assets _asset,
            Enums.TimeFrames _timeFrame,
            Enums.Indicators _indicator,
            DateTime _lastCloseTime = new DateTime(),
            int _amount = 500)
            where T : Indicator
        {
            var returnList = new List<T>();
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            TimeSpan timeFrame;

            // Translate timeframe here to do date checks later on
            if (_timeFrame is Enums.TimeFrames.M5 || _timeFrame is Enums.TimeFrames.M15)
            {
                timeFrame = TimeSpan.FromMinutes(Convert.ToDouble(_timeFrame.GetStringValue().Trim('m')));
            }
            else if (_timeFrame is Enums.TimeFrames.H1 || _timeFrame is Enums.TimeFrames.H4)
            {
                timeFrame = TimeSpan.FromHours(Convert.ToDouble(_timeFrame.GetStringValue().Trim('h')));
            }
            else if (_timeFrame is Enums.TimeFrames.D1)
            {
                timeFrame = TimeSpan.FromDays(Convert.ToDouble(_timeFrame.GetStringValue().Trim('d')));
            }
            else
            {
                Log.Warning("{asset} | {timeFrame} | {indicator} | {lastClose} | timeframe could not be translated",
                    _asset.GetStringValue(),
                    _timeFrame.GetStringValue(),
                    _indicator.GetStringValue(),
                    _lastCloseTime);

                return returnList;
            }

            try
            {
                using var contextDB = new CryptoTradingSystemContext(connectionString);

                var properties = typeof(CryptoTradingSystemContext).GetProperties();

                foreach (var property in properties)
                {
                    if (!property.Name.Equals($"{_indicator.GetStringValue()}s"))
                    {
                        continue;
                    }

                    var dbset = (DbSet<T>)property.GetValue(contextDB);

                    var indicatorsToCheck = dbset
                        .Where(x => x.AssetName == _asset.GetStringValue() && x.Interval == _timeFrame.GetStringValue() && x.CloseTime >= _lastCloseTime)
                        .OrderBy(x => x.CloseTime)
                        .Take(_amount);

                    DateTime previousCandle = DateTime.MinValue;
                    foreach (var indicator in indicatorsToCheck)
                    {
                        // If we do have a previous candle, check if the difference from the current to the previous one is above the timeframe we are looking for
                        // If so, then it is a gap and then check if the gap is towards the current Year and Month, this is where we can be sure, that the data is not complete yet.
                        // Break here then, so we can do a new request and get the new incoming data
                        if (previousCandle != DateTime.MinValue)
                        {
                            bool gap = (indicator.CloseTime - previousCandle) > timeFrame;
                            if (gap 
                                && indicator.CloseTime.Year == currentYear 
                                && indicator.CloseTime.Month == currentMonth 
                                && (previousCandle.Year != currentYear || previousCandle.Month != currentMonth))
                            {
                                break;
                            }
                        }
                        else
                        {
                            // Do not allow to calculate indicators if we do not have data from the past
                            if (indicator.CloseTime.Year == currentYear 
                                && (indicator.CloseTime.Month == currentMonth || indicator.CloseTime.Month == currentMonth - 1))
                            {
                                break;
                            }
                        }

                        returnList.Add(indicator);
                    }

                    break;
                }
            }
            catch (Exception e)
            {
                Log.Error("{asset} | {timeFrame} | {indicator} | {lastClose} | could not get candles from Database", 
                    _asset.GetStringValue(), 
                    _timeFrame.GetStringValue(), 
                    _indicator.GetStringValue(), 
                    _lastCloseTime);
                throw;
            }

            return returnList;
        }
    }
}
