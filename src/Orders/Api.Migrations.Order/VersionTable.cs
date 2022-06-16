using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.Options;

namespace Api.Migrations.Order;

public class VersionTable : DefaultVersionTableMetaData
{
    public VersionTable(IConventionSet conventionSet, IOptions<RunnerOptions> runnerOptions) : base(conventionSet, runnerOptions)
    {
    }

    public override string SchemaName => "order";
}