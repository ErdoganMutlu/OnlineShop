using FluentMigrator;

namespace Api.Migrations.Customer.Migrations;

[Migration(1)]
public class M00001CreateProductsTable : AutoReversingMigration
{
    public override void Up()
    {
        var schema = "customer";
        var tableName = "Customers";

        Create.Table(tableName).InSchema(schema)
            .WithColumn("Id").AsInt32().Identity()
            .WithColumn("Name").AsString(512).NotNullable()
            .WithColumn("Surname").AsString(512).NotNullable()
            .WithColumn("Details").AsString(int.MaxValue).Nullable();


        Create.PrimaryKey($"PK_{schema}.{tableName}").OnTable(tableName).WithSchema(schema).Column("Id");            
    }
}