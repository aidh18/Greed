using System;
using System.Collections.Generic;
using Greed.Game.Casting;
using Greed.Game.Services;


namespace Greed.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;
        private int updatedFrameRate = 10;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        private int points = 100;

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the player.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            // something else
            Actor player = cast.GetFirstActor("player");
            Point velocity = keyboardService.GetDirection();
            player.SetVelocity(velocity);
        }

        /// <summary>
        /// Updates the player's position and resolves any collisions with minerals.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor score = cast.GetFirstActor("score");
            Actor player = cast.GetFirstActor("player");
            List<Actor> minerals = cast.GetActors("minerals");

            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            player.MoveNext(maxX, maxY);

            Random random = new Random();
            foreach (Actor mineral in minerals)
            {
                mineral.MoveNext(maxX, maxY);
                
                if (player.GetPosition().Equals(mineral.GetPosition()))
                {
                    Mineral m = (Mineral) mineral;
                    points += m.GetPoints();
                    score.SetText(points.ToString());
                    
                    videoService.UpdateFrameRate(updatedFrameRate);
                    
                    int x = random.Next(0,60);
                    int y = 0;
                    Point position = new Point(x, y);
                    position = position.Scale(Program.CELL_SIZE);
                    mineral.SetPosition(position);

                }
            } 
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }

    }
}