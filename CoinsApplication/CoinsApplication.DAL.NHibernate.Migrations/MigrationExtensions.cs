using FluentMigrator.Builders.Create.Table;

namespace CoinsApplication.DAL.NHibernate.Migrations
{
    public static class MigrationExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("Id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
                .Identity();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithTimeStamps(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("ModifiedAt").AsDateTime().NotNullable();
        }
    }
}
