using System;
using System.IO;
using SQLite;
using CashTracker.Database;
using CashTracker.Droid.Database;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace CashTracker.Droid.Database
{
    /// <summary>
    /// The backing SQLite Database
    /// </summary>
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteDb()
        {
        }

        /// <summary>
        /// Returns the connection to the app's database
        /// </summary>
        /// <returns></returns>
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "CashTrackerDB.db3");
            
            return new SQLiteAsyncConnection(path);
        }

        /// <summary>
        /// Returns the connection to the app's database
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "CashTrackerDB.db3");

            return new SQLiteConnection(path);
        }
    }
}