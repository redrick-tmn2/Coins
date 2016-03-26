using FluentMigrator;

namespace CoinsApplication.DAL.NHibernate.Migrations
{
    [Migration(250320161113)]
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
                .WithColumn("Flag").AsBinary()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("Coin")
                .WithIdColumn()
                .WithColumn("Title").AsString()
                .WithColumn("Year").AsInt32().NotNullable()
                .WithColumn("CurrencyId").AsInt32().ForeignKey("Currency", "Id")
                .WithColumn("CountryId").AsInt32().ForeignKey("Country", "Id");

            Create.Table("Image")
                .WithIdColumn()
                .WithColumn("Content").AsBinary()
                .WithColumn("CoinId").AsInt32().Nullable().ForeignKey("Coin", "Id");
        }

        public override void Down()
        {
            Delete.Table("Currency");
            Delete.Table("Country");
            Delete.Table("Image");
            Delete.Table("Coin");
        }
    }
}
