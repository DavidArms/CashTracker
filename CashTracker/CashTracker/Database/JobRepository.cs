using CashTracker.Models;

namespace CashTracker.Database
{
    /// <summary>
    /// Database repository for the <see cref="Job"/> entity
    /// </summary>
    public class JobRepository : BaseSQLiteRepository<Job>
    {
        //readonly List<Job> jobs; // TODO: Maybe could cache the jobs here if they are queried for?

        /// <summary>
        /// Constructor for repo
        /// </summary>
        public JobRepository()
        { }
 
    }
}