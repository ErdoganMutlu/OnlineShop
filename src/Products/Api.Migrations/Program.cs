using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Npgsql;


namespace Api.Migrations;

static class Program
{
    private static void Main(string[] args)
    {
        var migrationArgs = EnsureDefaultsSet();

        CreateDatabaseIfNotExists(migrationArgs.ConnectionString);

        var schemas = GetSchemas(migrationArgs.Schemas);
        foreach (var schema in schemas)
        {
            var serviceProvider = CreateServices(migrationArgs, schema);

            using var scope = serviceProvider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
        }
    }
        
    /// <summary>
    /// Configure the dependency injection services
    /// </summary>
    private static IServiceProvider CreateServices(MigrationArguments migrationArgs, string schemaName)
    {
        var assemblyName = $"{migrationArgs.AssemblyName}.{schemaName}";
        var assembly = Assembly.Load(assemblyName);
            
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(migrationArgs.ConnectionString)
                .ScanIn(assembly).For.All())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    }

    /// <summary>
    /// Update the database
    /// </summary>
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // Execute the migrations
        runner.MigrateUp();
    }

        
    static MigrationArguments EnsureDefaultsSet()
    {
        return new MigrationArguments
        {
            AssemblyName ="Api.Migrations",
            Schemas ="Product",
            ConnectionString ="Server=db;Database=online_product;Port=5432;User Id=postgres;Password=Secret!Passw0rd"  //TODO: I will move appSettings
        };
    }

    static string[] GetSchemas(string schemasArgs)
    {
        var schemasCsv = schemasArgs.ToString().Split(';');
        return schemasCsv;
    }


    private static void CreateDatabaseIfNotExists(string connectionString)
    {
        try
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);
            var databaseName = connectionStringBuilder.Database;
            connectionStringBuilder.Database = "postgres";
            
            using var connection = new NpgsqlConnection(connectionStringBuilder.ToString());
            connection.Open();

            using var command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $"CREATE DATABASE {databaseName};";
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            // ignored
        }
    }
}