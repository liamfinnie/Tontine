using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ODataTrades.Models.Mapping
{
    public class tradeMap : EntityTypeConfiguration<Trade>
    {
        public tradeMap()
        {
            // Primary Key
            this.HasKey(t => t.trade_reference);

            // Properties
            this.Property(t => t.trade_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.trade_reference)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.client_application_code)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Trade");
            this.Property(t => t.trade_id).HasColumnName("trade_id");
            this.Property(t => t.trade_reference).HasColumnName("trade_reference");
            this.Property(t => t.last_updated).HasColumnName("last_updated");
            this.Property(t => t.client_application_code).HasColumnName("client_application_code");
            this.Property(t => t.trade_markup).HasColumnName("trade_markup");
        }
    }
}
