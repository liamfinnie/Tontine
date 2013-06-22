using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ODataTrades.Models.Mapping
{
    public class currencyMap : EntityTypeConfiguration<Currency>
    {
        public currencyMap()
        {
            // Primary Key
            this.HasKey(t => t.currency_code_number);

            // Properties
            this.Property(t => t.currency_code_number)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.currency_code)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.currency_name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Currency");
            this.Property(t => t.currency_code_number).HasColumnName("currency_code_number");
            this.Property(t => t.currency_code).HasColumnName("currency_code");
            this.Property(t => t.currency_name).HasColumnName("currency_name");
        }
    }
}
