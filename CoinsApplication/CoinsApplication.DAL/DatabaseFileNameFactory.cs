using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinsApplication.DAL
{
    public static class DatabaseFileNameFactory
    {
        const string DatabaseFileName = "CoinsCollection.db";

        public static string GetDatabaseFileName()
        {
            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentsFolder, DatabaseFileName);

            return databasePath;
        }
    }
}
