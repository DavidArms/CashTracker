using SQLite;

namespace CashTracker.Database
{
    /// <summary>
    /// A SQLite Db
    /// </summary>
    public interface ISQLiteDb
    {
        SQLiteConnection GetConnection();
    }
}
