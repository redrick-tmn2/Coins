using FluentMigrator;

namespace CoinsApplication.DAL.NHibernate.Migrations
{
    [Migration(201604072154)]
    public class AdditionalFieldsMigration : Migration
    {
        public override void Up()
        {
            Alter.Column("Year").OnTable("Coin").AsInt32().Nullable();
            Create.Column("Diameter").OnTable("Coin").AsInt32().Nullable();
            Create.Column("Thickness").OnTable("Coin").AsInt32().Nullable();
        }

        public override void Down()
        {
            Alter.Column("Year").OnTable("Coin").AsInt32().NotNullable();
            Delete.Column("Diameter").FromTable("Coin");
            Delete.Column("Thickness").FromTable("Coin");
        }
    }
}
