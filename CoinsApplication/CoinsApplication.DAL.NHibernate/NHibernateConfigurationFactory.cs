using System.IO;
using CoinsApplication.DAL.NHibernate.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using Environment = System.Environment;

namespace CoinsApplication.DAL.NHibernate
{
    internal class NHibernateConfigurationFactory
    {
        private const string DatabaseFileName = "CoinsCollection.db";

        #region private methods

        private static string GetDatabaseFile()
        {
            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(documentsFolder, DatabaseFileName);
        }

        private static void BuildSchema(Configuration config)
        {
            config.Properties.Add("current_session_context_class", "thread_static");
        }

        #endregion

        #region public methods

        public static string GetConnectionString()
        {
            return $"data source = {GetDatabaseFile()}";
        }

        public static FluentConfiguration GetConfiguration()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(GetDatabaseFile()))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CoinMap>())
                .ExposeConfiguration(BuildSchema);
        }

        #endregion
    }
}