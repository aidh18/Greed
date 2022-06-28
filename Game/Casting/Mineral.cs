namespace Greed.Game.Casting
{
    /// <summary>
    /// <para>A solid inorganic substace of natural occurrence.</para>
    /// <para>
    /// The responsibility of a Mineral is to provide the points associated with it.
    /// </para>
    /// </summary>
    public class Mineral : Actor
    {
        private int points = 0;

        /// <summary>
        /// Constructs a new instance of an Mineral.
        /// </summary>
        public Mineral()
        {
        }

        /// <summary>
        /// Gets the mineral's points.
        /// </summary>
        /// <returns>The message.</returns>
        public string GetMessage()
        {
            return message;
        }

        /// <summary>
        /// Sets the artifact's message to the given value.
        /// </summary>
        /// <param name="message">The given message.</param>
        public void SetMessage(string message)
        {
            this.message = message;
        }
    }
}