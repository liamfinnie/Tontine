using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ODataTrades.Models.Mapping
{
    public class countryMap : EntityTypeConfiguration<Country>
    {
        public countryMap()
        {
            // Primary Key
            this.HasKey(t => t.country_numeric_code);

            // Properties
            this.Property(t => t.country_numeric_code)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.country_alpha2_code)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.country_alpha3_code)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.country_name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Country");
            this.Property(t => t.country_numeric_code).HasColumnName("country_numeric_code");
            this.Property(t => t.country_alpha2_code).HasColumnName("country_alpha2_code");
            this.Property(t => t.country_alpha3_code).HasColumnName("country_alpha3_code");
            this.Property(t => t.country_name).HasColumnName("country_name");
        }
    }
}
