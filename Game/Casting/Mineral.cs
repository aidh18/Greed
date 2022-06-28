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
        /// Constructs a new instance of a Mineral.
        /// </summary>
        public Mineral(int points)
        {
            this.points = points;
        }

        /// <summary>
        /// Gets the mineral's points.
        /// </summary>
        /// <returns>The points.</returns>
        public int GetPoints()
        {
            return points;
        }
    }
}