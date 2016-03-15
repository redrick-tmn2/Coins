using System;
using System.Reflection;
using CoinsApplication.DAL.NHibernate.Migrations;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors.SQLite;

namespace CoinsApplication.DAL.NHibernate.Migrator
{
    public class SqLiteMigrator
    {
        private class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
            public string ProviderSwitches { get; set; }
        }

        public void Migrate(Action<IMigrationRunner> runnerAction)
        {
            var options = new MigrationOptions
            {
                PreviewOnly = false, Timeout = 0
            };

            var factory = new SQLiteProcessorFactory();
            var assembly = Assembly.GetAssembly(typeof(InitialMigration));
            
            var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            var migrationContext = new RunnerContext(announcer);

            var processor = factory.Create(NHibernateConfigurationFactory.GetConnectionString(), announcer, options);

            var runner = new MigrationRunner(assembly, migrationContext, processor);
            runnerAction(runner);
        }
    }
}
