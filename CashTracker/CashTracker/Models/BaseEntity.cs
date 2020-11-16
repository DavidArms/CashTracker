using SQLite;
using System;

namespace CashTracker.Models
{
    public class BaseEntity
    {
        /// <summary>
        /// The identifier/primary key
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Date the entity was created
        /// </summary>
        [NotNull]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Date the entity was last modified
        /// </summary>
        [NotNull]
        public DateTime ModifiedDate { get; set; }

    }
}
