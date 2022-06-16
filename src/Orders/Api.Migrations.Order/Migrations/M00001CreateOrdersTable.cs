using FluentMigrator;

namespace Api.Migrations.Order.Migrations;

[Migration(1)]
public class M00001CreateOrdersTable : AutoReversingMigration
{
    public override void Up()
    {
        var schema = "order";
        var tableName = "Orders";

        Create.Table(tableName).InSchema(schema)
            .WithColumn("Id").AsInt32().Identity()
            .WithColumn("CustomerId").AsInt32().NotNullable()
            .WithColumn("OrderDateTime").AsDateTime().NotNullable();


        Create.PrimaryKey($"PK_{schema}.{tableName}").OnTable(tableName).WithSchema(schema).Column("Id");            
    }
}