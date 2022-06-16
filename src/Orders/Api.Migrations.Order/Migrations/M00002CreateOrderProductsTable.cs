using FluentMigrator;

namespace Api.Migrations.Order.Migrations;

[Migration(2)]
public class M00002CreateOrderProductsTable : AutoReversingMigration
{
    public override void Up()
    {
        var schema = "order";
        var ordersTable = "Orders";
        var tableName = "OrderProducts";

        Create.Table(tableName).InSchema(schema)
            .WithColumn("Id").AsInt32().Identity()
            .WithColumn("OrderId").AsInt32().NotNullable()
            .WithColumn("ProductId").AsInt32().NotNullable();
            
        Create.PrimaryKey($"PK_{schema}.{tableName}").OnTable(tableName).WithSchema(schema).Column("Id");  
            
        Create.ForeignKey($"FK_{schema}.{tableName}_{schema}.{ordersTable}_OrderId")
            .FromTable(tableName).InSchema(schema).ForeignColumn("OrderId")
            .ToTable(ordersTable).InSchema(schema).PrimaryColumn("Id");

        Create.Index($"IX_{schema}.{tableName}_OrderId").OnTable(tableName).InSchema(schema).OnColumn("OrderId");
    }
}