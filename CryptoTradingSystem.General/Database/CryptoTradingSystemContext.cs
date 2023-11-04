using CryptoTradingSystem.General.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingSystem.General.Database
{
    public class CryptoTradingSystemContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Asset>? Assets { get; set; }
        public DbSet<EMA>? EMAs { get; set; }
        public DbSet<SMA>? SMAs { get; set; }
        public DbSet<ATR>? ATRs { get; set; }
        public DbSet<AssetAdditionalInformation> AssetAdditionalInformations { get; set; }

        public CryptoTradingSystemContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.AssetName).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Interval).HasMaxLength(10).IsRequired();

                entity.Property(e => e.CandleOpen).HasColumnType("decimal(28,8)");
                entity.Property(e => e.CandleHigh).HasColumnType("decimal(28,8)");
                entity.Property(e => e.CandleLow).HasColumnType("decimal(28,8)");
                entity.Property(e => e.CandleClose).HasColumnType("decimal(28,8)");
                entity.Property(e => e.QuoteAssetVolume).HasColumnType("decimal(28,8)");
                entity.Property(e => e.Volume).HasColumnType("decimal(28,8)");
                entity.Property(e => e.TakerBuyBaseAssetVolume).HasColumnType("decimal(28,8)");
                entity.Property(e => e.TakerBuyQuoteAssetVolume).HasColumnType("decimal(28,8)");

                entity.Property(e => e.OpenTime).HasColumnType("datetime(6)");
                entity.Property(e => e.CloseTime).HasColumnType("datetime(6)");

                entity.HasKey(e => new { e.AssetName, e.Interval, e.OpenTime, e.CloseTime });
                entity.Ignore(e => e.Ema);
                entity.Ignore(e => e.Sma);
                entity.Ignore(e => e.Atr);
                entity.Ignore(e => e.AdditionalInformation);

                entity.HasOne(e => e.Ema).WithOne(ema => ema.Asset)
                    .HasForeignKey<EMA>(ema => new { ema.AssetName, ema.Interval, ema.OpenTime, ema.CloseTime });

                entity.HasOne(e => e.Sma).WithOne(sma => sma.Asset)
                    .HasForeignKey<SMA>(sma => new { sma.AssetName, sma.Interval, sma.OpenTime, sma.CloseTime });

                entity.HasOne(e => e.Atr).WithOne(atr => atr.Asset)
                    .HasForeignKey<ATR>(atr => new { atr.AssetName, atr.Interval, atr.OpenTime, atr.CloseTime });

                entity.HasOne(e => e.AdditionalInformation).WithOne(additionalInfo => additionalInfo.Asset)
                    .HasForeignKey<AssetAdditionalInformation>(additionalInfo => new { additionalInfo.AssetName, additionalInfo.Interval, additionalInfo.OpenTime, additionalInfo.CloseTime });
            });

            modelBuilder.Entity<EMA>(entity =>
            {
                entity.Property(e => e.EMA5).HasColumnType("decimal(28,8)");
                entity.Property(e => e.EMA9).HasColumnType("decimal(28,8)");
                entity.Property(e => e.EMA12).HasColumnType("decimal(28,8)");
                entity.Property(e => e.EMA20).HasColumnType("decimal(28,8)");
                entity.Property(e => e.EMA26).HasColumnType("decimal(28,8)");
                entity.Property(e => e.EMA50).HasColumnType("decimal(28,8)");
                entity.Property(e => e.EMA75).HasColumnType("decimal(28,8)");
                entity.Property(e => e.EMA200).HasColumnType("decimal(28,8)");

                entity.HasKey(e => new { e.AssetName, e.Interval, e.OpenTime, e.CloseTime });
            });

            modelBuilder.Entity<SMA>(entity =>
            {
                entity.Property(e => e.SMA5).HasColumnType("decimal(28,8)");
                entity.Property(e => e.SMA9).HasColumnType("decimal(28,8)");
                entity.Property(e => e.SMA12).HasColumnType("decimal(28,8)");
                entity.Property(e => e.SMA20).HasColumnType("decimal(28,8)");
                entity.Property(e => e.SMA26).HasColumnType("decimal(28,8)");
                entity.Property(e => e.SMA50).HasColumnType("decimal(28,8)");
                entity.Property(e => e.SMA75).HasColumnType("decimal(28,8)");
                entity.Property(e => e.SMA200).HasColumnType("decimal(28,8)");

                entity.HasKey(e => new { e.AssetName, e.Interval, e.OpenTime, e.CloseTime });
            });

            modelBuilder.Entity<ATR>(entity =>
            {
                entity.Property(e => e.ATR14).HasColumnType("decimal(28,8)");

                entity.HasKey(e => new { e.AssetName, e.Interval, e.OpenTime, e.CloseTime });
            });

            modelBuilder.Entity<AssetAdditionalInformation>(entity =>
            {
                entity.Property(e => e.ReturnToLastCandle).HasColumnType("decimal(28,8)");
                entity.Property(e => e.ReturnToLastCandleInPercentage).HasColumnType("decimal(28,8)");

                entity.HasKey(e => new { e.AssetName, e.Interval, e.OpenTime, e.CloseTime });
            });
        }
    }
}
