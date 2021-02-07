namespace CashTracker.Controls
{
    /// <summary>
    /// An abstraction for a message to display to the user
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// A message that shows for a "long" time
        /// </summary>
        /// <param name="message"></param>
        void LongAlert(string message);

        /// <summary>
        /// A message that shows for a "short" time
        /// </summary>
        /// <param name="message"></param>
        void ShortAlert(string message);
    }
}
