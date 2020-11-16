using SQLite;
using System;

namespace CashTracker.Models
{
    /// <summary>
    /// The job creating trackable income for the user
    /// </summary>
    public class Job : BaseEntity
    {
        /// <summary>
        /// The name of the job
        /// </summary>
        [NotNull]
        public string Name { get; set; }

        /// <summary>
        /// The name of the employer/entity who is paying for the job
        /// </summary>
        public string Employer { get; set; }
    }
}
