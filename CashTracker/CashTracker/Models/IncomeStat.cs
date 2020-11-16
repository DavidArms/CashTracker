using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace CashTracker.Models
{
    /// <summary>
    /// A statistic related to the money made and hours worked on a job
    /// </summary>
    public class IncomeStat : BaseEntity
    {
        /// <summary>
        /// The job to which this income stat is related
        /// </summary>
        [ForeignKey(typeof(Job))]
        public int Job_ID { get; set; }

        /// <summary>
        /// The amount of money earned
        /// </summary>
        [NotNull]
        public double MoneyMade { get; set; }

        /// <summary>
        /// The amount of hours worked on the job
        /// </summary>
        [NotNull]
        public double HoursWorked { get; set; }

        /// <summary>
        /// The date of when this work began
        /// </summary>
        [NotNull]
        public DateTime DateStart { get; set; }
    }
}
