﻿using System;
using System.Linq;

namespace CryptoTradingSystem.General.Data;

public static class EnumsExtension
{
	/// <summary>
	///   Will get the string value for a given enums value, this will
	///   only work if you assign the StringValue attribute to
	///   the items in your enum.
	/// </summary>
	/// <param name="value"></param>
	public static string? GetStringValue(this Enum value)
	{
		var type = value.GetType();

		var fieldInfo = type.GetField(value.ToString());

		// Get the stringvalue attributes
		var attribs = fieldInfo?.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

		//return attribs is not null && attribs.Length > 0 ? attribs[0].StringValue : null;
		// Return the first if there was a match.
		return attribs is
		{
			Length: > 0
		}
			? attribs[0]
				.StringValue
			: null;
	}

	public static T Next<T>(this T v) where T : struct => Enum.GetValues(v.GetType())
		.Cast<T>()
		.Concat(
			new[]
			{
				default(T)
			})
		.SkipWhile(e => !v.Equals(e))
		.Skip(1)
		.First();

	public static T Previous<T>(this T v) where T : struct => Enum.GetValues(v.GetType())
		.Cast<T>()
		.Concat(
			new[]
			{
				default(T)
			})
		.Reverse()
		.SkipWhile(e => !v.Equals(e))
		.Skip(1)
		.First();
}