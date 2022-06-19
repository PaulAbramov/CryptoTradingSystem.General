using CryptoTradingSystem.General.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingSystem.General.Database
{
    public class CryptoTradingSystemContext : DbContext
    {
        private readonly string connectionString;

        public DbSet<Asset> Assets { get; set; }
        public DbSet<EMA> EMAs { get; set; }
        public DbSet<SMA> SMAs { get; set; }
        public DbSet<ATR> ATRs { get; set; }

        public CryptoTradingSystemContext(string _connectionString)
        {
            connectionString = _connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder _modelBuilder)
        {
            base.OnModelCreating(_modelBuilder);

            _modelBuilder.Entity<Asset>(_entity =>
            {
                _entity.Property(_e => _e.AssetName).HasMaxLength(10).IsRequired();
                _entity.Property(_e => _e.Interval).HasMaxLength(10).IsRequired();

                _entity.Property(_e => _e.CandleOpen).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.CandleHigh).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.CandleLow).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.CandleClose).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.QuoteAssetVolume).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.Volume).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.TakerBuyBaseAssetVolume).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.TakerBuyQuoteAssetVolume).HasColumnType("decimal(28,8)");

                _entity.Property(_e => _e.OpenTime).HasColumnType("datetime(6)");
                _entity.Property(_e => _e.CloseTime).HasColumnType("datetime(6)");

                _entity.HasKey(_e => new { _e.AssetName, _e.Interval, _e.OpenTime, _e.CloseTime });
                _entity.Ignore(_e => _e.Ema);
                _entity.Ignore(_e => _e.Sma);
                _entity.Ignore(_e => _e.Atr);

                _entity.HasOne(_e => _e.Ema).WithOne(_ema => _ema.Asset)
                    .HasForeignKey<EMA>(_ema => new { _ema.AssetName, _ema.Interval, _ema.OpenTime, _ema.CloseTime });

                _entity.HasOne(_e => _e.Sma).WithOne(_sma => _sma.Asset)
                    .HasForeignKey<SMA>(_sma => new { _sma.AssetName, _sma.Interval, _sma.OpenTime, _sma.CloseTime });

                _entity.HasOne(_e => _e.Atr).WithOne(_atr => _atr.Asset)
                    .HasForeignKey<ATR>(_atr => new { _atr.AssetName, _atr.Interval, _atr.OpenTime, _atr.CloseTime });
            });

            _modelBuilder.Entity<EMA>(_entity =>
            {
                _entity.Property(_e => _e.EMA5).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.EMA9).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.EMA12).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.EMA20).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.EMA26).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.EMA50).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.EMA75).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.EMA200).HasColumnType("decimal(28,8)");

                _entity.HasKey(_e => new { _e.AssetName, _e.Interval, _e.OpenTime, _e.CloseTime });
            });

            _modelBuilder.Entity<SMA>(_entity =>
            {
                _entity.Property(_e => _e.SMA5).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.SMA9).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.SMA12).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.SMA20).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.SMA26).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.SMA50).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.SMA75).HasColumnType("decimal(28,8)");
                _entity.Property(_e => _e.SMA200).HasColumnType("decimal(28,8)");

                _entity.HasKey(_e => new { _e.AssetName, _e.Interval, _e.OpenTime, _e.CloseTime });
            });

            _modelBuilder.Entity<ATR>(_entity =>
            {
                _entity.Property(_e => _e.ATR14).HasColumnType("decimal(28,8)");

                _entity.HasKey(_e => new { _e.AssetName, _e.Interval, _e.OpenTime, _e.CloseTime });
            });
        }
    }
}
