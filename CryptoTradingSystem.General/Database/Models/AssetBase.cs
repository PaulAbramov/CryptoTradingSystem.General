using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTradingSystem.General.Database.Models;

public class AssetBase
{
	public string? AssetName { get; set; }
	public string? Interval { get; set; }
	public DateTime OpenTime { get; set; }
	public DateTime CloseTime { get; set; }

	[NotMapped]
	public Asset? Asset { get; set; }
}