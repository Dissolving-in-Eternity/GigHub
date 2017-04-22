using NUnit.Framework;
using System.Data.Entity.Migrations;

namespace GigHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            // Reference to configuration class of code-first migrations
            // In order to bring our integration test DB to the latest version
            var configuration = new GigHub.Migrations.Configuration();

            // DB-migrator object
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
