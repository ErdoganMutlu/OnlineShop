using FluentMigrator;

namespace Api.Migrations.Product.Migrations;

[Migration(1)]
public class M00001CreateProductsTable : AutoReversingMigration
{
    public override void Up()
    {
        var schema = "product";
        var tableName = "Products";

        Create.Table(tableName).InSchema(schema)
            .WithColumn("Id").AsInt32().Identity()
            .WithColumn("Name").AsString(512).NotNullable()
            .WithColumn("Price").AsDouble().NotNullable()
            .WithColumn("Details").AsString(int.MaxValue).Nullable();


        Create.PrimaryKey($"PK_{schema}.{tableName}").OnTable(tableName).WithSchema(schema).Column("Id");            
    }
}