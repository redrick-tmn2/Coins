using System.Data.Entity;
using System.Data.SQLite;
using CoinsApplication.DAL.Entities;

namespace CoinsApplication.DAL
{
    public class CoinsContext : DbContext
    {
        public CoinsContext()
            : base(new SQLiteConnection
            {
                ConnectionString = new SQLiteConnectionStringBuilder
                {
                    DataSource = DatabaseFileNameFactory.GetDatabaseFileName(),
                    ForeignKeys = true
                }.ConnectionString
            }, true)
        {
            
        }
        
        public DbSet<Coin> Coins { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Country> Countries { get; set; }
    }
}
