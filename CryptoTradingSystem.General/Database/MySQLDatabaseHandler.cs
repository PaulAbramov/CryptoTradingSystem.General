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
        public IEnumerable<T> GetIndicators<T>(
            Enums.Assets _asset,
            Enums.TimeFrames _timeFrame,
            Enums.Indicators _indicator,
            DateTime _firstCloseTime = new DateTime(),
            DateTime _lastCloseTime = new DateTime())
            where T : Indicator
        {
            Log.Debug("passed parameters " +
                "| asset: {asset} " +
                "| timeframe: {timeFrame} " +
                "| indicator: {indicator} " +
                "| firstclosetime: {lastCloseTime} " +
                "| lastclosetime: {lastCloseTime} ", 
                _asset.GetStringValue(), 
                _timeFrame.GetStringValue(),
                _indicator.GetStringValue(),
                _firstCloseTime,
                _lastCloseTime);

            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            if (_firstCloseTime == DateTime.MinValue)
            {
                _firstCloseTime = DateTime.MaxValue;
            }

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
                Log.Warning(
                    "{asset} | {timeFrame} | {indicator} | {firstClose} | {lastClose} | timeframe could not be translated",
                    _asset.GetStringValue(),
                    _timeFrame.GetStringValue(),
                    _indicator.GetStringValue(),
                    _firstCloseTime,
                    _lastCloseTime);

                return Enumerable.Empty<T>();
            }

            try
            {
                using var contextDB = new CryptoTradingSystemContext(connectionString);

                var property = typeof(CryptoTradingSystemContext).GetProperty($"{_indicator.GetStringValue()}s");

                Log.Debug("{propertyName} does match {indicator}", property.Name, _indicator.GetStringValue());

                var dbset = (DbSet<T>)property.GetValue(contextDB);

                var indicators = dbset
                    .Where(x => x.AssetName == _asset.GetStringValue()
                        && x.Interval == _timeFrame.GetStringValue()
                        && x.CloseTime <= _firstCloseTime
                        && x.CloseTime >= _lastCloseTime)
                    .OrderBy(x => x.CloseTime).AsEnumerable();

                return indicators.ToList();
            }
            catch (Exception e)
            {
                Log.Error(
                    e,
                    "{asset} | {timeFrame} | {indicator} | {firstClose} | {lastClose} | could not get candles from Database", 
                    _asset.GetStringValue(), 
                    _timeFrame.GetStringValue(), 
                    _indicator.GetStringValue(),
                    _firstCloseTime,
                    _lastCloseTime);
                throw;
            }
        }
    }
}
