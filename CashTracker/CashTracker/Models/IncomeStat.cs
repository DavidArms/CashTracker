//using SQLite;
//using SQLiteNetExtensions.Attributes;
using System;

namespace StacksStats.Models
{
    /// <summary>
    /// A statistic related to the money made and hours worked on a job
    /// </summary>
    public class IncomeStat
    {
        /// <summary>
        /// Identifier
        /// </summary>
        //[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        /// <summary>
        /// The job to which this income stat is related
        /// </summary>
        //[ForeignKey(typeof(Job))]
        public int Job_ID { get; set; }

        /// <summary>
        /// The amount of money earned
        /// </summary>
        //[NotNull]
        public double MoneyMade { get; set; }

        /// <summary>
        /// The amount of hours worked on the job
        /// </summary>
        //[NotNull]
        public double HoursWorked { get; set; }

        /// <summary>
        /// The date of when this work was performed
        /// </summary>
        //[NotNull]
        public DateTime Date { get; set; }

        /// <summary>
        /// Integer representing the day of the week of when this work was performed
        /// </summary>
        /// <remarks>Could be derived from the Date property but this could be quicker</remarks>
        //[NotNull]
        public int DayOfWeek { get; set; }
    }
}
