﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using Serilog;

namespace CryptoTradingSystem.General.Helper
{
    public static class Retry
    {
        public static void Do(Action _action, TimeSpan _retryInterval, int _maxAttemptCount = 10)
        {
            Do<object>(() =>
            {
                _action();
                return null;
            }, _retryInterval, _maxAttemptCount);
        }

        public static T Do<T>(Func<T> _action, TimeSpan _retryInterval, int _maxAttemptCount = 10)
        {
            var exceptions = new List<Exception>();

            for (int attempted = 0; attempted < _maxAttemptCount; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        Task.Delay(_retryInterval).GetAwaiter().GetResult();
                    }
                    return _action();
                }
                catch (Exception ex)
                {
                    if(attempted == _maxAttemptCount - 1)
                    {
                        Log.Warning(
                            ex,
                            "Run {attempt}/{maxAttempts} failed. action: {action}.", 
                            attempted + 1, 
                            _maxAttemptCount, 
                            _action.GetMethodInfo().Name[1.._action.GetMethodInfo().Name.IndexOf(">")]);
                    }
                    else
                    {
                        Log.Debug(
                            ex, 
                            "Run {attempt}/{maxAttempts} failed. action: {action}.",
                            attempted + 1,
                            _maxAttemptCount,
                            _action.GetMethodInfo().Name[1.._action.GetMethodInfo().Name.IndexOf(">")]);
                    }

                    exceptions.Add(ex);
                }
            }
            throw new AggregateException(exceptions);
        }
    }
}
