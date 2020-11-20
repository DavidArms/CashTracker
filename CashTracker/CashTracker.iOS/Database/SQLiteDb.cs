using SQLite;
using CashTracker.iOS.Database;
using System;
using System.IO;
using Xamarin.Forms;
using CashTracker.Database;

[assembly: Dependency(typeof(SQLiteDb))]
namespace CashTracker.iOS.Database
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteDb()
        {
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "CashTracker.db3");

            return new SQLiteAsyncConnection(path);
        }

        public SQLiteConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "CashTracker.db3");

            return new SQLiteConnection(path);
        }
    }
}