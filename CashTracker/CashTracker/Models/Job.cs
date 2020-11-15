//using SQLite;
using System;

namespace CashTracker.Models
{
    /// <summary>
    /// The job creating trackable income for the user
    /// </summary>
    public class Job
    {
        /// <summary>
        /// The identifier for the job
        /// </summary>
        //[PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// The name of the job
        /// </summary>
        //[NotNull]
        public string Name { get; set; }

        /// <summary>
        /// When the job was created in the app
        /// </summary>
        //[NotNull]
        public DateTime CreationDate { get; set; }
    }
}
