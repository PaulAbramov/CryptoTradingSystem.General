using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CryptoTradingSystem.General.Helper;

public static class Retry
{
	public static void Do(Action action, TimeSpan retryInterval, int maxAttemptCount = 10) => Do<object>(
		() =>
		{
			action();
			return null;
		},
		retryInterval,
		maxAttemptCount);

	public static T? Do<T>(Func<T?> action, TimeSpan retryInterval, int maxAttemptCount = 10)
	{
		var exceptions = new List<Exception>();

		for (var attempted = 0; attempted < maxAttemptCount; attempted++)
		{
			try
			{
				if (attempted > 0)
				{
					Task.Delay(retryInterval)
						.GetAwaiter()
						.GetResult();
				}

				return action();
			}
			catch (Exception ex)
			{
				if (attempted == maxAttemptCount - 1)
				{
					Log.Warning(
						ex,
						"Run {attempt}/{maxAttempts} failed. action: {action}.",
						attempted + 1,
						maxAttemptCount,
						action.GetMethodInfo()
							.Name[1..action.GetMethodInfo()
								.Name.IndexOf(
									">",
									StringComparison.Ordinal)]);
				}
				else
				{
					Log.Debug(
						ex,
						"Run {attempt}/{maxAttempts} failed. action: {action}.",
						attempted + 1,
						maxAttemptCount,
						action.GetMethodInfo()
							.Name[1..action.GetMethodInfo()
								.Name.IndexOf(
									">",
									StringComparison.Ordinal)]);
				}

				exceptions.Add(ex);
			}
		}

		throw new AggregateException(exceptions);
	}
}