using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ODataTrades.Models.Mapping;

namespace ODataTrades.Models
{
    public partial class HermesContext : DbContext
    {
        static HermesContext()
        {
            Database.SetInitializer<HermesContext>(null);
        }

        public HermesContext()
            : base("Name=HermesContext")
        {
        }

        public DbSet<Country> countries { get; set; }
        public DbSet<Currency> currencies { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Trade> trades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new countryMap());
            modelBuilder.Configurations.Add(new currencyMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new tradeMap());
        }
    }
}
