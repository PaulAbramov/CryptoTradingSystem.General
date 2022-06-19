using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                    exceptions.Add(ex);
                }
            }
            throw new AggregateException(exceptions);
        }
    }
}
