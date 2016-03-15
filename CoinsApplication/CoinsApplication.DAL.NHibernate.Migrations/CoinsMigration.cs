using FluentMigrator;

namespace CoinsApplication.DAL.NHibernate.Migrations
{
    [Migration(1)]
    public class CoinsMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Currency")
                .WithIdColumn()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Code").AsString().NotNullable();

            Create.Table("Country")
                .WithIdColumn()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Flag").AsBinary().Nullable();

            Create.Table("Coin")
                .WithIdColumn()
                .WithColumn("Title").AsString()
                .WithColumn("Year").AsInt32().NotNullable()
                .WithColumn("Image").AsBinary()
                .WithColumn("CurrencyId").AsInt32().ForeignKey("Currency", "Id")
                .WithColumn("CountryId").AsInt32().ForeignKey("Country", "Id");

            Insert.IntoTable("Country").Row(new
            {
                Name = "Russia"
            });

            Insert.IntoTable("Currency").Row(new
            {
                Name = "Rouble",
                Code = "RUB"
            });
        }

        public override void Down()
        {
            Delete.Table("Currency");
            Delete.Table("Country");
            Delete.Table("Coin");
        }
    }
}
