using CashTracker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CashTracker.Database
{
    public class JobRepository : BaseSQLiteRepository
    {
        readonly List<Job> jobs;

        /// <summary>
        /// The connection to the database
        /// </summary>
        public SQLiteConnection Connection => DependencyService.Get<ISQLiteDb>().GetConnection();

        /// <summary>
        /// Constructor for repo
        /// </summary>
        /// <remarks>Creates the tables we need if they don't already exist in the DB connection</remarks>
        public JobRepository()
        {
            Connection.CreateTable<Job>();
            Connection.CreateTable<IncomeStat>();
        }

        
    }
}