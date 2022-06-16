using FluentMigrator;

namespace Api.Migrations.Order.Migrations;

[Migration(3)]
public class M00003CreateOrderViewsTable : AutoReversingMigration
{
    public override void Up()
    {
        var schema = "order";
        var ordersTable = "Orders";
        var tableName = "OrderViews";

        Create.Table(tableName).InSchema(schema)
            .WithColumn("Id").AsInt32().Identity()
            .WithColumn("OrderId").AsInt32().NotNullable()
            .WithColumn("OrderDateTime").AsDateTime().NotNullable()
            .WithColumn("CustomerId").AsInt32().NotNullable()
            .WithColumn("CustomerName").AsString(512).NotNullable()
            .WithColumn("CustomerSurname").AsString(512).NotNullable()
            .WithColumn("ProductId").AsInt32().NotNullable()
            .WithColumn("ProductName").AsString(512).NotNullable()
            .WithColumn("ProductPrice").AsDouble().NotNullable()
            .WithColumn("ProductDetails").AsString(int.MaxValue).Nullable();
            
        Create.PrimaryKey($"PK_{schema}.{tableName}").OnTable(tableName).WithSchema(schema).Column("Id");  
            
        Create.ForeignKey($"FK_{schema}.{tableName}_{schema}.{ordersTable}_OrderId")
            .FromTable(tableName).InSchema(schema).ForeignColumn("OrderId")
            .ToTable(ordersTable).InSchema(schema).PrimaryColumn("Id");

        Create.Index($"IX_{schema}.{tableName}_OrderId").OnTable(tableName).InSchema(schema).OnColumn("OrderId");
    }
}