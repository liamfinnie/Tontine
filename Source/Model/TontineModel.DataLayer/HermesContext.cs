using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using TontineModel.DomainClasses;

namespace TontineModel.DataLayer
{
    public class HermesContext : DbContext
    {
        public HermesContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public DbSet<StagingTrade> StagingTrades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StagingTrade>().ToTable("staging_trade");
            modelBuilder.Entity<StagingTrade>().Property(c => c.StagingTradeId).HasColumnName("staging_trade_id");
            modelBuilder.Entity<StagingTrade>().Property(c => c.StagingTradeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<StagingTrade>().Property(c => c.TradeReference).HasColumnName("trade_reference");
            modelBuilder.Entity<StagingTrade>().Property(c => c.TradeRepresentation).HasColumnName("trade_representation");
            modelBuilder.Entity<StagingTrade>().Property(c => c.TradeRepresentation).HasColumnType("XML");
            modelBuilder.Entity<StagingTrade>().Property(c => c.SourceApplicationCode).HasColumnName("source_application_code");
            modelBuilder.Entity<StagingTrade>().Property(c => c.CreatedDate).HasColumnName("created_date");
            modelBuilder.Entity<StagingTrade>().Property(c => c.CreatedDate).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
